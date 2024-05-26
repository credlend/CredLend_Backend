using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly ILoanPlanRepository _loanPlanRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LoanPlanController(ILoanPlanRepository loanPlanRepository, IUnitOfWork uow, IMapper mapper)
        {
            _loanPlanRepository = loanPlanRepository;
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _loanPlanRepository.GetAll();

                var plans = response.Select(response => new LoanPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    InterestRate = response.InterestRate,
                    PaymentTerm = response.PaymentTerm,
                    IsActive = response.IsActive
                });

                if (plans == null)
                {
                    return NotFound("Nenhum palno de investimento cadatrado");
                }

                var activeLoanPlan = plans.Where(p => p.IsActive == true).ToList();

                return Ok(activeLoanPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var response = await _loanPlanRepository.GetById(id);

                var loanPlan = new LoanPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    InterestRate= response.InterestRate,
                    PaymentTerm= response.PaymentTerm,
                    IsActive = response.IsActive
                };

                if (loanPlan == null)
                {
                    return NotFound("Plano não encontrado");
                }

                return Ok(loanPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] LoanPlanRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var loanPlan = new LoanPlan
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    InterestRate = request.InterestRate,
                    PaymentTerm = request.PaymentTerm,
                    IsActive = request.IsActive
                };

                _loanPlanRepository.Add(loanPlan);

                await _uow.SaveChangesAsync();
                return Ok(loanPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] LoanPlanViewModel loanPlan)
        {
            try
            {
                var entity = await _loanPlanRepository.GetById(loanPlan.Id);

                if (loanPlan.Id != entity.Id)
                {
                    return NotFound("O ID da solicitação não corresponde ao ID existente.");
                }

                if (loanPlan == null) return BadRequest();

                entity.Id = loanPlan.Id;
                entity.ValuePlan = loanPlan.ValuePlan;
                entity.TransactionWay = loanPlan.TransactionWay;
                entity.InterestRate = loanPlan.InterestRate;
                entity.PaymentTerm = loanPlan.PaymentTerm;
                entity.IsActive = loanPlan.IsActive;

                _loanPlanRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("{id:Guid}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingPlan = await _loanPlanRepository.GetById(id);

                if (id != existingPlan.Id)
                {
                    return NotFound("O ID da solicitação não corresponde ao ID existente.");
                }

                if (existingPlan.IsActive)
                {
                    existingPlan.IsActive = false;
                }
                else
                {
                    existingPlan.IsActive = true;
                }

                _loanPlanRepository.PatchLoanPlan(existingPlan);
                await _uow.SaveChangesAsync();
                return Ok(existingPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}