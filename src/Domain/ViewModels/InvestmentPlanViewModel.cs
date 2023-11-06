using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class InvestmentPlanViewModel : PlanViewModel
    {
        public DateTime ReturnDeadLine { get; set; }

         public float ReturnRate { get; set; }
        
    }
}