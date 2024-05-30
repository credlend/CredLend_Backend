using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
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
        private readonly IOperationsLoanPlanService _service;

        public OperationsLoanPlanController(IOperationsLoanPlanService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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

                var loanPlan = new OperationsLoanPlanDTO
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    Email = request.Email,
                    OperationDate = request.OperationDate,
                    InterestRate = request.InterestRate,
                    UserID = request.UserID,
                    UserName = request.UserName,
                    PaymentTerm = request.PaymentTerm,
                };

                _service.Add(loanPlan);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var entity = await _service.Get(id);

                if (entity == null)
                {
                    return NotFound("Plano de investimento não encontrado.");
                }

                _service.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}