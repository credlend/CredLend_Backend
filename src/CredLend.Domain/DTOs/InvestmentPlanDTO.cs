using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class InvestmentPlanDTO
    {
        public Guid Id { get; set; }
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public DateTime ReturnDeadLine { get; set; }
        public double ReturnRate { get; set; }
        public bool IsActive { get; set; }
    }
}
