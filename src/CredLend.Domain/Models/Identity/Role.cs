using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity
{
    public class Role : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}