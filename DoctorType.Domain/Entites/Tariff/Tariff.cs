using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entities.Tariff
{
    public class Tariff : BaseEntity
    {
        #region properties

        public string TariffName { get; set; }

        public int TariffPrice { get; set; }

        public int CountOfAcceptRequest { get; set; }

        public int tariffDuration { get; set; }

        #endregion

        #region relations

        public ICollection<UserSelectedTariff> UserSelectedTariff { get; set; }

        #endregion
    }
}
