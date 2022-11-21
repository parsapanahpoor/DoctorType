using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.Access;
using DoctorType.Web.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    public class RoleController : AdminBaseController
    {
        #region Ctor

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        #region List Of Roles

        public async Task<IActionResult> ListOfRoles()
        {
            return View(await _roleService.FilterRoles());
        }

        #endregion

        #region Create Role

        public async Task<IActionResult> CreateRole()
        {
            ViewData["AllPermissions"] = await _roleService.GetAllPermissions();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(Domain.ViewModels.Access.CreateRoleViewModel roleViewModel, List<ulong> SelectedPermissions)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.CraeteRole(roleViewModel, SelectedPermissions);
                switch (result)
                {
                    case Domain.ViewModels.Access.CreateRoleResult.Success:
                        TempData[SuccessMessage] = "نقش جدید با موفقیت اضافه شد!";
                        return RedirectToAction("ListOfRoles");

                    case Domain.ViewModels.Access.CreateRoleResult.RoleTitleExists:
                        TempData[WarningMessage] = "عنوان نقش وارد شده در سیستم وجود دارد!";
                        break;
                }
            }

            ViewData["AllPermissions"] = await _roleService.GetAllPermissions();
            return View(roleViewModel);
        }

        #endregion

        #region Edit Role

        public async Task<IActionResult> EditRole(ulong roleId)
        {
            ViewData["AllPermissions"] = await _roleService.GetAllPermissions();
            ViewData["PermissionsOfRole"] = await _roleService.GetPermissionsOfRole(roleId);
            var role = await _roleService.GetRoleForEdit(roleId);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Domain.Entities.Account.Role role, List<ulong> SelectedPermissions)
        {
            var result = await _roleService.EditRole(role, SelectedPermissions);
            switch (result)
            {
                case EditRoleResult.Success:
                    TempData[SuccessMessage] = "نقش مورد نظر با موفقیت ویرایش شد!";
                    return RedirectToAction("ListOfRoles");

                case EditRoleResult.RoleTitleExists:
                    TempData[WarningMessage] = "عنوان نقش وارد شده در سیستم وجود دارد!";
                    break;

                case EditRoleResult.RoleNotFound:
                    return NotFound();
            }

            ViewData["AllPermissions"] = await _roleService.GetAllPermissions();
            ViewData["PermissionsOfRole"] = await _roleService.GetPermissionsOfRole(role.Id);
            return View(role);
        }
        #endregion

        #region Delete Role

        public async Task<IActionResult> DeleteRole(ulong roleId)
        {
            var result = await _roleService.SoftDeleteRoleByAdmin(roleId);

            if (result)
            {
                return JsonResponseStatus.Success(null, "عملیات باموفقیت انجام شده است .");
            }

            return JsonResponseStatus.Error(null, "عملیات باشکست روبرو شده است .");
        }

        #endregion

    }
}
