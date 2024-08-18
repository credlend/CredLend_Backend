using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class OperationsLoanPlanRequest
    {
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime PaymentTerm { get; set; }
        public double InterestRate { get; set; }
    }
}
