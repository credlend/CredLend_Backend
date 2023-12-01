using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class PlanViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public bool IsActive { get; set; }
    }
}