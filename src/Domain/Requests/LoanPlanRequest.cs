using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class LoanPlanRequest
    {
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public DateTime PaymentTerm { get; set; }
        public double InterestRate { get; set; }
        public bool IsActive { get; set; }
    }
}
