﻿using AngleSharp.Css;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Domain.Interfaces;
using DoctorType.Application.Convertors;
using DoctorType.Application.Extensions;
using DoctorType.Application.Generators;
using DoctorType.Application.Security;
using DoctorType.Application.Services.Interfaces;
using DoctorType.Application.StaticTools;
using DoctorType.Application.Utils;
using DoctorType.Data.DbContext;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Interfaces;
using DoctorType.Domain.ViewModels.Account;
using DoctorType.Domain.ViewModels.Admin;
using DoctorType.Domain.ViewModels.Admin.Account;
using DoctorType.Domain.ViewModels.Common;
using DoctorType.Domain.ViewModels.Site.Account;
using DoctorType.Domain.ViewModels.UserPanel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RegisterUser.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DoctorType.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Ctor

        private readonly DoctorTypeDbContext _context;

        private readonly IViewRenderService _viewRenderService;

        private readonly IEmailSender _emailSender;

        private readonly IUserRepository _userRepository;

        private readonly ISMSService _smsservice;

        private static readonly HttpClient client = new HttpClient();

        public UserService(DoctorTypeDbContext context, IViewRenderService viewRenderService,
                                IEmailSender emailSender, IUserRepository userRepository, ISMSService smsservice)
        {
            _context = context;
            _viewRenderService = viewRenderService;
            _emailSender = emailSender;
            _userRepository = userRepository;
            _smsservice = smsservice;
        }

        #endregion

        #region Authorize

        //Get User Roles 
        public async Task<List<string>?> GetUserRoles(ulong userId)
        {
            return await _userRepository.GetUserRoles(userId);
        }

        //Check That Has User Fill Personal Information 
        public async Task<bool> CheckThatHasUserFillPersonalInformation(ulong userId)
        {
            #region Get User By User Id

            var user = await GetUserById(userId);
            if (user == null) return false;

            #endregion

            #region Check User Info

            if (string.IsNullOrEmpty(user.NationalId)
                || string.IsNullOrEmpty(user.Mobile)
                || string.IsNullOrEmpty(user.FirstName)
                || string.IsNullOrEmpty(user.LastName)
                || string.IsNullOrEmpty(user.Username))
            {
                return false;
            }

            #endregion

            return true;
        }

        //Add User Role 
        public async Task AddUserRole(UserRole userRole)
        {
            await _userRepository.AddUserRole(userRole);
        }

        public async Task ResendActivationCodeSMS(string Mobile)
        {
            var user = await _userRepository.GetUserByMobile(Mobile);
            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();
            user.ExpireMobileSMSDateTime = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            #region Send Verification Code SMS

            var result = $"https://api.kavenegar.com/v1/564672526D58694D3477685571796F7372574F576C476B6366785462356D3164683370395A2B61356D6E383D/verify/lookup.json?receptor={user.Mobile}&token={user.MobileActivationCode}&template=Register";
            var results = client.GetStringAsync(result);

            var message = Messages.SendActivationRegisterSms(user.MobileActivationCode);

            await _smsservice.SendSimpleSMS(user.Mobile, message);

            #endregion

        }

        public async Task<bool> IsExistUserById(ulong userId)
        {
            return await _userRepository.IsExistUserById(userId);
        }

        public async Task<User?> GetUserByMobile(string mobile)
        {
            return await _context.Users.FirstOrDefaultAsync(s =>
                s.Mobile == mobile.Trim().ToLower() && !s.IsDelete);
        }

        public async Task<LoginResult> CheckUserForLogin(LoginUserViewModel login)
        {
            // get email and password
            var password = PasswordHasher.EncodePasswordMd5(login.Password.SanitizeText());
            var mobile = login.Mobile.Trim().ToLower().SanitizeText();

            // get user by email
            var user = await GetUserByMobile(mobile);

            // check user exists
            if (user == null) return LoginResult.UserNotFound;

            // check user password
            if (!user.Password.Equals(password)) return LoginResult.UserNotFound;

            // check user ban status
            if (user.IsBan) return LoginResult.UserIsBan;

            // check user activation
            if (!user.IsMobileConfirm) return LoginResult.MobileNotActivated;

            return LoginResult.Success;
        }

        public async Task<bool> IsExistsUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(s => s.Email == email.Trim().ToLower() && !s.IsDelete);
        }

        public async Task<bool> IsExistUserByMobile(string mobile)
        {
            return await _context.Users.AnyAsync(p => p.Mobile == mobile && !p.IsDelete);
        }

        public async Task<User?> GetUserById(ulong userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(s => s.Id == userId && !s.IsDelete);
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserViewModel register)
        {
            //Fix Email Format
            var mobile = register.Mobile.Trim().ToLower().SanitizeText();

            //Check Email Address
            if (await IsExistsUserByEmail(register.Mobile))
            {
                return RegisterUserResult.MobileExist;
            }

            //Check Mobile Number
            if (await IsExistUserByMobile(register.Mobile))
            {
                return RegisterUserResult.MobileExist;
            }

            //Field about Accept Site Roles
            if (register.SiteRoles == false)
            {
                return RegisterUserResult.SiteRoleNotAccept;
            }

            //Hash Password
            var password = PasswordHasher.EncodePasswordMd5(register.Password.SanitizeText());

            //Create User
            var User = new DoctorType.Domain.Entities.Account.User()
            {
                //Email = email,
                Password = password,
                Username = mobile,
                Mobile = register.Mobile.SanitizeText(),
                EmailActivationCode = CodeGenerator.GenerateUniqCode(),
                MobileActivationCode = new Random().Next(10000, 999999).ToString(),
                ExpireMobileSMSDateTime = DateTime.Now
            };

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();

            #region Send Email


            //var emailViewModel = new EmailViewModel
            //{
            //    EmailActivationCode = User.EmailActivationCode,
            //    ButtonName = "فعالسازی حساب کاربری",
            //    FullName = User.Username,
            //    Description = $"{User.Username} عزیز لطفا جهت فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید .",
            //    ButtonLink = $"{PathTools.SiteAddress}/Activate-Account/{User.EmailActivationCode}",
            //    EmailBanner = string.Empty,
            //};

            #endregion

            #region Send Verification Code SMS

            //var result = $"https://api.kavenegar.com/v1/564672526D58694D3477685571796F7372574F576C476B6366785462356D3164683370395A2B61356D6E383D/verify/lookup.json?receptor={User.Mobile}&token={User.MobileActivationCode}&template=Register";
            //var results = client.GetStringAsync(result);

            //var message = Messages.SendActivationRegisterSms(User.MobileActivationCode);

            //await _smsservice.SendSimpleSMS(User.Mobile, message);

            #endregion

            return RegisterUserResult.Success;

        }

        public async Task<bool> AccountActivation(string emailActivationCode)
        {
            // get user by email activation code
            var user = await GetUserByEmailActivationCode(emailActivationCode);

            // check user exists
            if (user == null) return false;

            // update user
            user.IsEmailConfirm = true;
            user.EmailActivationCode = CodeGenerator.GenerateUniqCode();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByEmailActivationCode(string emailActivationCode)
        {
            return await _context.Users.FirstOrDefaultAsync(s =>
                s.EmailActivationCode == emailActivationCode.SanitizeText());
        }

        public async Task<ResetPasswordResult> ResetUserPassword(ResetPasswordViewModel pass, string mobile)
        {
            #region Get User By Mobile

            var user = await GetUserByMobile(mobile);
            if (user == null) return ResetPasswordResult.NotFound;

            if (user.MobileActivationCode != pass.ActiveCode) return ResetPasswordResult.WrongActiveCode;

            #endregion

            #region Update User Info

            user.Password = PasswordHasher.EncodePasswordMd5(pass.Password.SanitizeText());
            user.IsMobileConfirm = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            #endregion

            return ResetPasswordResult.Success;
        }

        #endregion

        #region Site Side

        public async Task<ForgotPasswordResult> RecoverUserPassword(ForgetPasswordViewModel forgot)
        {
            #region Get User By Mobile

            var user = await GetUserByMobile(forgot.Mobile);
            if (user == null) return ForgotPasswordResult.NotFound;

            if (user != null && user.IsBan)
            {
                return ForgotPasswordResult.UserIsBlocked;
            }

            #endregion

            #region Update User Info

            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();
            user.ExpireMobileSMSDateTime = DateTime.Now;

            _context.Users.Update(user);

            var smsViewModel = new SendVerificationSmsViewModel()
            {
                Receptor = user.Mobile,
                Token = user.MobileActivationCode
            };

            await _context.SaveChangesAsync();

            #endregion

            #region Send Verification Code SMS

            var message = Messages.SendActivationRegisterSms(user.MobileActivationCode);

            await _smsservice.SendSimpleSMS(user.Mobile, message);

            #endregion

            return ForgotPasswordResult.Success;
        }

        //Get List Of Admins About Send Notification For Arrival New Consultant Inormations
        public async Task<List<string>?> GetListOfAdminsAboutSendNotificationForArrivalNewConsultantInormations()
        {
            return await _userRepository.GetListOfAdminsAboutSendNotificationForArrivalNewConsultantInormations();
        }

        public async Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeViewModel activeMobileByActivationCodeViewModel)
        {
            #region Get User By Mobile

            if (!await IsExistUserByMobile(activeMobileByActivationCodeViewModel.Mobile.SanitizeText()))
            {
                return ActiveMobileByActivationCodeResult.AccountNotFound;
            }

            var user = await GetUserByMobile(activeMobileByActivationCodeViewModel.Mobile.SanitizeText());
            if (user == null) return ActiveMobileByActivationCodeResult.AccountNotFound;

            #endregion

            #region Validation Activation Code

            if (user.MobileActivationCode != activeMobileByActivationCodeViewModel.MobileActiveCode)
            {
                return ActiveMobileByActivationCodeResult.AccountNotFound;
            }

            #endregion

            #region Update User 

            user.IsMobileConfirm = true;
            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();

            await _context.SaveChangesAsync();

            #endregion

            return ActiveMobileByActivationCodeResult.Success;
        }

        #endregion

        #region Admin

        //Get List Of Admins About Send Notification For Arrival New Nurses Inormations
        public async Task<List<string>?> GetListOfAdminsAboutSendNotificationForArrivalNewNursesInormations()
        {
            return await _userRepository.GetListOfAdminsAboutSendNotificationForArrivalNewNursesInormations();
        }

        //Get List Of Admins and Supporters User Id For Send Notification  For Home Pharmacy
        public async Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInHomePharmacy()
        {
            return await _userRepository.GetAdminsAndSupportersNotificationForSendNotificationInHomePharmacy();
        }

        //Get List Of Admins and Supporters User Id For Send Notification For Online Request
        public async Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInOnlineVisit()
        {
            return await _userRepository.GetAdminsAndSupportersNotificationForSendNotificationInOnlineVisit();
        }

        //Get List Of Admins and Supporters User Id For Send Notification For Home Visit
        public async Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInHomeVisit()
        {
            return await _userRepository.GetAdminsAndSupportersNotificationForSendNotificationInHomeVisit();
        }

        //Get List Of Admins and Supporters User Id For Send Notification For Death Certificate
        public async Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInDeathCertificate()
        {
            return await _userRepository.GetAdminsAndSupportersNotificationForSendNotificationInDeathCertificate();
        }

        //Get List Of Admins and Supporters 
        public async Task<List<string>?> GetAllAdminsAndSupportersNotification()
        {
            return await _userRepository.GetAllAdminsAndSupportersNotification();
        }

        //Get List Of Admins 
        public async Task<List<User>?> GetListOfAdmins()
        {
            return await _userRepository.GetListOfAdmins();
        }

        //Get List Of Supporters
        public async Task<List<User>?> GetListOfSupporters()
        {
            return await _userRepository.GetListOfSupporters();
        }

        //Get Home Pharmacy Supporters
        public async Task<List<User>?> GetHomePharmacySupporters()
        {
            return await _userRepository.GetHomePharmacySupporters();
        }

        //Get Home Visit Supporters
        public async Task<List<User>?> GetHomeVisitSupporters()
        {
            return await _userRepository.GetHomeVisitSupporters();
        }

        //Get Death Certificate Supporters
        public async Task<List<User>?> GetDeathCertificateSupporters()
        {
            return await _userRepository.GetDeathCertificateSupporters();
        }

        //Get Online Visit Supporters
        public async Task<List<User>?> GetOnlineVisitSupporters()
        {
            return await _userRepository.GetOnlineVisitSupporters();
        }

        //Get Home Nurse Supporters
        public async Task<List<User>?> GetHomeNurseSupporters()
        {
            return await _userRepository.GetHomeNurseSupporters();
        }

        //Update User 
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        //Filter User In Modal
        public async Task<FilterUserViewModel> FilterUsersInModal(FilterUserViewModel filter)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .OrderByDescending(p => p.CreateDate)
                .AsQueryable();

            #region order

           

            #endregion

            #region filter

            if ((!string.IsNullOrEmpty(filter.Email)))
            {
                query = query.Where(u => u.Email.Contains(filter.Email));
            }
            if ((!string.IsNullOrEmpty(filter.Mobile)))
            {
                query = query.Where(u => u.Mobile.Contains(filter.Mobile));
            }
            if ((!string.IsNullOrEmpty(filter.Username)))
            {
                query = query.Where(u => u.Username.Contains(filter.Username));
            }

            #endregion

            #region paging

            await filter.Paging(query);

            #endregion

            return filter;
        }

        public async Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter)
        {
            var query = _context.Users
                .Where(s => !s.IsDelete)
                .OrderByDescending(s => s.CreateDate)
                .AsQueryable();

            #region Status

            switch (filter.Status)
            {
                case UserStatus.All:
                    break;
                case UserStatus.EmailConfirmed:
                    query = query.Where(s => s.IsEmailConfirm);
                    break;
                case UserStatus.EmailNotConfirmed:
                    query = query.Where(s => !s.IsEmailConfirm);
                    break;
                case UserStatus.MobileConfirmed:
                    query = query.Where(s => s.IsMobileConfirm);
                    break;
                case UserStatus.MobileNotConfirmed:
                    query = query.Where(s => !s.IsMobileConfirm);
                    break;
                case UserStatus.BanForComment:
                    query = query.Where(s => s.BanForComment);
                    break;
                case UserStatus.IsBan:
                    query = query.Where(s => s.IsBan);
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.Email}%"));
            }

            if (!string.IsNullOrEmpty(filter.Mobile))
            {
                query = query.Where(s => s.Mobile != null && EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));
            }

            if (filter.RoleId != 0)
            {
                query = query.Include(s => s.UserRoles).Where(s => s.UserRoles.Select(s => s.RoleId).Contains(filter.RoleId));
            }

            //if (!string.IsNullOrEmpty(filter.FromDate))
            //{
            //    var fromDate = filter.FromDate.ToMiladiDateTime();
            //    query = query.Where(s => s.CreateDate >= fromDate);
            //}

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(s => s.Username.Contains(filter.FullName));
            }

            //if (!string.IsNullOrEmpty(filter.ToDate))
            //{
            //    var toDate = filter.ToDate.ToMiladiDateTime();
            //    query = query.Where(s => s.CreateDate <= toDate);
            //}

            if (filter.TodayRegister == true)
            {
                query = query.Where(p => !p.IsDelete && p.CreateDate.Year == DateTime.Now.Year && p.CreateDate.DayOfYear == DateTime.Now.DayOfYear);
            }

            #endregion

            await filter.Paging(query);

            return filter;
        }

        public async Task<bool> ChangePasswordInAdmin(ChangePasswordInAdminViewModel passwordViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == passwordViewModel.UserId);

            if (user == null)
            {
                return false;
            }

            user.Password = PasswordHasher.EncodePasswordMd5(passwordViewModel.Password);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AdminEditUserInfoViewModel> FillAdminEditUserInfoViewModel(ulong userId)
        {
            #region Get User By Id

            var user = await GetUserById(userId);

            if (user == null) return null;

            #endregion

            #region Get User Role

            var userRoleIds = await _context.UserRoles
               .Where(s => !s.IsDelete && s.UserId == userId)
               .Select(s => s.RoleId)
               .ToListAsync();

            #endregion

            #region Fill View Model

            AdminEditUserInfoViewModel model = new AdminEditUserInfoViewModel()
            {
                Mobile = user.Mobile,
                Email = user.Email,
                BanForComment = user.BanForComment,
                BanForTicket = user.BanForTicket,
                IsAdmin = user.IsAdmin,
                IsBan = user.IsBan,
                IsEmailConfirm = user.IsEmailConfirm,
                IsMobileConfirm = user.IsMobileConfirm,
                username = user.Username,
                UserRoles = userRoleIds,
                UserId = user.Id,
                AvatarName = user.Avatar,
                ExtraPhoneNumber = user.ExtraPhoneNumber,
                FatherName = user.FatherName,
                FirstName = user.FirstName,
                HomePhoneNumber = user.HomePhoneNumber,
                LastName = user.LastName,
                NationalId = user.NationalId,
                WorkAddress = user.WorkAddress
            };

            if (user.BithDay != null && user.BithDay.HasValue)
            {
                model.BithDay = user.BithDay.Value.ToShamsi();
            }

            if (!string.IsNullOrEmpty(user.NationalId))
            {
                model.NationalId = user.NationalId;
            }

            #endregion

            return model;
        }

        public async Task<AdminEditUserInfoResult> EditUserInfo(AdminEditUserInfoViewModel edit, IFormFile? UserAvatar)
        {
            #region Data Valdiation

            var user = await GetUserById(edit.UserId);

            if (user == null)
            {
                return AdminEditUserInfoResult.UserNotFound;
            }

            if (UserAvatar != null && !UserAvatar.IsImage())
            {
                return AdminEditUserInfoResult.NotValidImage;
            }

            if (UserAvatar != null)
            {
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    user.Avatar.DeleteImage(PathTools.UserAvatarPathServer, PathTools.UserAvatarPathThumbServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(UserAvatar.FileName);
                UserAvatar.AddImageToServer(imageName, PathTools.UserAvatarPathServer, 270, 270, PathTools.UserAvatarPathThumbServer);
                user.Avatar = imageName;
            }

            if (!string.IsNullOrEmpty(edit.Email))
            {
                if (!await IsValidEmailForUserEditByAdmin(edit.Email, user.Id))
                {
                    return AdminEditUserInfoResult.NotValidEmail;
                }
            }

            if (!string.IsNullOrEmpty(edit.Mobile) && !await IsValidMobileForUserEditByAdmin(edit.Mobile, user.Id))
            {
                return AdminEditUserInfoResult.NotValidMobile;
            }

            if (!string.IsNullOrEmpty(edit.NationalId) && !await IsValidNationalIdForUserEditByAdmin(edit.NationalId, user.Id))
            {
                return AdminEditUserInfoResult.NotValidNationalId;
            }

            #endregion

            #region Update User Field

            user.Username = edit.username.SanitizeText();
            user.Email = edit.Email;
            user.Mobile = edit.Mobile;
            user.IsMobileConfirm = edit.IsMobileConfirm;
            user.IsEmailConfirm = edit.IsEmailConfirm;
            user.IsAdmin = edit.IsAdmin;
            user.IsBan = edit.IsBan;
            user.BanForComment = edit.BanForComment;
            user.BanForTicket = edit.BanForTicket;
            user.NationalId = edit.NationalId;
            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;
            user.ExtraPhoneNumber = edit.ExtraPhoneNumber;
            user.FatherName = edit.FatherName;
            user.HomePhoneNumber = edit.HomePhoneNumber;
            user.WorkAddress = edit.WorkAddress;

            if (!string.IsNullOrEmpty(edit.BithDay))
            {
                user.BithDay = edit.BithDay.ToMiladiDateTime();
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            #endregion

            #region Delete User Rols

            var roles = await _context.UserRoles.Where(s => !s.IsDelete && s.UserId == user.Id).ToListAsync();

            _context.RemoveRange(roles);

            #endregion

            #region Check That If User Has Role Then Update other Information That Related By Role Informations 

            var userRoles = await _userRepository.GetUserRoles(user.Id);

            if (userRoles != null && userRoles.Any())
            {
             
            }

            #endregion

            #region Add User Roles

            if (edit.UserRoles != null && edit.UserRoles.Any())
            {
                foreach (var roleId in edit.UserRoles)
                {
                    var userRole = new UserRole()
                    {
                        RoleId = roleId,
                        UserId = user.Id
                    };
                    await _context.AddAsync(userRole);
                }

                await _context.SaveChangesAsync();
            }

            #endregion

            return AdminEditUserInfoResult.Success;
        }

        public async Task<bool> IsValidMobileForUserEditByAdmin(string mobile, ulong userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => !s.IsDelete && s.Mobile == mobile.Trim());

            if (user == null) return true;
            if (user.Id == userId) return true;

            return false;
        }

        //Validate For NAtional Id 
        public async Task<bool> IsValidNationalIdForUserEditByAdmin(string nationalId, ulong userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => !s.IsDelete && s.NationalId == nationalId.Trim());

            if (user == null) return true;
            if (user.Id == userId) return true;

            return false;
        }

        public async Task<bool> IsValidEmailForUserEditByAdmin(string email, ulong userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => !s.IsDelete && s.Email == email.Trim().ToLower());

            if (user == null) return true;

            if (user.Id == userId) return true;

            return false;
        }

        #endregion

        #region User Panel

        public async Task<UserPanelEditUserInfoViewModel> FillUserPanelEditUserInfoViewModel(ulong userId)
        {
            #region Get User By Id

            var user = await GetUserById(userId);

            if (user == null) return null;

            #endregion

            #region Fill View Model

            UserPanelEditUserInfoViewModel model = new UserPanelEditUserInfoViewModel()
            {
                Mobile = user.Mobile,
                Email = user.Email,
                UserId = user.Id,
                AvatarName = user.Avatar,
                username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ExtraPhoneNumber = user.ExtraPhoneNumber,
                FatherName = user.FatherName,
                HomePhoneNumber = user.HomePhoneNumber,
                WorkAddress = user.WorkAddress,
            };

            if (user.BithDay != null && user.BithDay.HasValue)
            {
                model.BithDay = user.BithDay.Value.ToShamsi();
            }

            if (!string.IsNullOrEmpty(user.NationalId))
            {
                model.NationalId = user.NationalId;
            }

            #endregion

            return model;
        }

        public async Task<UserPanelEditUserInfoResult> EditUserInfoInUserPanel(UserPanelEditUserInfoViewModel edit, IFormFile? UserAvatar)
        {
            #region Data Valdiation

            var user = await GetUserById(edit.UserId);

            if (user == null)
            {
                return UserPanelEditUserInfoResult.UserNotFound;
            }

            if (UserAvatar != null && !UserAvatar.IsImage())
            {
                return UserPanelEditUserInfoResult.NotValidImage;
            }

            if (UserAvatar != null)
            {
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    user.Avatar.DeleteImage(PathTools.UserAvatarPathServer, PathTools.UserAvatarPathThumbServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(UserAvatar.FileName);
                UserAvatar.AddImageToServer(imageName, PathTools.UserAvatarPathServer, 270, 270, PathTools.UserAvatarPathThumbServer);
                user.Avatar = imageName;
            }

            if (!string.IsNullOrEmpty(edit.Email))
            {
                if (!await IsValidEmailForUserEditByAdmin(edit.Email, user.Id))
                {
                    return UserPanelEditUserInfoResult.NotValidEmail;
                }
            }

            if (string.IsNullOrEmpty(edit.NationalId))
            {
                return UserPanelEditUserInfoResult.NationalId;
            }

            if (!string.IsNullOrEmpty(edit.NationalId) && !await IsValidNationalIdForUserEditByAdmin(edit.NationalId, user.Id))
            {
                return UserPanelEditUserInfoResult.NotValidNationalId;
            }

            #endregion

            #region Update User Field

            user.Username = edit.username.SanitizeText();
            user.Email = edit.Email.SanitizeText();
            user.FirstName = edit.FirstName.SanitizeText();
            user.LastName = edit.LastName.SanitizeText();
            user.BithDay = edit.BithDay.ToMiladiDateTime();
            user.FatherName = edit.FatherName.SanitizeText();
            user.NationalId = edit.NationalId;
            user.ExtraPhoneNumber = edit.ExtraPhoneNumber.SanitizeText();
            user.HomePhoneNumber = edit.HomePhoneNumber.SanitizeText();
            user.WorkAddress = edit.WorkAddress.SanitizeText();

            _context.Update(user);
            await _context.SaveChangesAsync();

            #endregion

            #region Check That If User Has Role Then Update other Information That Related By Role Informations 

            var userRoles = await _userRepository.GetUserRoles(user.Id);

            if (userRoles != null && userRoles.Any())
            {
            }

            #endregion

            return UserPanelEditUserInfoResult.Success;
        }

        public async Task<ChangeUserPasswordResponse> ChangeUserPasswordAsync(ulong userId, ChangeUserPasswordViewModel model)
        {
            #region Get User By Id

            var user = await GetUserById(userId);

            if (user == null) return ChangeUserPasswordResponse.UserNotFound;

            #endregion

            #region Change Password

            if (user.Password != PasswordHasher.EncodePasswordMd5(model.CurrentPassword))
            {
                return ChangeUserPasswordResponse.WrongPassword;
            }

            user.Password = PasswordHasher.EncodePasswordMd5(model.NewPassword);
            await _userRepository.EditUser(user);

            #endregion

            return ChangeUserPasswordResponse.Success;
        }

        #endregion
    }
}
