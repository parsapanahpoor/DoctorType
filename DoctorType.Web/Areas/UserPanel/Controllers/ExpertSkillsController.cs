using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.Entities.Account;
using DoctorType.Web.Areas.UserPanel.ActionFilterAttributes;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    [CheckUserIsExpert]
    public class ExpertSkillsController : UserPanelBaseController
    {
        #region Ctor

        private readonly IAdvertisementCategoryService _advertisementCategoryService;

        public ExpertSkillsController(IAdvertisementCategoryService advertisementCategoryService)
        {
            _advertisementCategoryService = advertisementCategoryService;
        }

        #endregion

        #region Manage User Skills 

        public async Task<IActionResult> ManageUserSkills()
        {
            return View(await _advertisementCategoryService.FillManageUserSkillsViewModel(User.GetUserId()));
        }

        #endregion

        #region Add Skill To The User Skills 

        public async Task<IActionResult> AddSkillToTheUserSkills(ulong skillId)
        {
            #region Add Skill To User Skills

            var res = await _advertisementCategoryService.AddSkillToTheUserSkills(skillId , User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است.";
                return RedirectToAction(nameof(ManageUserSkills));
            }

            #endregion

            TempData[ErrorMessage] = "شما از قبل این تخصص را انتخاب کرده اید.";
            return RedirectToAction(nameof(ManageUserSkills));
        }

        #endregion

        #region Remove Skill From User Skills

        public async Task<IActionResult> RemoveSkillFromUserSkills(ulong skillId)
        {
             #region Add Skill To User Skills

            var res = await _advertisementCategoryService.RemoveSkillFromUserSkills(skillId , User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است.";
                return RedirectToAction(nameof(ManageUserSkills));
            }

            #endregion

            TempData[ErrorMessage] = "شما از قبل این تخصص را انتخاب کرده اید.";
            return RedirectToAction(nameof(ManageUserSkills));
        }

        #endregion
    }
}
