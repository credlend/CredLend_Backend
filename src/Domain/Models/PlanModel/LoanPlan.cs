using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.PlanModel
{
    public class LoanPlan : Plan
    {
        public DateTime PaymentTerm { get; set; }
        public float InterestRate { get; set; }
    }
}