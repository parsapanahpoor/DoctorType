using DoctorType.Data.DbContext;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.Interfaces.EfCore;
using DoctorType.Domain.ViewModels.Access;
using DoctorType.Domain.ViewModels.Common.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        #region Ctor

        private readonly DoctorTypeDbContext _context;

        public RoleRepository(DoctorTypeDbContext contetx)
        {
            _context = contetx;
        }

        #endregion

        #region Roles

        public async Task<List<string>> GetUsersRoleEnglisTitle(ulong UserId)
        {
            return await _context.UserRoles.Where(p => p.UserId == UserId).Include(p => p.Role)
                                                    .Select(p => p.Role.RoleUniqueName).ToListAsync();
        }
        public async Task AddRoleAsync(Domain.Entities.Account.Role role)
        {
            await _context.Roles.AddAsync(role);
        }
        public async Task<Domain.Entities.Account.Role> GetRoleAsyncById(ulong roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }
        public void EditRole(Domain.Entities.Account.Role role)
        {
            _context.Roles.Update(role);
        }
        public void SoftDeleteRole(Domain.Entities.Account.Role role)
        {
            role.IsDelete = true;
            EditRole(role);
        }
        public async Task SoftDeleteRole(ulong roleId)
        {
            var role = await GetRoleAsyncById(roleId);
            SoftDeleteRole(role);
        }
        public void DeletePermanentRole(Domain.Entities.Account.Role role)
        {
            _context.Roles.Remove(role);
        }
        public async Task DeletePermanentRole(ulong roleId)
        {
            var role = await GetRoleAsyncById(roleId);
            DeletePermanentRole(role);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistRoleTitle(string roleTitle)
        {
            return await _context.Roles.AnyAsync(s => s.RoleUniqueName == roleTitle);
        }

        public async Task<List<Role>> FilterRoles()
        {
            return await _context.Roles.Where(p => !p.IsDelete).ToListAsync();
        }

        public async Task<List<Domain.Entities.Account.Role>> GetAllRoles()
        {
            return await _context.Roles.Where(r => r.IsDelete == false).ToListAsync();
        }

        public async Task AddRoleToUser(UserRole user)
        {
            await _context.UserRoles.AddAsync(user);
        }
        public async Task<List<ulong>> GetUserRolesID(ulong UserId)
        {
            return await _context.UserRoles.Where(r => r.UserId == UserId)
                                    .Select(r => r.RoleId).ToListAsync();
        }

        public void DeleteAllUserRole(List<UserRole> User)
        {
            _context.UserRoles.RemoveRange(User);
        }

        public async Task<List<UserRole>> GetAllUserRoles(ulong userid)
        {
            return await _context.UserRoles.Where(r => r.UserId == userid).ToListAsync();
        }

        public bool IsUserHasRole(ulong Userid)
        {
            return _context.UserRoles.Any(p => p.UserId == Userid);
        }

        #endregion

        #region Permissions

        public void DeletePermanentRolePermissions(List<RolePermission> rolePermissions)
        {
            _context.RemoveRange(rolePermissions);
        }
        public async Task AddRangeRolePermissions(List<RolePermission> rolePermissions)
        {
            foreach (var rolePermission in rolePermissions)
            {
                await AddRolePermissionAsync(rolePermission);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddRolePermissionAsync(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
        }
        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _context.Permissions
            .Where(s => !s.IsDelete)
                .ToListAsync();
        }
        public async Task<List<ulong>> GetPermissionsOfRole(ulong roleId)
        {
            return await _context.RolePermissions
                .AsQueryable()
                .Where(s => s.RoleId == roleId)
                .Select(s => s.PermissionId)
                .ToListAsync();
        }
        public async Task<List<RolePermission>> GetListOfPermissionsRole(ulong roleId)
        {
            return await _context.RolePermissions
                .Where(s => s.RoleId == roleId)
            .ToListAsync();
        }

        public async Task<List<ulong>> GetAllRolesPermissionID(string PermissionTitle)
        {
            return await _context.RolePermissions.Include(p => p.Permission)
                                         .Where(p => p.Permission.PermissionUniqueName == PermissionTitle)
                                                             .Select(p => p.RoleId).ToListAsync();
        }

        #endregion
    }
}
