using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.ViewModels.Account;
using DoctorType.Web.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace DoctorType.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Ctor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Register

        [HttpGet("Register")]
        [RedirectHomeIfLoggedInActionFilter]
        public IActionResult Register(string? mobile)
        {
            #region Redirect Mobile 

            if (!string.IsNullOrEmpty(mobile))
            {
                TempData[SuccessMessage] = "لطفا اطلاعات خواسته شده را جهت ادامه ی مراحل ثبت نام وارد نمایید.";

                return View(new RegisterUserViewModel()
                {
                    Mobile = mobile
                });
            }

            #endregion

            return View();
        }

        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            #region Model State Validations

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "مقادیر وارد شده معتبر نمی باشد . ";
                return View(register);
            }

            #endregion

            #region Register User Methods

            var result = await _userService.RegisterUser(register);

            switch (result)
            {
                case RegisterUserResult.MobileExist:
                    TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                    TempData[InfoMessage] = "در صورتی که از قبل در سایت ثبت نام کردید از گزینه ی ورود به سایت استفاده کنید";
                    ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
                    break;

                case RegisterUserResult.SiteRoleNotAccept:
                    TempData[ErrorMessage] = "قوانین سایت باید پذیرفته شوند ";
                    break;

                case RegisterUserResult.Success:
                    TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد .";
                    TempData[InfoMessage] = $"پیامی  حاوی کد فعالسازی حساب کاربری به {register.Mobile} ارسال شد .";

                    return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = register.Mobile });
            }

            #endregion

            return View(register);
        }

        #endregion

    }
}
