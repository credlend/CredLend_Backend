using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class RegisterResponseDTO
    {
        public bool IsSucceded { get; set; }
        public string AuthToken { get; set; }
    }
}
