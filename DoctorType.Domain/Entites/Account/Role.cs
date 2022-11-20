﻿using DoctorType.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorType.Domain.Entities.Account
{
    public class Role : BaseEntity
    {
        #region Properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        public string  Title { get; set; }

        [Display(Name = "نام یکتا")]
        [Required(ErrorMessage = "این فیلد الزامی است .")]
        public string  RoleUniqueName { get; set; }

        public ulong? ParentId { get; set; }

        #endregion

        #region Relation

        public ICollection<RolePermission> RolePermissions { get; set; }

        public Role Parent { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
