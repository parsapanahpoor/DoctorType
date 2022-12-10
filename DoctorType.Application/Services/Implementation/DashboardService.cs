using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.DbContext;
using DoctorType.Domain.ViewModels.Admin.Dashboard;
using DoctorType.Domain.ViewModels.UserPanel.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        #region Ctor

        private readonly DoctorTypeDbContext _context;

        public DashboardService(DoctorTypeDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        //Fill Admin Panel Dashboard
        public async Task<AdminDashboardViewModel> FillAdminPanelDashboard()
        {
            AdminDashboardViewModel model = new AdminDashboardViewModel();

            #region Count Of Users

            model.CountOfUsers = await _context.Users.CountAsync(p => !p.IsDelete);

            #endregion

            #region Count Of Customers

            model.CountOfCustomers = await _context.Users.CountAsync(p => !p.IsDelete && !p.IsUserExpert);

            #endregion

            #region Count Of Experts 

            model.CountOfExperts = await _context.Users.CountAsync(p => !p.IsDelete && p.IsUserExpert);

            #endregion

            #region Count Of DisActive Users

            model.CountOfDisActiveUsers = await _context.Users.CountAsync(p => !p.IsDelete && !p.IsMobileConfirm);

            #endregion

            #region Count Of Waitig For Expert Advertisement

            model.CountOfWaitigForExpertAdvertisement = await _context.Advertisemenets.CountAsync(p => !p.IsDelete && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.WithoutRequestForWork);

            #endregion

            #region Count Of Selected From Expert Advertisement

            model.CountOfSelectedFromExpertAdvertisement = await _context.Advertisemenets.CountAsync(p => !p.IsDelete && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert);

            #endregion

            #region Lastest Advsertisement Without Expert

            model.LastestAdvsertisementWithoutExpert = await _context.Advertisemenets.Include(p=> p.User).Where(p => !p.IsDelete && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.WithoutRequestForWork)
                                                                .OrderByDescending(p=> p.CreateDate).Take(10).ToListAsync();

            #endregion

            #region Lastest Advsertisement Without Expert

            model.LastestAdvsertisementWithExpert = await _context.Advertisemenets.Include(p=> p.User).Where(p => !p.IsDelete && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert)
                                                                .OrderByDescending(p => p.CreateDate).Take(10).ToListAsync();

            #endregion

            return model;
        }

        #endregion

        #region User Panel Side 

        public async Task<UserPanelDashboardViewModel> FillUserPanelDashboardViewModel(ulong userId)
        {
            #region Get User By Id

            var user = await _context.Users.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == userId);
            if (user == null) return null;

            #endregion

            //Create Instance
            UserPanelDashboardViewModel model = new UserPanelDashboardViewModel();

            #region User 

            model.User = user;

            #endregion

            #region Count Of Your Advertisement

            model.CountOfYourAdvertisement = await _context.Advertisemenets.CountAsync(p => !p.IsDelete && p.UserId == userId);

            #endregion

            #region Count Of Your Waiting Advertisement

            model.CountOfYourWaitingAdvertisement = await _context.Advertisemenets.CountAsync(p => !p.IsDelete && p.UserId == userId && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.WithoutRequestForWork);

            #endregion

            #region Count Of Selected Advertisements

            model.CountOfSelectedAdvertisements = await _context.Advertisemenets.CountAsync(p => !p.IsDelete && p.UserId == userId && p.AdvertismenetState == Domain.Enums.Advertisement.AdvertismenetState.SelectedFromExpert);

            #endregion

            return model;
        }

        #endregion
    }
}
