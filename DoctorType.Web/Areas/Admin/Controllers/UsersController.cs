using DoctorType.Application.Services.Implementation;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        #region Ctor

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #endregion

        #region List Of Users 

        public async Task<IActionResult> ListOfUsers()
        {
            return View(await _userService.GetTheListOfSimpleUsersForShowInDataTablesInAdminPanel());
        }

        #endregion

        #region Edit User

        [HttpGet]
        public async Task<IActionResult> EditUserInfo(ulong id)
        {
            #region Fill View Model

            var result = await _userService.FillAdminEditUserInfoViewModel(id);

            if (result == null) return NotFound();

            #endregion

            #region Page Data

            ViewBag.ListOfRoles = await _roleService.GetAllRoles();
            ViewBag.UserRole = await _roleService.GetUserRoles((ulong)id);

            #endregion

            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserInfo(AdminEditUserInfoViewModel edit, List<ulong> Roles, IFormFile? UserAvatar)
        {
            #region Model State Validation

            if (!ModelState.IsValid)
            {
                #region Page Data

                ViewBag.ListOfRoles = await _roleService.GetAllRoles();
                ViewBag.UserRole = await _roleService.GetUserRoles((ulong)edit.UserId);

                #endregion

                TempData[ErrorMessage] = "ناموفق";
                return View(edit);
            }

            #endregion

            #region Edit User Method

            var result = await _userService.EditUserInfo(edit, UserAvatar, Roles);

            switch (result)
            {
                case AdminEditUserInfoResult.NotValidImage:
                    TempData[ErrorMessage] = "تصویر وارد شده مورد تایید نمی باشد.";
                    break;
                case AdminEditUserInfoResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشده است.";
                    return RedirectToAction("FilterUsers", "Account", new { area = "Admin" });

                case AdminEditUserInfoResult.Success:
                    TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                    return RedirectToAction("AccountDetail", "Account", new { area = "Admin", id = edit.UserId });

                case AdminEditUserInfoResult.NotValidEmail:
                    TempData[ErrorMessage] = "ایمیل ولرد شده معتبر نمی باشد.";
                    break;
                case AdminEditUserInfoResult.NotValidMobile:
                    TempData[ErrorMessage] = "موبایل وارد شده مورد تایید نمی باشد.";
                    break;
                case AdminEditUserInfoResult.NotValidNationalId:
                    TempData[ErrorMessage] = "کدملی وارد شده مورد تایید نمی باشد";
                    break;
            }

            #endregion

            #region Page Data

            ViewBag.ListOfRoles = await _roleService.GetAllRoles();
            ViewBag.UserRole = await _roleService.GetUserRoles((ulong)edit.UserId);

            #endregion

            return View(edit);
        }

        #endregion
    }
}
