using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class AuthResponseDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? CompleteName { get; set; }
        public string? Email {  get; set; } 
        public bool IsAuthSuccessful { get; set; }
        public string? Token { get; set; }
        public string? Provider { get; set; }
        public bool ExternalLogin { get; set; }
    }
}
