using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class InvestmentPlanViewModel 
    {
        public Guid Id { get; set; } 
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public DateTime ReturnDeadLine { get; set; }
        public double ReturnRate { get; set; }
        public bool IsActive { get; set; }

    }
}