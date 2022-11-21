using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Interfaces
{
    public interface IRoleService
    {
        #region Roles

        Task<CreateRoleResult> CraeteRole(CreateRoleViewModel roleViewModel, List<ulong> SelectedPermissions);
        Task<List<Role>> FilterRoles();
        Task<List<Domain.Entities.Account.Role>> GetAllRoles();
        Task AddRoleToUser(ulong UserId, List<ulong> RoleId);
        Task<List<ulong>> GetUserRoles(ulong Userid);
        Task<List<UserRole>> GetAllUserRoles(ulong userid);
        Task DeleteAllUserRole(ulong userID);
        bool IsUserHasRole(ulong UserID);
        Task<bool> CheckPermission(string permissionTitle, ulong UserID);
        Task<Domain.Entities.Account.Role> GetRoleForEdit(ulong roleId);
        Task<EditRoleResult> EditRole(Domain.Entities.Account.Role role, List<ulong> SelectedPermissions);
        Task<bool> SoftDeleteRoleByAdmin(ulong roleId);
        Task<bool> RecoveryRoleByAdmin(ulong roleId);
        Task<List<string>> GetUsersRoleEnglisTitle(ulong UserId);

        #endregion

        #region Permissions

        Task<List<Permission>> GetAllPermissions();
        Task AddPermissionsToRole(ulong roleId, List<ulong> SelectedPermissions);
        Task<List<ulong>> GetPermissionsOfRole(ulong roleId);
        Task<List<RolePermission>> GetListOfPermissionsRole(ulong roleId);
        Task<EditRoleResult> EditPermissionsRole(ulong roleId, List<ulong> SelectedPermissions);

        #endregion
    }
}
