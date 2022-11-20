using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        #region Ctor

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region List Of Users 

        public async Task<IActionResult> ListOfUsers()
        {
            return View(await _userService.GetTheListOfSimpleUsersForShowInDataTablesInAdminPanel());
        }

        #endregion
    }
}
