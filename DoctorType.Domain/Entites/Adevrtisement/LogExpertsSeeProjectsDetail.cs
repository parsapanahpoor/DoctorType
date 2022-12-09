using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entites.Adevrtisement
{
    public class LogExpertsSeeProjectsDetail : BaseEntity
    {
        #region properties

        public ulong AdvertisementId { get; set; }

        public ulong UserId { get; set; }

        #endregion

        #region relation 

        public User User { get; set; }

        public Advertisemenet Advertisement { get; set; }

        #endregion
    }
}
