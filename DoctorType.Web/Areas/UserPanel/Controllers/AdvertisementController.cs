using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    public class AdvertisementController : UserPanelBaseController
    {
        #region Ctor 

        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        #endregion

        #region List OF Advertisement

        public async Task<IActionResult> ListOfAdvertisement()
        {
            return View();
        }

        #endregion

        #region Create Advertisement

        [HttpGet]
        public async Task<IActionResult> CreateAdvertisement()
        {
            return View(await _advertisementService.FillCreateAdvertisementUserPanelSideViewModel(User.GetUserId()));
        }
        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdvertisement(CreateAdvertisementUserPanelSideViewModel model)
        {
            #region Model State Validation

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                return View(model);
            }

            #endregion

            #region Create Advertisement

            var res = await _advertisementService.CreateAdvertisementFromUserPanel(model);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfAdvertisement));
            }

            #endregion

            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return View(model);
        }

        #endregion
    }
}
