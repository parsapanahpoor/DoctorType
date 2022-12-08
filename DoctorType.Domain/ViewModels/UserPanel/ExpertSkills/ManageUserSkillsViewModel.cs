using DoctorType.Domain.Entites.Adevrtisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.UserPanel.ExpertSkills
{
    public class ManageUserSkillsViewModel
    {
        #region Properties

        public List<AdvertisementCategory> SiteCategories { get; set; }

        public List<AdvertisementCategory> UserSkills { get; set; }

        #endregion
    }
}
