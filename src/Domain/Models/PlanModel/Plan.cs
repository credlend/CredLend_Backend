using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.PlanModel
{
    public class Plan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TypePlan { get; set; }
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public bool IsActive { get; set; }
    }
}