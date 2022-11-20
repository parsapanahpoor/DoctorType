﻿using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.DbContext;
using Microsoft.Extensions.Configuration;
using Kavenegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorType.Application.StaticTools;

namespace DoctorType.Application.Services.Implementation
{
    public class SMSService : ISMSService
    {
        #region Ctor

        private readonly IConfiguration _configuration;

        public SMSService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        public async Task<SendResult?> SendLookupSMS(string receptor, string token, string token2, string token3, string template)
        {
            var apikey = _configuration["kavenegar:apikey"];

            try
            {
                Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apikey);

                var result = await api.VerifyLookup(receptor, token, token2, token3, template);

                return result;
            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                await ExeptionLog.LogError(ex);
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                await ExeptionLog.LogError(ex);
            }

            return null;
        }

        public async Task<SendResult?> SendSimpleSMS(string receptor, string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_configuration["kavenegar:apikey"]);

            //var result = await api.Send("10008663", receptor, message);
            var result = await api.Send("100050004030", receptor, message);

            return result;
        }
    }
}
