using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Interfaces.EfCore
{
    public interface IAdvertisementCategoryRepository
    {
        #region Admin Side

        Task<List<AdvertisementCategory>?> FilterChildAdsCategory(ulong parentId);

        Task<List<AdvertisementCategory>?> FilterAdsCategory();

        Task Savechanges();

        Task AddAdsCategory(AdvertisementCategory category);

        Task<AdvertisementCategory> GetAdsCategory(ulong id);

        void UpdateAdsCategory(AdvertisementCategory category);

        void DeleteAdsCategory(AdvertisementCategory category);

        Task<List<AdvertisementCategoryViewModel>> GetAllCorrectCategoryForShowInUserPanel();

        #endregion

        Task<AdvertisementCategory> GetAdvertisementCategoryById(ulong categoryId);

        Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar();

        Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id);
    }
}
