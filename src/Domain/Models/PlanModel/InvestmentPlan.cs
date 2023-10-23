using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.PlanModel
{
    public class InvestmentPlan : Plan 
    {
        public float ReturnRate { get; set; }

        public DateTime ReturnDeadLine { get; set; }
    }
}