
using Microsoft.AspNetCore.Mvc;


namespace DoctorType.Web.Areas.Admin.ViewComponents
{
    public class SiteSideBarViewComponent : ViewComponent
    {
        #region Ctor

       

     

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View("SiteSideBar");
        }
    }
}
