﻿using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Models.PlanModel;
using Infrastructure.Data;
namespace CredLend.Service
{
    public class InvestmentPlanService : IInvestmentPlanService
    {
        private readonly IInventmentPlanRepository _repository;
        private readonly ApplicationDataContext _context;

        public InvestmentPlanService(IInventmentPlanRepository inventmentPlanRepository, ApplicationDataContext context)
        {
            _repository = inventmentPlanRepository ?? throw new ArgumentNullException(nameof(inventmentPlanRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<InvestmentPlanDTO>> Get()
        {
            var allPlans = await _repository.GetAll();
            var activePlans = allPlans.Where(p => p.IsActive).ToList();

            var activePlanDTOs = activePlans.Select(p => new InvestmentPlanDTO
            {
                Id = p.Id,
                IsActive = p.IsActive,
                ReturnDeadLine = p.ReturnDeadLine,
                ReturnRate = p.ReturnRate,
                TransactionWay = p.TransactionWay,
                ValuePlan = p.ValuePlan,
            }).ToList();

            return activePlanDTOs;
        }

        public async Task<InvestmentPlanDTO> Get(Guid id)
        {
            var plan = await _repository.GetById(id);

            if (plan == null || !plan.IsActive)
            {
                return null;
            }

            var planDTO = new InvestmentPlanDTO
            {
                Id = plan.Id,
                IsActive = plan.IsActive,
                ReturnDeadLine = plan.ReturnDeadLine,
                ReturnRate = plan.ReturnRate,
                TransactionWay = plan.TransactionWay,
                ValuePlan = plan.ValuePlan,
            };

            return planDTO;
        }

        public void Add(InvestmentPlanDTO dto)
        {
            var investmentPlan = new InvestmentPlan
            {
                ValuePlan = dto.ValuePlan,
                TransactionWay = dto.TransactionWay,
                ReturnDeadLine = CalculateReturnDeadLine(30),
                ReturnRate = dto.ReturnRate,
                IsActive = true
            };

            _repository.Add(investmentPlan);
        }

        public void Update(InvestmentPlanDTO dto)
        {
            var entity = _context.InvestmentPlan.Find(dto.Id);

            if (entity != null)
            {
                entity.ReturnRate = dto.ReturnRate;
                entity.TransactionWay = dto.TransactionWay;
                entity.ValuePlan = dto.ValuePlan;

                _repository.Update(entity);
            }
        }

        public void Delete(Guid id)
        {
            var entity = _context.InvestmentPlan.Find(id);

            if (entity != null)
            {
                entity.IsActive = false;
                _repository.Update(entity);
            }
        }

        private DateTime CalculateReturnDeadLine(int daysToAdd)
        {
            DateTime returnDeadLine = DateTime.UtcNow;
            DateTime futureDate = returnDeadLine.AddDays(daysToAdd);
            return futureDate;
        }
    }
}