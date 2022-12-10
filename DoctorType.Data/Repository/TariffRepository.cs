using DoctorType.Data.DbContext;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Entities.Tariff;
using DoctorType.Domain.Interfaces.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Data.Repository
{
    public class TariffRepository : ITariffRepository
    {
        #region Ctor 

        private readonly DoctorTypeDbContext _context;

        public TariffRepository(DoctorTypeDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        //Show Tariffs In Home Page 
        public async Task<List<Tariff>> ShowTariffInHomePage()
        {
            return await _context.Tariffs.Where(p => !p.IsDelete).ToListAsync();
        }

        //Add Tariff
        public async Task CreateTariff(Tariff tariff)
        {
            await _context.Tariffs.AddAsync(tariff);
            await _context.SaveChangesAsync();
        }

        //Get Tariff By Tariff Id 
        public async Task<Tariff?> GetTariffById(ulong tarriffId)
        {
            return await _context.Tariffs.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == tarriffId);
        }

        //Edit Tariff Method 
        public async Task EditTariff(Tariff tariff)
        {
            _context.Tariffs.Update(tariff);
            await _context.SaveChangesAsync();
        }

        //Delete Tariff
        public async Task DeleteTariff(Tariff tariff)
        {
            _context.Tariffs.Remove(tariff);
            await _context.SaveChangesAsync();
        }

        //Filter Tariff In Admin Side 
        public async Task<List<Tariff>> FilterTariff()
        {
            return await _context.Tariffs
                .Where(a => !a.IsDelete)
                .OrderByDescending(s => s.CreateDate)
                .ToListAsync();
        }

        #endregion

        #region User Panel 

        //Add User Selected Tariff To The Data Base 
        public async Task AddUserSelectedTariffToTheDataBase(UserSelectedTariff model)
        {
            await _context.UserSelectedTariffs.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        //Show List Of Tarrifs In User Panel
        public async Task<List<Tariff>> ShowListOfTarrifsInUserPanel()
        {
            return await _context.Tariffs.Where(p => !p.IsDelete)
                                .OrderBy(p=> p.tariffDuration).ToListAsync();
        }

        #endregion
    }
}
