using DoctorType.Application.Services.Interfaces;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Entities.SiteSetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        #region Ctor

        private DoctorTypeDbContext _context;

        public EmailSender(DoctorTypeDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            try
            {
                var defaultSiteEmail = await GetDefaultEmailSetting();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(defaultSiteEmail.Smtp);

                mail.From = new MailAddress(defaultSiteEmail.From, defaultSiteEmail.DisplayName);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                if (defaultSiteEmail.Port != 0)
                {
                    SmtpServer.Port = defaultSiteEmail.Port;
                    SmtpServer.EnableSsl = defaultSiteEmail.EnableSsL;
                }

                SmtpServer.Credentials = new System.Net.NetworkCredential(defaultSiteEmail.From, defaultSiteEmail.Password);
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<EmailSetting> GetDefaultEmailSetting()
        {
            return await _context.EmailSetting.FirstOrDefaultAsync(s => !s.IsDelete && s.IsDefaultEmail);
        }
    }

}
