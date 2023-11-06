using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Domain.Models.PlanModel
{
    public interface IInventmentPlanRepository : IRepository<InvestmentPlan, Guid>
    {
       void SwitchInvestmentPlan(InvestmentPlan entity);
    }
}