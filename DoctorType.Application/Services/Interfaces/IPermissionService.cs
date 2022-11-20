using DoctorFAM.Domain.ViewModels.Common;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Check Permission

        Task<bool> HasUserPermission(ulong userId, string permissionName);

        #endregion

        #region Role

        //Check Is User Master Of Laboratory 
        Task<bool> CheckIsUserMasterOfLaboratory(ulong userId);

        //Check Is User Has Permission To Consultant Panel 
        Task<bool> IsUserConsultant(ulong userId);

        Task<List<Role>> GetListOfRoles();

        Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter);

        Task<bool> CreateRole(CreateRoleViewModel create);

        Task<bool> IsRoleNameValid(string name, ulong roleId);

        Task<EditRoleViewModel> FillEditRoleViewModel(ulong roleId);

        Task<Role?> GetRoleById(ulong roleId);

        Task<List<SelectListViewModel>> GetSelectRolesList();

        Task<EditRoleResult> EditRole(EditRoleViewModel edit);

        Task<bool> DeleteRole(ulong roleId);

        Task<GetUserRoles> GetUserRole(ulong userId);

        Task<bool> IsUserAdmin(ulong userId);

        Task<bool> IsUserDoctor(ulong userId);

        Task<bool> IsUserDoctorOrDoctorEmployee(ulong userId);

        Task<bool> IsUserPharmacy(ulong userId);

        Task<bool> IsUserSupporter(ulong userId);

        //Check Is User Has Permission To Nurse Panel 
        Task<bool> IsUserNurse(ulong userId);

        //Check Is User Has Permission To Laboratory Panel 
        Task<bool> IsUserLaboratory(ulong userId);

        //Get List Of Laboratory Roles
        Task<List<Role>> GetListOfLaboratoryRoles();

        Task<List<string>?> GetUserRoleses(ulong userId);

        #endregion
    }
}
