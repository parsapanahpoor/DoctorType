using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Interfaces.EfCore
{
    public interface IRoleRepository
    {
        #region Roles

        Task AddRoleAsync(Entities.Account.Role role);

        Task<Entities.Account.Role> GetRoleAsyncById(ulong roleId);

        void EditRole(Entities.Account.Role role);

        void SoftDeleteRole(Entities.Account.Role role);

        Task SoftDeleteRole(ulong roleId);

        void DeletePermanentRole(Entities.Account.Role role);

        Task DeletePermanentRole(ulong roleId);

        Task SaveChangesAsync();

        Task<List<Entities.Account.Role>> GetAllRoles();

        Task<bool> IsExistRoleTitle(string roleTitle);

        Task<List<Role>> FilterRoles();

        Task AddRoleToUser(UserRole user);

        Task<List<ulong>> GetUserRolesID(ulong UserId);

        void DeleteAllUserRole(List<UserRole> User);

        Task<List<UserRole>> GetAllUserRoles(ulong userid);

        bool IsUserHasRole(ulong Userid);

        Task<List<ulong>> GetAllRolesPermissionID(String PermissionTitle);

        Task<List<string>> GetUsersRoleEnglisTitle(ulong UserId);

        #endregion

        #region Permissions

        void DeletePermanentRolePermissions(List<Domain.Entities.Account.RolePermission> rolePermissions);

        Task AddRangeRolePermissions(List<RolePermission> rolePermissions);

        Task AddRolePermissionAsync(RolePermission rolePermission);

        Task<List<Permission>> GetAllPermissions();

        Task<List<ulong>> GetPermissionsOfRole(ulong roleId);

        Task<List<RolePermission>> GetListOfPermissionsRole(ulong roleId);

        #endregion
    }
}
