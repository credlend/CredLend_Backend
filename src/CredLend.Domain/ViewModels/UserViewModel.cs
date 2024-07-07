using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string CompleteName { get; set; }

        public string UserName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}
