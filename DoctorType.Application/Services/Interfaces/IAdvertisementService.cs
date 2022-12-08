using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
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

        Task<List<AdvertisementCategory>?> FilterAdsCategory();

        Task<AdvertisementCategory> GetAdsCategory(ulong Id);

        Task RecoveryAdsCategory(ulong Id);

        Task RemoveAdsCategory(ulong Id);

        Task<AdvertisementCategoryViewModel> SetAdsCAtegoryViewModel(ulong id);

        Task<List<AdvertisementCategoryViewModel>> GetAllCorrectCategory();

        Task<AdvertisementCategory> GetAdvertisementCategoryById(ulong categoryId);

        #endregion

        Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar();

        Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id);
    }
}
