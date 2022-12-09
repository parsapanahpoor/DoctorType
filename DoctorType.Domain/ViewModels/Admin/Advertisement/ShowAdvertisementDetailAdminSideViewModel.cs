using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorType.Domain.ViewModels.Admin.Advertisement
{
    public class ShowAdvertisementDetailAdminSideViewModel
    {
        public ulong AdvertisementId { get; set; }

        public User User { get; set; }

        [Display(Name = " عنوان آگهی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = " توضیح کامل ")]
        public string? Description { get; set; }

        public string File { get; set; }

        public DateTime CreateDate { get; set; }

        public int CountOfUserAdvertisement { get; set; }

        public int CountOfAcceptedUserAdvertisement { get; set; }

        public int CountOFWaitingUserAdvertisement { get; set; }

        public List<AdvertisementCategory> AdvertisementCategories { get; set; }
    }
}
