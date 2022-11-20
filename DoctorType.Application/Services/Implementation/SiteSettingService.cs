using DoctorType.Domain.Entities.SiteSetting;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Interfaces;
using DoctorType.Domain.ViewModels.Admin.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorType.Domain.Interfaces;

namespace DoctorType.Application.Services.Implementation
{
    public class SiteSettingService : ISiteSettingService
    {
        #region Ctor

        private readonly ISiteSettingRepository _siteSettingRepository;

        public SiteSettingService(ISiteSettingRepository siteSettingRepository)
        {
            _siteSettingRepository = siteSettingRepository;
        }

        #endregion

        #region Admin 

        public async Task<EditSiteSettingViewModel> FillEditSiteSettingViewModel()
        {
            #region Get Site Setting

            var setting = await _siteSettingRepository.GetSiteSetting();

            #endregion

            #region Fill View Model

            var result = new EditSiteSettingViewModel();

            if (setting != null)
            {
                result = new EditSiteSettingViewModel()
                {
                    CopyRightText = setting.CopyRightText,
                    SendSMSTime = setting.SendSMSTimer,
                    SiteDomain = setting.SiteDomain,
                };
            }

            #endregion

            return result;
        }

        public async Task<EditSiteSettingResult> EditSiteSetting(EditSiteSettingViewModel editSiteSettingViewModel)
        {
            #region Get Site Setting

            var setting = await _siteSettingRepository.GetSiteSetting();

            #endregion

            #region Model State validation

            if (string.IsNullOrEmpty(editSiteSettingViewModel.CopyRightText))
            {
                return EditSiteSettingResult.Fail;
            }

            if (editSiteSettingViewModel.SendSMSTime == null)
            {
                return EditSiteSettingResult.Fail;
            }

            if (editSiteSettingViewModel.SiteDomain == null)
            {
                return EditSiteSettingResult.Fail;
            }

            #endregion

            if (setting == null)
            {
                var createResult = await CreateSiteSetting(editSiteSettingViewModel);
                return createResult;
            }
            else
            {
                setting.CopyRightText = editSiteSettingViewModel.CopyRightText;
                setting.SendSMSTimer = editSiteSettingViewModel.SendSMSTime.Value;
                setting.SiteDomain = editSiteSettingViewModel.SiteDomain;
            }

            await _siteSettingRepository.UpdateSiteSetting(setting);

            return EditSiteSettingResult.Success;
        }

        public async Task<EditSiteSettingResult> CreateSiteSetting(EditSiteSettingViewModel editSiteSettingViewModel)
        {
            #region Model State validation

            if (string.IsNullOrEmpty(editSiteSettingViewModel.CopyRightText))
            {
                return EditSiteSettingResult.Fail;
            }

            if (editSiteSettingViewModel.SendSMSTime == null)
            {
                return EditSiteSettingResult.Fail;
            }

            if (editSiteSettingViewModel.SiteDomain == null)
            {
                return EditSiteSettingResult.Fail;
            }

            #endregion

            var newSetting = new SiteSetting()
            {
                CopyRightText = editSiteSettingViewModel.CopyRightText,
                SendSMSTimer = editSiteSettingViewModel.SendSMSTime.Value,
                SiteDomain = editSiteSettingViewModel.SiteDomain,
            };

            await _siteSettingRepository.AddSiteSetting(newSetting);

            return EditSiteSettingResult.Success;
        }

        #endregion

        #region Site Side

        public async Task<bool> IsExistSiteSetting()
        {
            return await _siteSettingRepository.IsExistSiteSetting();
        }

        public async Task<int> GetSMSTimer()
        {
            return await _siteSettingRepository.GetSMSTimer();
        }

        public async Task<string?> GetSiteAddressDomain()
        {
            return await _siteSettingRepository.GetSiteAddressDomain();
        }

        #endregion
    }
}
