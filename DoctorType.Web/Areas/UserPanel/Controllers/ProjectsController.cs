using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Web.Areas.UserPanel.ActionFilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Areas.UserPanel.Controllers
{
    [CheckUserIsExpert]
    public class ProjectsController : UserPanelBaseController
    {
        #region Ctor

        private readonly IAdvertisementService _advertisementService;

        public ProjectsController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        #endregion

        #region List OF Advertisements

        public async Task<IActionResult> ListOfProjects()
        {
            #region Fill Models

            var model = await _advertisementService.ListOfProjectForWorkingOnInUserPanel(User.GetUserId());
            if (model == null)
            {
                TempData[WarningMessage] = "کاربر عزیز لطفا ابتدا تخصص های خود را انتخاب کنید.";
                return RedirectToAction("ManageUserSkills", "ExpertSkills", new { area = "UserPanel" });
            }

            #endregion

            return View(model);
        }

        #endregion

        #region Show Advertisement Detail

        public async Task<IActionResult> ShowAdvertisementDetail(ulong advertisementId)
        {
            #region Fill Model

            var model = await _advertisementService.FillShowAdvertisementDetailUserSideViewModel(advertisementId , User.GetUserId());
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        #endregion
    }
}
