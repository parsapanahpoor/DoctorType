using DoctorType.Domain.Entites.Adevrtisement;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Admin.Dashboard
{
    public class AdminDashboardViewModel
    {
        #region properties

        public int CountOfUsers { get; set; }

        public int CountOfCustomers { get; set; }

        public int CountOfExperts { get; set; }

        public int CountOfDisActiveUsers { get; set; }

        public int CountOfWaitigForExpertAdvertisement { get; set; }

        public int CountOfSelectedFromExpertAdvertisement { get; set; }

        public List<Advertisemenet> LastestAdvsertisementWithoutExpert { get; set; }

        public List<Advertisemenet> LastestAdvsertisementWithExpert { get; set; }

        #endregion
    }
}
