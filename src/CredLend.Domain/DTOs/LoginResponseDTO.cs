using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class LoginResponseDTO
    {
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public bool IsSucceded { get; set; }
        public bool IsActive { get; set; }
    }
}
