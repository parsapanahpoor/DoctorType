using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorType.Domain.Entites.Adevrtisement
{
    public class AdvertisementCategory : BaseEntity
    {
        #region Properties

        [Display(Name = " نام فارسی دسته بندی    ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = " نام لاتین دسته بندی    ")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? UrlName { get; set; }

        public ulong? ParentId { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public ICollection<AdvertisementCategory> CategoryParent { get; set; }

        public List<ExpertsSelectedSkils> ExpertsSelectedSkils { get; set; }

        #endregion
    }
}
