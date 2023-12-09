using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Domain.Models.OperationsModel
{
    public interface IOperationsInvestmentPlanRepository : IRepository<OperationsInvestmentPlan, Guid>
    {
    }
}