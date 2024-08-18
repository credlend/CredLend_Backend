using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.Requests;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoanPlanController : ControllerBase
    {
        private readonly ILoanPlanService _service;

        public LoanPlanController(ILoanPlanService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _service.Get();

                var plans = response.Select(response => new LoanPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    InterestRate = response.InterestRate,
                    PaymentTerm = response.PaymentTerm,
                    IsActive = response.IsActive
                }).ToList();

                if (plans.Count == 0)
                {
                    return NotFound("Nenhum plano de investimento cadastrado");
                }

                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var response = await _service.Get(id);

                if (response == null)
                {
                    return NotFound("Plano não encontrado");
                }

                var investmentPlan = new LoanPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    PaymentTerm = response.PaymentTerm,
                    InterestRate = response.InterestRate,
                    IsActive = response.IsActive
                };

                return Ok(investmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(LoanPlanRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var loanPlan = new LoanPlanDTO
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    InterestRate = request.InterestRate
                };

                _service.Add(loanPlan);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, LoanPlanRequest loanPlan)
        {
            try
            {
                var entity = await _service.Get(id);

                if (entity == null)
                {
                    return NotFound("Plano de investimento não encontrado.");
                }

                if (id != entity.Id)
                {
                    return BadRequest("O ID da solicitação não corresponde ao ID existente.");
                }

                var investmentPlanDTO = new LoanPlanDTO
                {
                    Id = id,
                    ValuePlan = loanPlan.ValuePlan,
                    TransactionWay = loanPlan.TransactionWay,
                    InterestRate = loanPlan.InterestRate,
                };

                _service.Update(investmentPlanDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
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