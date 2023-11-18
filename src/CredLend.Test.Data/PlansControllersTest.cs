using Domain.Models.PlanModel;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Test.Data
{
    public class PlansControllersTest : IClassFixture<DbTest>
    {
        private readonly ServiceProvider _serviceProvider;
        public PlansControllersTest(DbTest DbTest)
        {
            _serviceProvider = DbTest.ServiceProvider;
        }

        [Fact]
        public async Task Add_WhenCalled_ReturnsCreatedAtActionResult()
        {
            using (var context = _serviceProvider.GetService<ApplicationDataContext>())
            {
                UnitOfWork _unitOfWork = new UnitOfWork(context);
                LoanPlanRepository _loanPlanRepository = new LoanPlanRepository(context);
                LoanPlan entity = new()
                {
                    Id = Guid.NewGuid(),
                    TypePlan = "Loan",
                    ValuePlan = 900.00,
                    TransactionWay = "PIX",
                    IsActive = true,
                    PaymentTerm = new DateTime(),
                    InterestRate = 3

                };
                var result = await _loanPlanRepository.Add(entity, entity.Id);
                await _unitOfWork.SaveChangesAsync();
                Assert.NotNull(result);
                Assert.Equal(entity.TypePlan, result.TypePlan);
                Assert.False(result.Id == Guid.Empty);
            }
        }
    }
}
