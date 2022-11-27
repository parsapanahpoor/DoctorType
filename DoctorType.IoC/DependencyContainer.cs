using DoctorType.Application.Services.Implementation;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.Repository;
using DoctorType.Domain.Interfaces;
using DoctorType.Domain.Interfaces.EfCore;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IEmailSender, EmailSender>(); services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITariffService, TariffService>();

            #endregion

            #region EF Core Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();

            #endregion
        }
    }
}
