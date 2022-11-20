using Academy.Domain.Entities.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Interfaces
{
    public interface ISiteSettingRepository
    {
        #region Admin Side 

        Task<SiteSetting?> GetSiteSetting();

        Task AddSiteSetting(SiteSetting newSetting);

        Task UpdateSiteSetting(SiteSetting setting);

        #endregion

        #region Site Side

        Task<bool> IsExistSiteSetting();

        Task<int> GetSMSTimer();

        Task<string?> GetSiteAddressDomain();

        #endregion
    }
}
