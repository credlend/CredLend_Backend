using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class LoanPlanViewModel : PlanViewModel
    {
        public DateTime PaymentTerm { get; set; }
        public double InterestRate { get; set; }
    }
}