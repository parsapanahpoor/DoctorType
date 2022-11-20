using Academy.Domain.Entities.SiteSetting;
using DoctorType.Domain.Entities;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Entities.SiteSetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Data.DbContext
{
    public class DoctorTypeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Ctor

        public DoctorTypeDbContext(DbContextOptions<DoctorTypeDbContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets

        #region Account 

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Site Setting 

        public DbSet<SiteSetting> SiteSettings { get; set; }

        public DbSet<EmailSetting> EmailSetting { get; set; }

        #endregion

        #endregion

        #region On Model Creating

        public string culture = CultureInfo.CurrentCulture.Name;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #region Role Seed Data

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Title = "Admin",
                RoleUniqueName = "Admin",
                CreateDate = DateTime.Now,
                IsDelete = false,
                ParentId = null
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
