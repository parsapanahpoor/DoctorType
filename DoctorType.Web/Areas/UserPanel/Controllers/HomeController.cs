using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        #region Ctor

        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        #endregion

        #region User Panel Dashboard

        public async Task<IActionResult> Index()
        {
            return View(await _dashboardService.FillUserPanelDashboardViewModel(User.GetUserId()));
        }

        #endregion
    }
}
