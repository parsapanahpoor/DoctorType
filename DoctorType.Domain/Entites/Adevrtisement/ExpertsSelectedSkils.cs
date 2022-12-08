using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DoctorType.Domain.Entities.Account;

namespace DoctorType.Domain.Entites.Adevrtisement
{
    public class ExpertsSelectedSkils : BaseEntity
    {
        #region Properties

        public ulong AdvertisementCategoryId { get; set; }

        public ulong UserId { get; set; }

        #endregion

        #region Relations

        public AdvertisementCategory AdvertisementCategory { get; set; }

        public User User { get; set; }

        #endregion
    }
}
