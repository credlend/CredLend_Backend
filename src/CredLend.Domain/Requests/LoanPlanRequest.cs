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
        public string TransactionWay { get; set; } = string.Empty;
        public double InterestRate { get; set; }
    }
}
