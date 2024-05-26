using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service
{
    public class InvestmentPlanService : IInvestmentPlanService
    {
        private readonly IInventmentPlanRepository _repository;
        private readonly ApplicationDataContext _context;
        public InvestmentPlanService(IInventmentPlanRepository inventmentPlanRepository, ApplicationDataContext context)
        {
            _repository = inventmentPlanRepository;
            _context = context;
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

            if(activePlanDTOs == null)
            {
                return null;
            }

            return activePlanDTOs;
        }

        public async Task<InvestmentPlanDTO> Get(Guid id)
        {
            var plan = await _repository.GetById(id);

            var planDTO = new InvestmentPlanDTO
            {
                Id = plan.Id,
                IsActive = plan.IsActive,
                ReturnDeadLine = plan.ReturnDeadLine,
                ReturnRate = plan.ReturnRate,
                TransactionWay = plan.TransactionWay,
                ValuePlan = plan.ValuePlan,
            };

            if (planDTO == null || planDTO.IsActive == false)
            {
                return null;
            }

            return planDTO;
        }

        public void Add(InvestmentPlanDTO dto)
        {
            var investmentPlan = new InvestmentPlan
            {
                ValuePlan = dto.ValuePlan,
                TransactionWay = dto.TransactionWay,
                ReturnDeadLine = dto.ReturnDeadLine,
                ReturnRate = dto.ReturnRate,
                IsActive = dto.IsActive
            };

            _repository.Add(investmentPlan);
        }

        public void Update(InvestmentPlanDTO dto)
        {
            var entity = _context.InvestmentPlan.Find(dto.Id);

            if (entity != null)
            {
                entity.IsActive = dto.IsActive;
                entity.ReturnDeadLine = dto.ReturnDeadLine;
                entity.ReturnRate = dto.ReturnRate;
                entity.TransactionWay = dto.TransactionWay;
                entity.ValuePlan = dto.ValuePlan;

                _repository.Update(entity);
            }
        }


        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
