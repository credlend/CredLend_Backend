using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class OperationsInvestmentPlanViewModel : OperationsViewModel
    {
        public double ReturnRate { get; set; }
        public DateTime ReturnDeadLine { get; set; }
    }
}