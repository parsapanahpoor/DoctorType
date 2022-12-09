using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Admin.Tariff
{
    public class CreateTariffViewModel
    {
        #region properties

        [Display(Name = "عنوان تعرفه")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string TariffName { get; set; }

        [Display(Name = "مبلغ تعرفه")]
        public int TariffPrice { get; set; }

        [Display(Name = "تعداد آگهی های قابل پذیرفتن ")]
        public int CountOfAcceptRequest { get; set; }

        [Display(Name = "مدت زمان تعرفه")]
        public int TariffDuration { get; set; }

        public string ColorName { get; set; }

        #endregion
    }
}
