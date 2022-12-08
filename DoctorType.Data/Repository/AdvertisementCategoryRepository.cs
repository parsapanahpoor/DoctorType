using DoctorType.Data.DbContext;
using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using DoctorType.Domain.ViewModels.Common.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Data.Repository
{
    public class AdvertisementCategoryRepository : IAdvertisementCategoryRepository
    {
        #region Ctor

        private readonly DoctorTypeDbContext _context;

        public AdvertisementCategoryRepository(DoctorTypeDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side

        public async Task<List<AdvertisementCategory>?> FilterAdsCategory()
        {
            return await _context.AdvertisementCategory.Where(p => !p.IsDelete && p.ParentId == null)
                            .OrderByDescending(p=> p.CreateDate).ToListAsync();
        }

        public async Task<List<AdvertisementCategory>?> FilterChildAdsCategory(ulong parentId)
        {
            return await _context.AdvertisementCategory.Where(p => !p.IsDelete && p.ParentId == parentId).ToListAsync();
        }

        public async Task Savechanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddAdsCategory(AdvertisementCategory category)
        {
            await _context.AdvertisementCategory.AddAsync(category);
        }

        public async Task<AdvertisementCategory> GetAdsCategory(ulong id)
        {
            return await _context.AdvertisementCategory.FindAsync(id);
        }

        public void UpdateAdsCategory(AdvertisementCategory category)
        {
            _context.AdvertisementCategory.Update(category);
        }

        public void DeleteAdsCategory(AdvertisementCategory category)
        {
            _context.AdvertisementCategory.Remove(category);
        }

        public async Task<List<AdvertisementCategoryViewModel>> GetAllCorrectCategoryForShowInUserPanel()
        {
            return await _context.AdvertisementCategory.Where(p => p.IsDelete == false)
                .Select(p => new AdvertisementCategoryViewModel()
                {
                    GroupName = p.Title,
                    UrlName = p.UrlName,
                    ParentId = p.ParentId,
                    CatgeoryId = p.Id
                }).ToListAsync();
        }

        #endregion

        public async Task<AdvertisementCategory> GetAdvertisementCategoryById(ulong categoryId)
        {
            return await _context.AdvertisementCategory.FindAsync(categoryId);
        }

        public async Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar()
        {
            var categories = await _context.AdvertisementCategory.Where(s => !s.IsDelete && s.ParentId == null)
                .ToListAsync();
            return new AdvertisementCategoriesForNavbar
            {
                AdvertisementCategories = categories
            };
        }

        public async Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id)
        {
            return await _context.AdvertisementCategory.Where(p => p.ParentId == Id && p.IsDelete == false).ToListAsync();
        }
    }
}
