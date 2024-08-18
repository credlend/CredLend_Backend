using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CredLend.Domain.Dto
{
    public class UpdateUserDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Deleted { get; set; } = false;
    }
}