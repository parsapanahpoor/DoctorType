using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Advertisement.AdvertisementCategory;
using DoctorType.Domain.ViewModels.UserPanel.ExpertSkills;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
        private readonly DoctorTypeDbContext _context;
        private readonly IUserService _userService;

        public AdvertisementCategoryService(IAdvertisementCategoryRepository adsCategory, DoctorTypeDbContext context, IUserService userService)
        {
            _adsCategory = adsCategory;
            _context = context;
            _userService = userService;
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

        #region User Panle 

        public async Task<AdvertisementCategoriesForNavbar> GetAdvertisementCategoriesForNavbar()
        {
            return await _adsCategory.GetAdvertisementCategoriesForNavbar();
        }

        public async Task<List<AdvertisementCategory>> GetChildCategoriesByParentID(ulong Id)
        {
            return await _adsCategory.GetChildCategoriesByParentID(Id);
        }

        #region Expert Skills 

        public async Task<ManageUserSkillsViewModel?> FillManageUserSkillsViewModel(ulong userId)
        {
            #region Get User 

            var user = await _userService.GetUserById(userId);
            if (user == null) return null;

            #endregion

            #region Fill Model 

            ManageUserSkillsViewModel model = new ManageUserSkillsViewModel()
            {
                SiteCategories = await _context.AdvertisementCategory.Where(p => !p.IsDelete && p.ParentId == null).OrderByDescending(p => p.CreateDate).ToListAsync(),
                UserSkills = await _context.ExpertsSelectedSkils.Include(p => p.AdvertisementCategory).Where(p => !p.IsDelete && p.UserId == userId).Select(p => p.AdvertisementCategory).ToListAsync()
            } ;

            #endregion

            return model;
        }

        //Add Skill To The User Skills
        public async Task<bool> AddSkillToTheUserSkills(ulong skillId, ulong userId)
        {
            #region Get Skill

            var skill = await _context.AdvertisementCategory.FirstOrDefaultAsync(p => !p.IsDelete);
            if(skill == null) return false;

            #endregion

            #region Get User 

            var user = await _userService.GetUserById(userId) ;
            if (user == null) return false;

            #endregion

            #region Check User Selected Skills 

            var isExist = await _context.ExpertsSelectedSkils.AnyAsync(p => !p.IsDelete && p.UserId == user.Id && p.AdvertisementCategoryId == skillId);
            if (isExist) return false;

            #endregion

            #region Add Skill To The User Skills 

            await _context.ExpertsSelectedSkils.AddAsync(new ExpertsSelectedSkils()
            {
                AdvertisementCategoryId = skillId,
                UserId = userId
            });

            await _context.SaveChangesAsync();

            #endregion

            return true; 
        }

        //Remove Skill From User Skills
        public async Task<bool> RemoveSkillFromUserSkills(ulong skillId, ulong userId)
        {
            #region Get Skill

            var skill = await _context.AdvertisementCategory.FirstOrDefaultAsync(p => !p.IsDelete);
            if (skill == null) return false;

            #endregion

            #region Get User 

            var user = await _userService.GetUserById(userId);
            if (user == null) return false;

            #endregion

            #region Check User Selected Skills 

            var slectedSkill = await _context.ExpertsSelectedSkils.FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == user.Id && p.AdvertisementCategoryId == skillId);
            if (slectedSkill == null) return false;

            #endregion

            #region Add Skill To The User Skills 

            _context.ExpertsSelectedSkils.Remove(slectedSkill) ;
            await _context.SaveChangesAsync();

            #endregion

            return true;
        }

        #endregion

        #endregion
    }
}
