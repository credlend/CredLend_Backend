using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.PlanModel;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class LoanPlanRepository : RepositoryBase<LoanPlan, Guid>, ILoanPlanRepository
    {
        
        public LoanPlanRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }

        public void SwitchLoanPlan(LoanPlan entity)
        {
            _entity.Update(entity);
        }
    }
}