using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Enums.Advertisement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorType.Domain.ViewModels.UserPanel.Advertisement
{
    public class CreateAdvertisementUserPanelSideViewModel
    {
        #region properties

        [Display(Name = " عنوان آگهی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        public ulong UserId { get; set; }

        [Display(Name = " توضیح کامل ")]
        public string? Description { get; set; }

        public IFormFile File{ get; set; }

        public List<ulong> AdvertisementSelectedSkills { get; set; }

        public List<AdvertisementCategory>? AdvertisementCategories { get; set; }

        #endregion
    }
}
