using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.OperationsModel
{
    public class OperationsInvestmentPlan : Operations
    {
        public double ReturnRate { get; set; }
        public DateTime ReturnDeadLine { get; set; }
    }
}