using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.Dto
{
    public class UpdateUserDto
    {
        public string Email {get; set;}
        public string Role {get; set; }
        public bool Delete {get; set;} = false;
    }
}