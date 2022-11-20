using BasePaging.Domain.ViewModels.Common;
using DoctorType.Domain.Entities.Account;
using DoctorType.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.ViewModels.Access
{
    public class FilterRolesViewModel : BasePaging<Role>
    {
        public string? RoleTitle { get; set; }

        public string? RoleUniqueName { get; set; }

        public ulong? ParentId { get; set; }

        public Role ParentRole { get; set; }
    }
}
