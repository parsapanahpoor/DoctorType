using BasePaging.Domain.ViewModels.Common;
using DoctorType.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Admin.Tariff
{
    public class FilterTariffViewModel : BasePaging<DoctorType.Domain.Entities.Tariff.Tariff>
    {
        #region properties

        public string? TariffName { get; set; }

        #endregion
    }
}
