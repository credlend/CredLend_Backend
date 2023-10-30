using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdm { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}