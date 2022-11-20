﻿using BusinessPortal.Application.Services.Implementation;
using DoctorFAM.Application.Services.Implementation;
using DoctorFAM.Application.Services.Interfaces;
using DoctorFAM.Data.Repository;
using DoctorFAM.Domain.Interfaces;
using DoctorType.Application.Services.Implementation;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.Repository;
using DoctorType.Domain.Interfaces;
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISMSService, SMSService>();

            #endregion

            #region EF Core Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();

            #endregion
        }
    }
}
