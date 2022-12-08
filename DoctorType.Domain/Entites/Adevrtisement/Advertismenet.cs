using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Entities.Common;
using DoctorType.Domain.Enums.Advertisement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorType.Domain.Entites.Adevrtisement
{
    public class Advertisemenet : BaseEntity
    {
        #region properties

        [Display(Name = " عنوان آگهی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        public ulong UserId { get; set; }

        public AdvertismenetState AdvertismenetState { get; set; }

        public string? Price { get; set; }

        [Display(Name = " توضیح کامل ")]
        public string? Description { get; set; }

        #endregion

        #region relation

        public User User { get; set; }

        public ICollection<AdvertisementSelectedSkill> AdvertisementSelectedSkills { get; set; }

        #endregion
    }
}
