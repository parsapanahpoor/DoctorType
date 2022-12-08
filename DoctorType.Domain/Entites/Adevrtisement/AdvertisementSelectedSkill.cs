using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entites.Adevrtisement
{
    public class AdvertisementSelectedSkill : BaseEntity
    {
        #region properties

        public ulong AdvertisementId { get; set; }

        public ulong AdvertisementCategoryId { get; set; }

        #endregion

        #region relation 

        public Advertisemenet Advertisement { get; set; }

        public AdvertisementCategory AdvertisementCategory { get; set; }

        #endregion
    }
}
