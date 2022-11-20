using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entities.SiteSetting
{
    public class SiteSetting : BaseEntity
    {
        [Display(Name = "متن کپی رایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CopyRightText { get; set; }

        [Display(Name = "تایم  ارسال اس ام اس ")]
        public int SendSMSTimer { get; set; }

        public string SiteDomain { get; set; }
    }
}
