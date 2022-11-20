using Academy.Domain.Entities.SiteSetting;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Interfaces;
using DoctorType.Domain.ViewModels.Admin.SiteSetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Data.Repository
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        #region Ctor

        private readonly DoctorTypeDbContext _context;

        public SiteSettingRepository(DoctorTypeDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side

        public async Task<SiteSetting?> GetSiteSetting()
        {
            return await _context.SiteSettings.FirstOrDefaultAsync();
        }

        public async Task AddSiteSetting(SiteSetting newSetting)
        {
            await _context.SiteSettings.AddAsync(newSetting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteSetting(SiteSetting setting)
        {
            _context.SiteSettings.Update(setting);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Site Side 

        public async Task<bool> IsExistSiteSetting()
        {
            return await _context.SiteSettings.AnyAsync();
        }

        public async Task<int> GetSMSTimer()
        {
            return await _context.SiteSettings.Select(p => p.SendSMSTimer).FirstOrDefaultAsync();
        }

        public async Task<string?> GetSiteAddressDomain()
        {
            return await _context.SiteSettings.Select(p => p.SiteDomain).FirstOrDefaultAsync();
        }

        #endregion
    }
}
