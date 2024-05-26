using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Models.PlanModel;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service
{
    public class LoanPlanService : ILoanPlanService
    {
        private readonly ILoanPlanRepository _repository;
        private readonly ApplicationDataContext _context;

        public LoanPlanService(ILoanPlanRepository loanPlanRepository, ApplicationDataContext context)
        {
            _repository = loanPlanRepository ?? throw new ArgumentNullException(nameof(loanPlanRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<LoanPlanDTO>> Get()
        {
            var allPlans = await _repository.GetAll();
            var activePlans = allPlans.Where(p => p.IsActive).ToList();

            var activePlanDTOs = activePlans.Select(p => new LoanPlanDTO
            {
                Id = p.Id,
                InterestRate = p.InterestRate,
                PaymentTerm = p.PaymentTerm,
                TransactionWay = p.TransactionWay,
                ValuePlan = p.ValuePlan,
                IsActive = p.IsActive
            }).ToList();

            return activePlanDTOs;
        }

        public async Task<LoanPlanDTO> Get(Guid id)
        {
            var plan = await _repository.GetById(id);

            if (plan == null || !plan.IsActive)
            {
                return null;
            }

            var planDTO = new LoanPlanDTO
            {
                Id = plan.Id,
                InterestRate = plan.InterestRate,
                PaymentTerm = plan.PaymentTerm,
                TransactionWay = plan.TransactionWay,
                ValuePlan = plan.ValuePlan,
                IsActive = plan.IsActive
            };

            return planDTO;
        }

        public void Add(LoanPlanDTO dto)
        {
            var investmentPlan = new LoanPlan
            {
                ValuePlan = dto.ValuePlan,
                TransactionWay = dto.TransactionWay,
                InterestRate = dto.InterestRate,
                PaymentTerm = dto.PaymentTerm,
                IsActive = true
            };

            _repository.Add(investmentPlan);
        }

        public void Update(LoanPlanDTO dto)
        {
            var entity = _context.LoanPlan.Find(dto.Id);

            if (entity != null)
            {
                entity.InterestRate = dto.InterestRate;
                entity.PaymentTerm = dto.PaymentTerm;
                entity.TransactionWay = dto.TransactionWay;
                entity.ValuePlan = dto.ValuePlan;

                _repository.Update(entity);
            }
        }

        public void Delete(Guid id)
        {
            var entity = _context.LoanPlan.Find(id);

            if (entity != null)
            {
                entity.IsActive = false;
                _repository.Update(entity);
            }
        }
    }
}
