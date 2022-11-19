﻿using DoctorType.Web.HttpServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoctorType.Web.HttpManager
{
    public class BackButtonActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.Controller is Controller controller)
            {
                controller.ViewBag.UrlReferer = context.HttpContext.Request.GetUrlReferer();
            }
        }
    }
}
