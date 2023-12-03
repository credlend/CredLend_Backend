using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.OperationsModel;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OperationsLoanPlanRepository : RepositoryBase<OperationsLoanPlan, Guid>, IOperationsLoanPlanRepository
    {

        public OperationsLoanPlanRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}