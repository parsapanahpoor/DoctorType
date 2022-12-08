using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Entities.Common;
using DoctorType.Domain.Entities.Tariff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entities.Account
{
    public class User : BaseEntity
    {
        #region properties

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        [MaxLength(300, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BithDay { get; set; }

        public string? FatherName { get; set; }

        [MaxLength(30)]
        public string? NationalId { get; set; }

        public string? ExtraPhoneNumber { get; set; }

        public string? HomePhoneNumber { get; set; }

        public string? WorkAddress { get; set; }

        [Display(Name = "تلفن همراه")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست .")]
        public string? Email { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? Avatar { get; set; }

        [Display(Name = "کد فعالسازی ایمیل")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? EmailActivationCode { get; set; }

        [Display(Name = "کد فعالسازی موبایل")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string MobileActivationCode { get; set; }

        public bool IsMobileConfirm { get; set; } = false;

        public bool IsEmailConfirm { get; set; } = false;

        public bool IsBan { get; set; } = false;

        public bool IsAdmin { get; set; } = false;

        public bool BanForTicket { get; set; }

        public bool BanForComment { get; set; }

        public int WalletBalance { get; set; }

        [Display(Name = "تاریخ انقضای تایم اس ام اس فعال سازی ")]
        public DateTime? ExpireMobileSMSDateTime { get; set; }

        public bool IsUserExpert { get; set; }

        #endregion

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        public List<UserSelectedTariff> UserSelectedTariff { get; set; }

        public List<ExpertsSelectedSkils> ExpertsSelectedSkils { get; set; }

        public List<Advertisemenet> Advertismenets { get; set; }

        public List<Wallet.Wallet> Wallets { get; set; }

        #endregion
    }
}
