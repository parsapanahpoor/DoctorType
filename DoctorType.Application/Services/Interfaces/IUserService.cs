﻿using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Account;
using DoctorType.Domain.ViewModels.Admin;
using DoctorType.Domain.ViewModels.Admin.Account;
using DoctorType.Domain.ViewModels.Site.Account;
using DoctorType.Domain.ViewModels.UserPanel.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region Authorize

        //Get User Roles 
        Task<List<string>?> GetUserRoles(ulong userId);

        //Check That Has User Fill Personal Information 
        Task<bool> CheckThatHasUserFillPersonalInformation(ulong userId);

        //Add User Role 
        Task AddUserRole(UserRole userRole);

        //Filter User In Modal
        Task<FilterUserViewModel> FilterUsersInModal(FilterUserViewModel filter);

        Task ResendActivationCodeSMS(string Mobile);

        Task<bool> IsExistUserById(ulong userId);

        Task<User?> GetUserById(ulong userId);

        Task<RegisterUserResult> RegisterUser(RegisterUserViewModel register);

        Task<bool> IsExistsUserByEmail(string email);

        Task<bool> IsExistUserByMobile(string mobile);

        Task<LoginResult> CheckUserForLogin(LoginUserViewModel login);

        Task<User?> GetUserByMobile(string mobile);

        Task<bool> AccountActivation(string emailActivationCode);

        Task<User> GetUserByEmailActivationCode(string emailActivationCode);

        Task<ResetPasswordResult> ResetUserPassword(ResetPasswordViewModel pass, string mobile);

        #endregion

        #region Site Side

        //Validate For NAtional Id 
        Task<bool> IsValidNationalIdForUserEditByAdmin(string mobile, ulong userId);

        Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeViewModel activeMobileByActivationCodeViewModel);

        Task<ForgotPasswordResult> RecoverUserPassword(ForgetPasswordViewModel forgot);

        #endregion

        #region Admin

        //Get The List Of Simple Users For Show In Data Tables In Admin Panel 
        Task<List<User>> GetTheListOfSimpleUsersForShowInDataTablesInAdminPanel();

        //Get List Of Admins and Supporters User Id For Send Notification For Death Certificate
        Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInDeathCertificate();

        //Get Death Certificate Supporters
        Task<List<User>?> GetDeathCertificateSupporters();

        //Get Home Visit Supporters
        Task<List<User>?> GetHomeVisitSupporters();

        //Get List Of Admins About Send Notification For Arrival New Nurses Inormations
        Task<List<string>?> GetListOfAdminsAboutSendNotificationForArrivalNewNursesInormations();

        //Get Online Visit Supporters
        Task<List<User>?> GetOnlineVisitSupporters();

        //Get Home Nurse Supporters
        Task<List<User>?> GetHomeNurseSupporters();

        //Get List Of Admins and Supporters User Id For Send Notification For Online Request
        Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInOnlineVisit();

        //Get List Of Admins and Supporters User Id For Send Notification For Home Visit
        Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInHomeVisit();

        //Get List Of Admins and Supporters User Id For Send Notification For Home Pharmacy
        Task<List<string>?> GetAdminsAndSupportersNotificationForSendNotificationInHomePharmacy();

        //Get List Of Admins 
        Task<List<User>?> GetListOfAdmins();

        //Get List Of Supporters
        Task<List<User>?> GetListOfSupporters();

        //Get Home Pharmacy Supporters
        Task<List<User>?> GetHomePharmacySupporters();

        //Update User 
        Task UpdateUser(User user);

        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter);

        Task<bool> ChangePasswordInAdmin(ChangePasswordInAdminViewModel passwordViewModel);

        Task<AdminEditUserInfoViewModel> FillAdminEditUserInfoViewModel(ulong userId);

        Task<bool> IsValidMobileForUserEditByAdmin(string mobile, ulong userId);

        Task<bool> IsValidEmailForUserEditByAdmin(string email, ulong userId);

        Task<AdminEditUserInfoResult> EditUserInfo(AdminEditUserInfoViewModel edit, IFormFile? UserAvatar, List<ulong> Roles;

        //Get List Of Admins About Send Notification For Arrival New Consultant Inormations
        Task<List<string>?> GetListOfAdminsAboutSendNotificationForArrivalNewConsultantInormations();

        #endregion

        #region User Panel

        Task<UserPanelEditUserInfoViewModel> FillUserPanelEditUserInfoViewModel(ulong userId);

        Task<UserPanelEditUserInfoResult> EditUserInfoInUserPanel(UserPanelEditUserInfoViewModel edit, IFormFile? UserAvatar);

        Task<ChangeUserPasswordResponse> ChangeUserPasswordAsync(ulong userId, ChangeUserPasswordViewModel model);

        //Get List Of Admins and Supporters 
        Task<List<string>?> GetAllAdminsAndSupportersNotification();

        #endregion
    }
}
