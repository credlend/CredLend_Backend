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
    public class OperationsInvestmentPlanController : ControllerBase
    {
        private readonly IOperationsInvestmentPlanRepository _operationsInvestmentPlan;

        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public OperationsInvestmentPlanController(IOperationsInvestmentPlanRepository opInvestmentPlanRepository, IUnitOfWork uow, IMapper mapper)
        {
            _operationsInvestmentPlan = opInvestmentPlanRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Add([FromBody] OperationsInvestmentPlanRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var opInvestmentPlan = new OperationsInvestmentPlan
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    UserName = request.UserName,
                    Email = request.Email,
                    OperationDate = request.OperationDate,
                    IsActive = request.IsActive,
                    UserID = request.UserID,
                    ReturnRate = request.ReturnRate,
                    ReturnDeadLine = request.ReturnDeadLine,
                };

                _operationsInvestmentPlan.Add(opInvestmentPlan);

                await _uow.SaveChangesAsync();
                return Ok(opInvestmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpDelete()]
        [Route("{id:Guid}/delete")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            try
            {
                var entity = await _operationsInvestmentPlan.GetById(UserId);

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