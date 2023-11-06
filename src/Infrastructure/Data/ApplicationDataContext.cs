using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.Models.UserModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDataContext : DbContext, IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<LoanPlan> LoanPlan { get; set; }
        public DbSet<InvestmentPlan> InvestmentPlan { get; set; }
    }
}