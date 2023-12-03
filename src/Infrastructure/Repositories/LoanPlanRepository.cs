using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.PlanModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanPlanRepository : RepositoryBase<LoanPlan, Guid>, ILoanPlanRepository
    {
        protected readonly DbSet<LoanPlan> _entity;
        
        public LoanPlanRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
            _entity = applicationDataContext.Set<LoanPlan>();
        }

        public void SwitchLoanPlan(LoanPlan entity)
        {
            _entity.Update(entity);
        }
    }
}