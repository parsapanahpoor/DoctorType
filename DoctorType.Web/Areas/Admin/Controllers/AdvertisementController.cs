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
    }
}
