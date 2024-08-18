using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.OperationsModel
{
    public class OperationsLoanPlan : Operations
    {
        public DateTime PaymentTerm { get; set; }
        public double InterestRate { get; set; }
    }
}