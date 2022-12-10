using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.UserPanel.ViewComponents
{
    #region UserPanel SideBar ViewComponent

    public class UserTariffViewComponents : ViewComponent
    {
        #region Ctor

        private readonly ITariffService _tariffService;

        public UserTariffViewComponents(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UserTariff", await _tariffService.GetUserTariffDetailForShowinHeader(User.GetUserId()));
        }
    }

    #endregion
}
