using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.Requests
{
    public class UserRequest
    {
        public string CompleteName { get; set; }

        public string UserName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
