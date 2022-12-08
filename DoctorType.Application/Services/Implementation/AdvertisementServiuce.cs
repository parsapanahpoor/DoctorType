using DoctorType.Application.Extensions;
using DoctorType.Application.Generators;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Application.StaticTools;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.UserPanel.Advertisement;
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

            #region Is Image 

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

        #endregion

        #region Admin Side 



        #endregion
    }
}
