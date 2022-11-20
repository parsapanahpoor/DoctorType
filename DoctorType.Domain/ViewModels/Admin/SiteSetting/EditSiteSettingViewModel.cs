using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Admin.SiteSetting
{
    public class EditSiteSettingViewModel
    {
        [Display(Name = "Copyright text")]
        public string? CopyRightText { get; set; }

        [Display(Name = "Send SMS Time")]
        public int? SendSMSTime { get; set; }

        [Display(Name = "Site Domain Address")]
        public string? SiteDomain { get; set; }

    }

    public enum EditSiteSettingResult
    {
        Success,
        Fail
    }
}
