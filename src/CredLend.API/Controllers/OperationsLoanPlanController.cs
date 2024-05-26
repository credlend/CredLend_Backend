using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.OperationsModel;
using Domain.Requests;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OperationsLoanPlanController : ControllerBase
    {
        private readonly IOperationsLoanPlanRepository _operationsLoanPlan;

        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public OperationsLoanPlanController(IOperationsLoanPlanRepository opLoanPlanRepository, IUnitOfWork uow, IMapper mapper)
        {
            _operationsLoanPlan = opLoanPlanRepository;
            _uow = uow;
            _mapper = mapper;
        }



        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Add([FromBody] OperationsLoanPlanRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var opLoanPlan = new OperationsLoanPlan
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    UserName = request.UserName,
                    Email = request.Email,
                    OperationDate = request.OperationDate,
                    IsActive = request.IsActive,
                    UserID = request.UserID,
                    InterestRate = request.InterestRate,
                    PaymentTerm = request.PaymentTerm,
                };

                _operationsLoanPlan.Add(opLoanPlan);

                await _uow.SaveChangesAsync();
                return Ok(opLoanPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpDelete("{OperationId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            try
            {
                var entity = await _operationsLoanPlan.GetById(UserId);

                if (entity == null) return NotFound();

                entity.IsActive = false;

                await _uow.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}