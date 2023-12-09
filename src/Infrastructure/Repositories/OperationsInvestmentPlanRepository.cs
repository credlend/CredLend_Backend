using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.OperationsModel;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OperationsInvestmentPlanRepository : RepositoryBase<OperationsInvestmentPlan, Guid>, IOperationsInvestmentPlanRepository
    {

        public OperationsInvestmentPlanRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}