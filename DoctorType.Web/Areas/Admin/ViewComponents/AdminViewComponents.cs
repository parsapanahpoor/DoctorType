using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorType.Web.Areas.Admin.ViewComponents
{

    #region Admin SideBar ViewComponent

    public class AdminSideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminSideBar");
        }
    }

    #endregion

    #region Admin Header ViewComponent

    public class AdminHeaderViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUserService _userService;

        public AdminHeaderViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminHeader", await _userService.GetUserById(User.GetUserId()));
        }
    }

    #endregion

    #region Admin Chatbox ViewComponent

    public class AdminChatboxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminChatbox");
        }
    }

    #endregion

    #region Admin Footer ViewComponent

    public class AdminFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminFooter");
        }
    }

    #endregion

    #region Notification Tickets ViewComponent

    public class NotificationTicketsViewComponent : ViewComponent
    {
        //#region Constructor

        //private readonly IContactUsService _contactUsService;
        //public NotificationTicketsViewComponent(IContactUsService contactUsService)
        //{
        //    this._contactUsService = contactUsService;
        //}

        //#endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("NotificationTickets");
        }
    }

    #endregion

    #region Incoming Email Messages

    public class UnReadEmailsViewComponent : ViewComponent
    {
        //private readonly IContactUsService _contactUsService;

        //public UnReadEmailsViewComponent(IContactUsService contactUsService)
        //{
        //    _contactUsService = contactUsService;
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //List<Domain.Entities.ContactUs.ContactUs> Messages = await _contactUsService.GetLastestEmailMessages(5);

            return await Task.FromResult((IViewComponentResult)View("UnReadEmails"));
        }
    }

    #endregion
}
