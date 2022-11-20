using DoctorType.Web.Areas.Admin.ActionFilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorType.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [CheckUserHasPermission]

    public class AdminBaseController : Controller
    {
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";
        protected string ErrorMessage = "ErrorMessage";
    }
}
