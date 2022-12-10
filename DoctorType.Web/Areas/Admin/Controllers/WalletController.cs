using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.Admin.Wallet;
using DoctorType.Web.HttpManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class WalletController : AdminBaseController
    {
        #region Ctor

        private readonly IWalletService _walletService;
        private readonly IUserService _userService;

        public WalletController(IWalletService walletService, IUserService userService)
        {
            _walletService = walletService;
            _userService = userService;
        }

        #endregion

        #region List Of Wallets

        public async Task<IActionResult> Index(FilterWalletViewModel model)
        {
            var filter = await _walletService.FilterWalletsAsync(model);
            return View(filter);
        }

        #endregion

        #region Create Wallet

        public async Task<IActionResult> CreateWallet(ulong id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                TempData[ErrorMessage] = "کاربر یافت نشده است.";
                return RedirectToAction("FilterUsers", "Account");
            }

            var model = new AdminCreateWalletViewModel
            {
                UserId = id,
                User = user
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet(ulong id, AdminCreateWalletViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.User = await _userService.GetUserById(id);
                return View(model);
            }

            var response = await _walletService.CreateWalletAsync(model);

            switch (response)
            {
                case AdminCreateWalletResponse.Success:
                    TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                    return RedirectToAction("AccountDetail", "Account", new { id = id });
                    break;

                case AdminCreateWalletResponse.UserNotFound:
                    TempData[ErrorMessage] = "کاربری یافت نشده است.";
                    return RedirectToAction("FilterUsers", "Account");

                default:
                    TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
                    break;
            }

            model.User = await _userService.GetUserById(id);
            return View(model);
        }

        #endregion

        #region Edit Wallet

        public async Task<IActionResult> EditWallet(ulong id)
        {
            var editableWallet = await _walletService.GetWalletForEditAsync(id);

            if (editableWallet == null)
            {
                TempData[ErrorMessage] = "کاربری یافت نشده است.";
                return RedirectToAction("Index");
            }


            return View(editableWallet);
        }

        [HttpPost]
        public async Task<IActionResult> EditWallet(ulong id, AdminEditWalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.User = await _userService.GetUserById(id);
                return View(model);
            }

            var response = await _walletService.EditWalletAsync(model);

            switch (response)
            {
                case AdminEditWalletResponse.Success:
                    TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                    return RedirectToAction("Index");

                case AdminEditWalletResponse.WalletNotFound:
                    TempData[ErrorMessage] = "کاربری یافت نشده است.";
                    return RedirectToAction("Index");
                    break;

                default:
                    TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
                    break;
            }

            model.User = await _userService.GetUserById(id);
            return View(model);
        }

        #endregion

        #region Remove Wallet

        public async Task<IActionResult> RemoveWallet(ulong id)
        {
            var result = await _walletService.RemoveWalletAsync(id);

            if (result)
            {
                return ApiResponse.SetResponse(ApiResponseStatus.Success, null,
                    "عملیات باموفقیت انجام شده است.");
            }

            return ApiResponse.SetResponse(ApiResponseStatus.Danger, null,
                "عملیات باشکست مواجه شده است.");
        }

        #endregion
    }
}
