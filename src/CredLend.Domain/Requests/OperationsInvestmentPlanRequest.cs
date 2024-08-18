using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class OperationsInvestmentPlanRequest
    {
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double ReturnRate { get; set; }
        public DateTime ReturnDeadLine { get; set; }
    }
}
