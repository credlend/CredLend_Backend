using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Domain.Models.PlanModel
{
    public interface ILoanPlanRepository : IRepository<LoanPlan, Guid>
    {
        void SwitchLoanPlan(LoanPlan entity);
    }
}