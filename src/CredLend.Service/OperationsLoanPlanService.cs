using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Models.OperationsModel;
using Domain.Models.PlanModel;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service
{
    public class OperationsLoanPlanService : IOperationsLoanPlanService
    {
        private readonly IOperationsLoanPlanRepository _repository;
        private readonly ApplicationDataContext _context;

        public OperationsLoanPlanService(IOperationsLoanPlanRepository operationsLoanPlanRepository, ApplicationDataContext context)
        {
            _repository = operationsLoanPlanRepository ?? throw new ArgumentNullException(nameof(operationsLoanPlanRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OperationsLoanPlanDTO> Get(Guid id)
        {
            var operation = await _repository.GetById(id);

            if (operation == null || !operation.IsActive)
            {
                return null;
            }

            var operationDTO = new OperationsLoanPlanDTO
            {
                Id = operation.Id,
                Email = operation.Email,
                InterestRate = operation.InterestRate,
                OperationDate = operation.OperationDate,
                PaymentTerm = operation.PaymentTerm,
                UserName = operation.UserName,
                UserID = operation.UserID,
                TransactionWay = operation.TransactionWay,
                ValuePlan = operation.ValuePlan,
                IsActive = operation.IsActive
            };

            return operationDTO;
        }

        public void Add(OperationsLoanPlanDTO dto)
        {
            var opLoanPlan = new OperationsLoanPlan
            {
                ValuePlan = dto.ValuePlan,
                TransactionWay = dto.TransactionWay,
                Email = dto.Email,
                InterestRate = dto.InterestRate,
                OperationDate = dto.OperationDate,
                PaymentTerm = dto.PaymentTerm,
                UserName = dto.UserName,
                UserID = dto.UserID,
                IsActive = true
            };

            _repository.Add(opLoanPlan);
        }

        public void Delete(Guid id)
        {
            var entity = _context.OperationsLoanPlan.Find(id);

            if (entity != null)
            {
                entity.IsActive = false;
                _repository.Update(entity);
            }
        }
    }
}
