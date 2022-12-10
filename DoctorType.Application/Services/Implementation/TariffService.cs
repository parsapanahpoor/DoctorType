
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.Entities.Tariff;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Admin.Tariff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Implementation
{
    public class TariffService : ITariffService
    {
        #region Ctor 

        private readonly ITariffRepository _tariff;

        private readonly IUserService _userService;

        public TariffService(ITariffRepository tariffRepository, IUserService userService)
        {
            _tariff = tariffRepository;
            _userService = userService;
        }

        #endregion

        #region Admin Side 

        //Crete Tariff
        public async Task<bool> CreateTariff(CreateTariffViewModel model)
        {
            #region Fill Entity 

            Tariff tariff = new Tariff()
            {
                tariffDuration = model.TariffDuration,
                TariffName = model.TariffName,
                TariffPrice = model.TariffPrice,
                CountOfAcceptRequest = model.CountOfAcceptRequest,
                ColorName = model.ColorName,
            };

            #endregion

            #region Add Method

            await _tariff.CreateTariff(tariff);

            #endregion

            return true;
        }

        //Get Tariff By Tariff Id 
        public async Task<Tariff?> GetTariffById(ulong tarriffId)
        {
            return await _tariff.GetTariffById(tarriffId);
        }

        //Edit Tariff 
        public async Task<bool> EditTariff(Tariff model)
        {
            #region Get Tariff By Tariff Id 

            var tariff = await _tariff.GetTariffById(model.Id);
            if (tariff == null) return false;

            #endregion

            #region Edit Properties

            tariff.TariffPrice = model.TariffPrice;
            tariff.TariffName = model.TariffName;
            tariff.tariffDuration = model.tariffDuration;
            tariff.CountOfAcceptRequest = model.CountOfAcceptRequest;
            tariff.ColorName = model.ColorName;

            #endregion

            #region Edit Method 

            await _tariff.EditTariff(tariff);

            #endregion

            return true;
        }

        //Delete Tariff
        public async Task<bool> DeleteTariff(ulong tariffId)
        {
            #region Get Tariff By Tariff Id 

            var tariff = await _tariff.GetTariffById(tariffId);
            if (tariff == null) return false;

            #endregion

            #region Edit Properties

            tariff.IsDelete = tariff.IsDelete;

            #endregion

            #region Delete Method

            await _tariff.DeleteTariff(tariff);

            #endregion

            return true;
        }

        //Filter Tariff In Admin Side 
        public async Task<List<Tariff>> FilterTariff()
        {
            return await _tariff.FilterTariff();
        }

        //Show Tariffs In Home Page 
        public async Task<List<Tariff>> ShowTariffInHomePage()
        {
            return await _tariff.ShowTariffInHomePage();
        }

        #endregion

        #region User Panel 

        //Show List Of Tarrifs In User Panel
        public async Task<List<Tariff>> ShowListOfTarrifsInUserPanel()
        {
            return await _tariff.ShowListOfTarrifsInUserPanel(); 
        }

        //Get Tariff From User
        public async Task GetTariffFromUser(ulong userId , Tariff tariff)
        {
            #region Register On Tariff

            UserSelectedTariff model = new UserSelectedTariff()
            {
                CurrentTarriff = true,
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(tariff.tariffDuration),
                Startdate = DateTime.Now,
                TariffId = tariff.Id,
                UserId = userId
            };

            //Add To The Data Base 
            await _tariff.AddUserSelectedTariffToTheDataBase(model);

            #endregion
        }

        #endregion
    }
}
