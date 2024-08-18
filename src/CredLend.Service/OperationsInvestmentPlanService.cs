using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Models.OperationsModel;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service
{
    public class OperationsInvestmentPlanService : IOperationsInvestmentPlanService
    {
        private readonly IOperationsInvestmentPlanRepository _repository;
        private readonly ApplicationDataContext _context;

        public OperationsInvestmentPlanService(IOperationsInvestmentPlanRepository operationsInvestmentPlanRepository, ApplicationDataContext context)
        {
            _repository = operationsInvestmentPlanRepository ?? throw new ArgumentNullException(nameof(operationsInvestmentPlanRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OperationsInvestmentPlanDTO> Get(Guid id)
        {
            var operation = await _repository.GetById(id);

            if (operation == null || !operation.IsActive)
            {
                return null;
            }

            var operationDTO = new OperationsInvestmentPlanDTO
            {
                Id = operation.Id,
                Email = operation.Email,
                ReturnRate = operation.ReturnRate,
                OperationDate = operation.OperationDate,
                ReturnDeadLine = operation.ReturnDeadLine,
                UserName = operation.UserName,
                UserID = operation.UserID,
                TransactionWay = operation.TransactionWay,
                ValuePlan = operation.ValuePlan,
                IsActive = operation.IsActive
            };

            return operationDTO;
        }

        public void Add(OperationsInvestmentPlanDTO dto)
        {
            var opInvestmentPlan = new OperationsInvestmentPlan
            {
                ValuePlan = dto.ValuePlan,
                TransactionWay = dto.TransactionWay,
                Email = dto.Email,
                ReturnRate = dto.ReturnRate,
                ReturnDeadLine= dto.ReturnDeadLine, 
                OperationDate = DateTime.UtcNow,
                UserName = dto.UserName,
                UserID = dto.UserID,
                IsActive = true
            };

            _repository.Add(opInvestmentPlan);
        }

        public void Delete(Guid id)
        {
            var entity = _context.OperationsInvestmentPlan.Find(id);

            if (entity != null)
            {
                entity.IsActive = false;
                _repository.Update(entity);
            }
        }
    }
}
