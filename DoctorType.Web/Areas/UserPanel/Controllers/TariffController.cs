using DoctorType.Application.Convertors;
using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Implementation;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Application.StaticTools;
using DoctorType.Domain.DTOs.ZarinPal;
using DoctorType.Domain.Entities.Tariff;
using DoctorType.Domain.Entities.Wallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    public class TariffController : UserPanelBaseController
    {
        #region ctor

        private readonly ITariffService _tariffService;

        private readonly IUserService _userService;

        private readonly IWalletService _walletService;

        public TariffController(ITariffService tariffService, IUserService userService, IWalletService walletService)
        {
            _tariffService = tariffService;
            _userService = userService;
            _walletService = walletService;
        }

        #endregion

        #region List Of Tarrifs 

        public async Task<IActionResult> ListOfTarrifs()
        {
            #region Fill Model 

            var model = await _tariffService.ShowListOfTarrifsInUserPanel() ;
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        #endregion

        #region Buy Tariff 

        public async Task<IActionResult> BeforeBuyTariff(ulong tariffId)
        {
            #region Get Tariff

            var tariff = await _tariffService.GetTariffById(tariffId);
            if (tariff == null || tariff.TariffPrice == 0)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                return RedirectToAction("Index", "Home" , new { area = "UserPanel" });
            }

            #endregion

            #region Online Payment

            //Random rnd = new Random();
            //ulong requestId = (ulong)rnd.Next(1 , 9999999);

            return RedirectToAction("PaymentMethod", "Payment", new
            {
                area = "",
                gatewayType = GatewayType.Zarinpal,
                amount = tariff.TariffPrice,
                description = "شارژ حساب کاربری برای پرداخت هزینه ی تعرفه ی ماهانه",
                returURL = $"{PathTools.SiteAddress}/ChargeAccountAfterPayment/" + tariff.Id,
                requestId = tariff.Id
            });

            #endregion
        }

        #endregion

        #region Charge Account After Payment

        [HttpGet("ChargeAccountAfterPayment/{id}", Name = "ChargeAccountAfterPayment")]
        public async Task<IActionResult> ChargeAccountAfterPayment(ulong id)
        {
            #region Get User By User Id

            var user = await _userService.GetUserById(User.GetUserId());
            if (user == null) NotFound();

            #endregion

            #region Get Tariff

            var tariff = await _tariffService.GetTariffById(id);
            if (tariff == null || tariff.TariffPrice == 0)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

            #endregion

            try
            {
                #region Fill Parametrs

                VerifyParameters parameters = new VerifyParameters();

                if (HttpContext.Request.Query["Authority"] != "")
                {
                    parameters.authority = HttpContext.Request.Query["Authority"];
                }

                parameters.amount = tariff.TariffPrice.ToString();
                parameters.merchant_id = PathTools.merchant;

                #endregion

                using (HttpClient client = new HttpClient())
                {
                    #region Verify Payment

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);

                    string errors = jo["errors"].ToString();

                    #endregion

                    if (data != "[]")
                    {
                        //Authority Code
                        string refid = jodata["data"]["ref_id"].ToString();

                        //Get Wallet Transaction For Validation 
                        var wallet = await _walletService.FindWalletTransactionForRedirectToTheBankPortal(user.Id, GatewayType.Zarinpal, tariff.Id, parameters.authority, tariff.TariffPrice);

                        if (wallet != null)
                        {
                            await _walletService.UpdateWalletAndCalculateUserBalanceAfterBankingPayment(wallet);

                            //Pay Tariff
                            await _walletService.PayReservationTariff(User.GetUserId(), tariff.TariffPrice);

                            await _tariffService.GetTariffFromUser(User.GetUserId() , tariff);

                            return RedirectToAction("PaymentResult", "Payment", new { IsSuccess = true, refId = refid });
                        }
                    }
                    else if (errors != "[]")
                    {
                        string errorscode = jo["errors"]["code"].ToString();

                        return BadRequest($"error code {errorscode}");

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return NotFound();
        }

        #endregion
    }
}
