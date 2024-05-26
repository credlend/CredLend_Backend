using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class InvestmentPlanRepository : RepositoryBase<InvestmentPlan, Guid>, IInventmentPlanRepository
    {
        
        public InvestmentPlanRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}