﻿using DoctorType.Application.Extensions;
using DoctorType.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoctorType.Web.Areas.Admin.ActionFilterAttributes
{
    public class CheckUserHasPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;

            base.OnActionExecuting(context);

            var hasUserAnyRole = service.IsUserAdmin(context.HttpContext.User.GetUserId()).Result;

            if (!hasUserAnyRole)
            {
                context.HttpContext.Response.Redirect("/");
            }
        }
    }
}
