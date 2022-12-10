using DoctorType.Domain.Entites.Adevrtisement;
using DoctorType.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.UserPanel.Dashboard
{
    public class UserPanelDashboardViewModel
    {
        #region properties

        public User User { get; set; }

        public int CountOfYourAdvertisement{ get; set; }

        public int CountOfYourWaitingAdvertisement { get; set; }

        public int CountOfSelectedAdvertisements { get; set; }

        public int CountOfYourProjects { get; set; }

        #endregion
    }
}
