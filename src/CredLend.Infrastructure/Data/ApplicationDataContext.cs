using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.Identity;
using Domain.Models.OperationsModel;
using Domain.Models.PlanModel;
using Domain.Models.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDataContext : IdentityDbContext<User, Role, string,
            IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
            IdentityRoleClaim<string>, IdentityUserToken<string>>, IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<LoanPlan> LoanPlan { get; set; }
        public DbSet<InvestmentPlan> InvestmentPlan { get; set; }
        public DbSet<OperationsLoanPlan> OperationsLoanPlan { get; set; }
        public DbSet<OperationsInvestmentPlan> OperationsInvestmentPlan { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(up =>
            {
                up.HasKey(u => new { u.UserId, u.RoleId });

                up.HasOne(u => u.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(u => u.RoleId)
                   .IsRequired();

                up.HasOne(u => u.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<OperationsLoanPlan>()
                .HasOne(o => o.User)
                .WithMany(u => u.OperationsLoanPlan)
                .HasForeignKey(o => o.UserID);

            modelBuilder.Entity<OperationsInvestmentPlan>()
                .HasOne(o => o.User)
                .WithMany(u => u.OperationsInvestmentPlan)
                .HasForeignKey(o => o.UserID);

        }
    }
}