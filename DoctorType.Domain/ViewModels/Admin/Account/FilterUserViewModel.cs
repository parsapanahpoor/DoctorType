﻿using BasePaging.Domain.ViewModels.Common;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Admin
{
    public class FilterUserViewModel : BasePaging<User>
    {
        public FilterUserViewModel()
        {
            Status = UserStatus.All;
        }

        public string? FullName { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ وارد شده معتبر نمی باشد")]
        public string? FromDate { get; set; }

        [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ وارد شده معتبر نمی باشد")]
        public string? ToDate { get; set; }

        public ulong RoleId { get; set; }

        public bool TodayRegister { get; set; }

        public UserStatus Status { get; set; }

    }

    public enum UserStatus
    {
        [Display(Name = "همه")] All,
        [Display(Name = "ایمیل تایید شده")] EmailConfirmed,
        [Display(Name = "ایمیل تایید نشده")] EmailNotConfirmed,
        [Display(Name = "موبایل تایید شده")] MobileConfirmed,
        [Display(Name = "موبایل تایید نشده")] MobileNotConfirmed,
        [Display(Name = "محدود برای ارسال کامنت")] BanForComment,
        [Display(Name = "مسدود شده")] IsBan,
    }
}
