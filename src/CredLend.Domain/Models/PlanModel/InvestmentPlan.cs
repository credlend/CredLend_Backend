using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.PlanModel
{
    public class InvestmentPlan : Plan 
    {
        public double ReturnRate { get; set; }
        public DateTime ReturnDeadLine { get; set; }
    }
}