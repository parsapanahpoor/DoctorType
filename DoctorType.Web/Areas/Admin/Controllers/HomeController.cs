
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region Constructor

        //private readonly IAdminHomeIndexService _adminHomeIndex;
        //private readonly IAdvertisementService _advertisementService;
        //private readonly IAdsCategoryService _adsCategoryService;
        //public HomeController(IAdminHomeIndexService adminHomeIndex, IAdvertisementService advertisementService
        //    , IAdsCategoryService adsCategoryService)
        //{
        //    _adminHomeIndex = adminHomeIndex;
        //    _advertisementService = advertisementService;
        //    _adsCategoryService = adsCategoryService;
        //}

        #endregion

        #region Admin Dashboard

        public async Task<IActionResult> Index()
        {
            //var model = await _adminHomeIndex.SetAdminHomeIndexViewModel();
            return View();
        }

        #endregion

    }
}
