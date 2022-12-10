using DoctorType.Domain.ViewModels.Admin.Dashboard;
using DoctorType.Domain.ViewModels.UserPanel.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        #region Admin Panel Side 

        //Fill Admin Panel Dashboard
        Task<AdminDashboardViewModel> FillAdminPanelDashboard();

        #endregion

        #region User Panel 

        Task<UserPanelDashboardViewModel> FillUserPanelDashboardViewModel(ulong userId);

        #endregion
    }
}
