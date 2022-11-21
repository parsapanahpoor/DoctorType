using DoctorType.Application.Services.Interfaces;
using DoctorType.Application.SiteServices;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Application.Services.Implementation
{
    public class RoleService : IRoleService
    {
        #region Ctor

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        #endregion

        #region Roles

        public async Task<List<string>> GetUsersRoleEnglisTitle(ulong UserId)
        {
            return await _roleRepository.GetUsersRoleEnglisTitle(UserId);
        }

        public async Task<CreateRoleResult> CraeteRole(CreateRoleViewModel roleViewModel, List<ulong> SelectedPermissions)
        {
            if (await _roleRepository.IsExistRoleTitle(roleViewModel.RoleUniqueName)) return CreateRoleResult.RoleTitleExists;
            var newRole = new Domain.Entities.Account.Role()
            {
                CreateDate = DateTime.Now,
                IsDelete = false,
                RoleUniqueName = roleViewModel.RoleUniqueName,
                Title = roleViewModel.Title,
            };

            await _roleRepository.AddRoleAsync(newRole);
            await _roleRepository.SaveChangesAsync();
            await AddPermissionsToRole(newRole.Id, SelectedPermissions);
            await _roleRepository.SaveChangesAsync();

            return CreateRoleResult.Success;
        }

        public async Task<List<Role>> FilterRoles()
        {
            return await _roleRepository.FilterRoles();
        }

        public async Task<List<Domain.Entities.Account.Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task AddRoleToUser(ulong UserId, List<ulong> RoleId)
        {
            foreach (var item in RoleId)
            {
                UserRole user = new UserRole
                {
                    UserId = UserId,
                    RoleId = item
                };
                await _roleRepository.AddRoleToUser(user);
            };
        }

        public async Task<List<ulong>> GetUserRoles(ulong Userid)
        {
            return await _roleRepository.GetUserRolesID(Userid);
        }

        public async Task<List<UserRole>> GetAllUserRoles(ulong userid)
        {
            return await _roleRepository.GetAllUserRoles(userid);
        }

        public async Task DeleteAllUserRole(ulong UserID)
        {
            List<UserRole> userRole = await GetAllUserRoles(UserID);
            _roleRepository.DeleteAllUserRole(userRole);
        }

        public bool IsUserHasRole(ulong UserID)
        {
            return _roleRepository.IsUserHasRole(UserID);
        }
        
        public async Task<Domain.Entities.Account.Role> GetRoleForEdit(ulong roleId)
        {
            return await _roleRepository.GetRoleAsyncById(roleId);
        }

        public async Task<EditRoleResult> EditRole(Domain.Entities.Account.Role role, List<ulong> SelectedPermissions)
        {

            var currentRole = await _roleRepository.GetRoleAsyncById(role.Id);
            if (currentRole == null) return EditRoleResult.RoleNotFound;
            if (await _roleRepository.IsExistRoleTitle(role.RoleUniqueName) && currentRole.RoleUniqueName != role.RoleUniqueName) return EditRoleResult.RoleTitleExists;

            currentRole.RoleUniqueName = role.RoleUniqueName;
            currentRole.Title = role.Title;
            currentRole.IsDelete = role.IsDelete;
            currentRole.CreateDate = role.CreateDate;

            _roleRepository.EditRole(currentRole);
            await _roleRepository.SaveChangesAsync();
            await EditPermissionsRole(role.Id, SelectedPermissions);

            return EditRoleResult.Success;
        }

        public async Task<bool> CheckPermission(string permissionTitle, ulong UserID)
        {
            List<ulong> UserRoles = await GetUserRoles(UserID);

            if (!UserRoles.Any())
                return false;

            List<ulong> RolesPermission = await _roleRepository.GetAllRolesPermissionID(permissionTitle);

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }

        public async Task<bool> SoftDeleteRoleByAdmin(ulong roleId)
        {
            var role = await _roleRepository.GetRoleAsyncById(roleId);

            if (role != null)
            {
                role.IsDelete = true;
                _roleRepository.EditRole(role);
                await _roleRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }
        public async Task<bool> RecoveryRoleByAdmin(ulong roleId)
        {
            var role = await _roleRepository.GetRoleAsyncById(roleId);
            if (role != null)
            {
                role.IsDelete = false;
                _roleRepository.EditRole(role);
                await _roleRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        #endregion

        #region Permissions

        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _roleRepository.GetAllPermissions();
        }

        public async Task AddPermissionsToRole(ulong roleId, List<ulong> SelectedPermissions)
        {
            List<RolePermission> rolePermissions = new List<RolePermission>();
            foreach (var permissionId in SelectedPermissions)
            {
                rolePermissions.Add(new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                    CreateDate = DateTime.Now,
                    IsDelete = false
                });
            }
            await _roleRepository.AddRangeRolePermissions(rolePermissions);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task<List<ulong>> GetPermissionsOfRole(ulong roleId)
        {
            return await _roleRepository.GetPermissionsOfRole(roleId);
        }

        public async Task<List<RolePermission>> GetListOfPermissionsRole(ulong roleId)
        {
            return await _roleRepository.GetListOfPermissionsRole(roleId);
        }

        public async Task<EditRoleResult> EditPermissionsRole(ulong roleId, List<ulong> SelectedPermissions)
        {
            var rolePermissionsList = await GetListOfPermissionsRole(roleId);
            _roleRepository.DeletePermanentRolePermissions(rolePermissionsList);
            await AddPermissionsToRole(roleId, SelectedPermissions);
            await _roleRepository.SaveChangesAsync();
            return EditRoleResult.Success;
        }

        #endregion
    }
}
