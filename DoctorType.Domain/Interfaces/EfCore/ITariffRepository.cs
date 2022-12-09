using DoctorType.Domain.Entities.Tariff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Interfaces.EfCore
{
    public interface ITariffRepository
    {
        #region Admin Side 

        //Show Tariffs In Home Page 
        Task<List<Tariff>> ShowTariffInHomePage();

        //Add Tariff
        Task CreateTariff(Tariff tariff);

        //Get Tariff By Tariff Id 
        Task<Tariff?> GetTariffById(ulong tarriffId);

        //Edit Tariff Method 
        Task EditTariff(Tariff tariff);

        //Delete Tariff
        Task DeleteTariff(Tariff tariff);

        //Filter Tariff In Admin Side 
        Task<List<Tariff>> FilterTariff();

        #endregion

        #region User Panel 

        //Show List Of Tarrifs In User Panel
        Task<List<Tariff>> ShowListOfTarrifsInUserPanel();

        #endregion
    }
}
