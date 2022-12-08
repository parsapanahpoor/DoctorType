using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory
{
    public class AdvertisementCategoryViewModel
    {
        #region Properties

        [Display(Name = " نام فارسی دسته بندی    ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupName { get; set; }

        [Display(Name = " نام لاتین دسته بندی    ")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? UrlName { get; set; }

        public ulong? ParentId { get; set; }

        public ulong? CatgeoryId { get; set; }

        #endregion

    }

    public enum CreateAdsCategoryResult
    {
        [Display(Name = "موفق")]
        Success,
        [Display(Name = "ارور تصویر ")]
        ImageError,
        [Display(Name = "ناموفق ")]
        Faild,
        CategoryNotFound

    }
}
