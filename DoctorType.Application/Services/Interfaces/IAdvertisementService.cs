using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using DoctorType.Domain.ViewModels.UserPanel.ExpertSkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IAdvertisementCategoryService
    {
        #region Admin Side

        Task<List<AdvertisementCategory>?> FilterChildAdsCategory(ulong parentId);

        Task<CreateAdsCategoryResult> AddAdsCategory(AdvertisementCategoryViewModel Category);

        Task<CreateAdsCategoryResult> EditAdsCategory(AdvertisementCategoryViewModel category);

        Task<List<AdvertisementCategory>?> FilterAdsCategory();

        Task<AdvertisementCategory> GetAdsCategory(ulong Id);

        Task RecoveryAdsCategory(ulong Id);

        Task RemoveAdsCategory(ulong Id);

        Task<AdvertisementCategoryViewModel> SetAdsCAtegoryViewModel(ulong id);

        Task<List<AdvertisementCategoryViewModel>> GetAllCorrectCategory();

        Task<AdvertisementCategory> GetAdvertisementCategoryById(ulong categoryId);

        #endregion

        #region User Panel 

        Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar();

        Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id);

        #region Expert Skills 

        Task<ManageUserSkillsViewModel?> FillManageUserSkillsViewModel(ulong userId);

        //Add Skill To The User Skills
        Task<bool> AddSkillToTheUserSkills(ulong skillId, ulong userId);

        //Remove Skill From User Skills
        Task<bool> RemoveSkillFromUserSkills(ulong skillId, ulong userId);

        #endregion

        #endregion
    }
}
