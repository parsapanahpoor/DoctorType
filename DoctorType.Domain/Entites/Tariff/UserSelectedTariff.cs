using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entities.Tariff
{
    public class UserSelectedTariff : BaseEntity
    {
        #region properties

        public ulong TariffId { get; set; }

        public ulong UserId { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime EndDate { get; set; }

        public bool CurrentTarriff { get; set; }

        #endregion

        #region realtion

        public User User { get; set; }

        public Tariff Tariff { get; set; }

        #endregion
    }
}
