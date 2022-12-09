using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
using DoctorType.Web.HttpManager;
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
            return View(await _advertisementService.FilterAdvertisementFromUserPanel(User.GetUserId()));
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

        #region Edit Advertisement

        [HttpGet]
        public async Task<IActionResult> EditAdvertisement(ulong advertisementId)
        {
            #region Fill Model 

            var model = await _advertisementService.FillEditAdvertisementUserPanelViewModel(advertisementId, User.GetUserId());
            if (model == null)
            {
                TempData[ErrorMessage] = "اطلاعات یافت نشده است.";
                return RedirectToAction(nameof(ListOfAdvertisement)) ;
            }

            #endregion

            return View(model);
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdvertisement(EditAdvertisementUserPanelViewModel model)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                return View(model);
            }

            #endregion

            #region Edit Advertisement

            var res = await _advertisementService.EditAdvertisementFromUserPanel(model , User.GetUserId());
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

        #region Delete Advertisement 

        public async Task<IActionResult> DeleteAdvertisement(ulong advertisementId)
        {
            //Delete Method 
            var res = await _advertisementService.DeleteAdvertisementFromUserPanel(advertisementId , User.GetUserId());

            if (res)
            {
                return JsonResponseStatus.Success(null, "عملیات باموفقیت انجام شده است .");
            }

            return JsonResponseStatus.Error(null, "عملیات باشکست روبرو شده است .");
        }

        #endregion
    }
}
