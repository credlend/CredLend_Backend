using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.UserModel
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}