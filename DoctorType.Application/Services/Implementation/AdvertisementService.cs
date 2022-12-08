using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Implementation
{
    public class AdvertisementCategoryService : IAdvertisementCategoryService
    {
        #region Ctor

        private readonly IAdvertisementCategoryRepository _adsCategory;

        public AdvertisementCategoryService(IAdvertisementCategoryRepository adsCategory)
        {
            _adsCategory = adsCategory;
        }

        #endregion

        #region Admin Side

        public async Task<CreateAdsCategoryResult> AddAdsCategory(AdvertisementCategoryViewModel Category)
        {
            if (Category.ParentId != null && Category.ParentId != 0)
            {
                var myCategory = _adsCategory.GetAdvertisementCategoryById(Category.ParentId.Value);
                if (myCategory == null) return CreateAdsCategoryResult.CategoryNotFound;
            }

            AdvertisementCategory Cat = new AdvertisementCategory
            {
                Title = Category.GroupName,
                UrlName = Category.UrlName,
                ParentId = ((Category.ParentId != null && Category.ParentId != 0) ? Category.ParentId : null),
                IsDelete = false,
                CreateDate = DateTime.Now,
            };

            await _adsCategory.AddAdsCategory(Cat);
            await _adsCategory.Savechanges();

            return CreateAdsCategoryResult.Success;
        }

        public async Task<CreateAdsCategoryResult> EditAdsCategory(AdvertisementCategoryViewModel category)
        {
            var cat = await _adsCategory.GetAdsCategory(category.CatgeoryId.Value);

            cat.Title = category.GroupName;
            cat.ParentId = cat.ParentId;
            cat.UrlName = category.UrlName;

            _adsCategory.UpdateAdsCategory(cat);
            await _adsCategory.Savechanges();

            return CreateAdsCategoryResult.Success;
        }

        public async Task<List<AdvertisementCategory>?> FilterAdsCategory()
        {
            return await _adsCategory.FilterAdsCategory();
        }

        public async Task<List<AdvertisementCategory>?> FilterChildAdsCategory(ulong parentId)
        {
            return await _adsCategory.FilterChildAdsCategory(parentId);
        }

        public async Task<AdvertisementCategory> GetAdsCategory(ulong Id)
        {
            return await _adsCategory.GetAdsCategory(Id);
        }

        public async Task RecoveryAdsCategory(ulong Id)
        {
            var cat = await _adsCategory.GetAdsCategory(Id);

            cat.IsDelete = false;
            await _adsCategory.Savechanges();
        }

        public async Task RemoveAdsCategory(ulong Id)
        {
            var cat = await _adsCategory.GetAdsCategory(Id);

            cat.IsDelete = true;
            await _adsCategory.Savechanges();
        }

        public async Task<AdvertisementCategoryViewModel> SetAdsCAtegoryViewModel(ulong id)
        {
            var Cat = await _adsCategory.GetAdsCategory(id);
            if (Cat == null) return null;
            AdvertisementCategoryViewModel model = new AdvertisementCategoryViewModel
            {
                CatgeoryId = Cat.Id,
                ParentId = Cat.ParentId,
                GroupName = Cat.Title,
                UrlName = Cat.UrlName,
            };

            return model;
        }

        public async Task<List<AdvertisementCategoryViewModel>> GetAllCorrectCategory()
        {
            return await _adsCategory.GetAllCorrectCategoryForShowInUserPanel();
        }

        public async Task<AdvertisementCategory> GetAdvertisementCategoryById(ulong categoryId)
        {
            return await _adsCategory.GetAdvertisementCategoryById(categoryId);
        }

        #endregion

        public async Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar()
        {
            return await _adsCategory.GetAdvertisementCategoriesForNavbar();
        }

        public async Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id)
        {
            return await _adsCategory.GetChildCategoriesByParentID(Id);
        }
    }
}
