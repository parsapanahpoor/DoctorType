using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class AdvertisementController : AdminBaseController
    {
        #region Ctor

        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        #endregion

        #region list Of Advertisements

        public async Task<IActionResult> ListOfAdvertisements()
        {
            return View(await _advertisementService.ListOfAdvertisementAdminSide()); 
        }

        #endregion

        #region Show Advertisement Detail

        public async Task<IActionResult> ShowAdvertisementDetail(ulong advertisementId)
        {
            #region Fill Model

            var model = await _advertisementService.FillShowAdvertisementDetailAdminSideViewModel(advertisementId);
            if (model == null) return NotFound();

            #endregion

            return View(model) ;
        }

        #endregion
    }
}
