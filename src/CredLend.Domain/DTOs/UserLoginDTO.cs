using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CredLend.Domain.Dto
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}