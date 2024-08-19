using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Domain.Models.OperationsModel;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.UserModel
{
    public class User : IdentityUser
    {
        public string? CompleteName { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<OperationsLoanPlan> OperationsLoanPlan { get; set; }
        public List<OperationsInvestmentPlan> OperationsInvestmentPlan { get; set; }
    }
}