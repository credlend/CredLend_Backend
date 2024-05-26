using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.BaseModel;

namespace Domain.Models.PlanModel
{
    public class Plan : Base
    {
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public bool IsActive { get; set; } = true;
    }
}