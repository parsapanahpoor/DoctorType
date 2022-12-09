using DoctorType.Application.Extensions;
using DoctorType.Application.Generators;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Application.StaticTools;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Implementation
{
    public class AdvertisementService : IAdvertisementService
    {
        #region Ctor

        public DoctorTypeDbContext _context { get; set; }

        private readonly IUserService _userService;

        public AdvertisementService(DoctorTypeDbContext context , IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #endregion

        #region User Panel Side 

        //Fill Create Advertisement View Model
        public async Task<CreateAdvertisementUserPanelSideViewModel> FillCreateAdvertisementUserPanelSideViewModel(ulong userId)
        {
            #region Make Instance From View Model

            CreateAdvertisementUserPanelSideViewModel model = new CreateAdvertisementUserPanelSideViewModel()
            {
                UserId = userId,
                AdvertisementCategories = await _context.AdvertisementCategory.Where(p=> !p.IsDelete).ToListAsync()
            };

            #endregion

            return model;
        }

        //Create Advertisement From User Panel 
        public async Task<bool> CreateAdvertisementFromUserPanel(CreateAdvertisementUserPanelSideViewModel model)
        {
            #region Check User

            var user = await _userService.GetUserById(model.UserId);
            if (user == null) return false;

            #endregion

            #region Create Adevrtisement 

            Advertisemenet advertisement = new Advertisemenet()
            {
                Title = model.Title,
                UserId = user.Id,
                Description = model.Description,
                AdvertismenetState = Domain.Enums.Advertisement.AdvertismenetState.WithoutRequestForWork
            } ;

            #region Upload File 

            if (model.File != null)
            {
                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(model.File.FileName);
                model.File.AddFileToServer(imageName, PathTools.AdvertisementPathServer);
                advertisement.File = imageName;
            }

            #endregion

            //Add Advertisement To Data Base 
            await _context.Advertisemenets.AddAsync(advertisement);
            await _context.SaveChangesAsync(); 

            #region Selected Skills

            if (model.AdvertisementSelectedSkills != null && model.AdvertisementSelectedSkills.Any())
            {
                List<AdvertisementSelectedSkill> selectedSkills = new List<AdvertisementSelectedSkill>();

                foreach (var item in model.AdvertisementSelectedSkills)
                {
                    selectedSkills.Add(new AdvertisementSelectedSkill()
                    {
                        AdvertisementCategoryId = item,
                        AdvertisementId = advertisement.Id
                    });
                }

                await _context.AdvertisementSelectedSkills.AddRangeAsync(selectedSkills) ;
                await _context.SaveChangesAsync();
            }

            #endregion

            #endregion

            return true;
        }

        //Filter Advertisement From User Panel 
        public async Task<List<Advertisemenet>?> FilterAdvertisementFromUserPanel(ulong userId)
        {
            #region Check User

            var user = await _userService.GetUserById(userId);
            if (user == null) return null;

            #endregion

            return await _context.Advertisemenets.Where(p => !p.IsDelete && p.UserId == userId).ToListAsync(); 
        }

        //Edit Advertisement From User Panel 
        public async Task<EditAdvertisementUserPanelViewModel?> FillEditAdvertisementUserPanelViewModel(ulong advertisementId , ulong userId)
        {
            #region Get User By ID 

            var user = await _userService.GetUserById(userId);
            if (user == null) return null;

            #endregion

            #region Get Advertisement 

            var advertisement = await _context.Advertisemenets.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == advertisementId);
            if (advertisement == null) return null;
            if (advertisement.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert) return null;

            #endregion

            #region Fill Model 

            EditAdvertisementUserPanelViewModel model = new EditAdvertisementUserPanelViewModel()
            {
                AdvertisementId = advertisementId,
                Description = advertisement.Description,
                Title = advertisement.Title,
                AdvertisementSelectedSkills = await _context.AdvertisementSelectedSkills.Where(p => !p.IsDelete && p.AdvertisementId == advertisementId).Select(p => p.AdvertisementCategoryId).ToListAsync(),
                AdvertisementCategories = await _context.AdvertisementCategory.Where(p => !p.IsDelete).OrderByDescending(p => p.CreateDate).ToListAsync()
            };

            #endregion

            return model;
        }

        //Edit Advertisement From User Panel 
        public async Task<bool> EditAdvertisementFromUserPanel(EditAdvertisementUserPanelViewModel model , ulong userId)
        {
            #region Get User By ID 

            var user = await _userService.GetUserById(userId);
            if (user == null) return false;

            #endregion

            #region Get Advertisement 

            var advertisement = await _context.Advertisemenets.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == model.AdvertisementId);
            if (advertisement == null) return false;
            if (advertisement.UserId != user.Id) return false;
            if (advertisement.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert) return false;

            #endregion

            #region Update Advertisement Fields 

            advertisement.Title = model.Title;
            advertisement.Description = model.Description;

            #endregion

            #region Edit File

            if (model.File != null)
            {
                if (!string.IsNullOrEmpty(advertisement.File))
                {
                    advertisement.File.DeleteFile(PathTools.AdvertisementPathServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(model.File.FileName);
                model.File.AddFileToServer(imageName, PathTools.AdvertisementPathServer);
                advertisement.File = imageName;
            }

            #endregion

            #region Selected Skills

            if (model.AdvertisementSelectedSkills != null && model.AdvertisementSelectedSkills.Any())
            {
                #region Remove Advertisement Skills

                var removeSkills = await _context.AdvertisementSelectedSkills.Where(p => !p.IsDelete && p.AdvertisementId == advertisement.Id).ToListAsync();

                _context.AdvertisementSelectedSkills.RemoveRange(removeSkills);
                await _context.SaveChangesAsync();

                #endregion

                List<AdvertisementSelectedSkill> selectedSkills = new List<AdvertisementSelectedSkill>();

                foreach (var item in model.AdvertisementSelectedSkills)
                {
                    selectedSkills.Add(new AdvertisementSelectedSkill()
                    {
                        AdvertisementCategoryId = item,
                        AdvertisementId = advertisement.Id
                    });
                }

                await _context.AdvertisementSelectedSkills.AddRangeAsync(selectedSkills);
                await _context.SaveChangesAsync();
            }

            #endregion

            return true;
        }

        //Delete Advertisement From User Panel 
        public async Task<bool> DeleteAdvertisementFromUserPanel(ulong advertisementId , ulong userId)
        {
            #region Get User By ID 

            var user = await _userService.GetUserById(userId);
            if (user == null) return false;

            #endregion

            #region Get Advertisement 

            var advertisement = await _context.Advertisemenets.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == advertisementId);
            if (advertisement == null) return false;
            if (advertisement.UserId != user.Id) return false;
            if (advertisement.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert) return false;

            #endregion

            #region Delete Advertisement 

            advertisement.IsDelete = true;

            #endregion

            #region Remove Advertisement Skills

            var removeSkills = await _context.AdvertisementSelectedSkills.Where(p => !p.IsDelete && p.AdvertisementId == advertisement.Id).ToListAsync();

            _context.AdvertisementSelectedSkills.RemoveRange(removeSkills);
            await _context.SaveChangesAsync();

            #endregion

            return true; 
        }

        #endregion

        #region Admin Side 

        //List Of Advertisement Admin Side
        public async Task<List<Advertisemenet>?> ListOfAdvertisementAdminSide()
        {
            return await _context.Advertisemenets.Include(p => p.User).Where(p => !p.IsDelete)
                                                    .OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        #endregion
    }
}
