using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoctorType.Web.Areas.UserPanel.ActionFilterAttributes
{
    public class CheckUserIsExpert : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService))!;

            base.OnActionExecuting(context);

            var user = service.GetUserById(context.HttpContext.User.GetUserId()).Result;

            if (!user.IsUserExpert)
            {
                context.HttpContext.Response.Redirect("/");
            }
        }
    }
}
