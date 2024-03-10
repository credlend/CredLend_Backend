using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.ViewModels;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentPlanController : ControllerBase
    {
        private readonly IInventmentPlanRepository _investmentPlan;
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public InvestmentPlanController(IInventmentPlanRepository inventmentPlanRepository, IUnitOfWork uow, IMapper mapper)
        {
            _investmentPlan = inventmentPlanRepository;
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllPlans()
        {
            try
            {
                var plans = await _investmentPlan.GetAll();

                if (plans == null)
                {
                    return NotFound("Nenhum palno de investimento cadatrado");
                }

                var activeInvestmentPlan = plans.Where(p => p.IsActive == true).ToList();

                return Ok(activeInvestmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] InvestmentPlanViewModel request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var investmentPlan = _mapper.Map<InvestmentPlan>(request);

                _investmentPlan.Add(investmentPlan, investmentPlan.Id);

                await _uow.SaveChangesAsync();
                return Ok(investmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpGet("{InvestmentPlanId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetById(Guid InvestmentPlanId)
        {
            try
            {
                var investmentPlan = await _investmentPlan.GetById(InvestmentPlanId);

                if (investmentPlan == null)
                {
                    return BadRequest("Plano não encontrado");
                }

                return Ok(investmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] InvestmentPlanViewModel investmentPlan)
        {
            try
            {
                var entity = await _investmentPlan.GetById(investmentPlan.Id);

                if (investmentPlan.Id != entity.Id)
                {
                    return BadRequest();
                }

                if (entity == null) return NotFound();

                _mapper.Map(investmentPlan, entity);

                _investmentPlan.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{InvestmentPlanId}")]
        public async Task<IActionResult> SwitchInvestmentPlan(Guid InvestmentPlanId)
        {
            try
            {
                var existingPlan = await _investmentPlan.GetById(InvestmentPlanId);

                if (InvestmentPlanId != existingPlan.Id)
                {
                    return BadRequest();
                }

                if (existingPlan.IsActive)
                {
                    existingPlan.IsActive = false;
                }
                else
                {
                    existingPlan.IsActive = true;
                }

                _investmentPlan.SwitchInvestmentPlan(existingPlan);
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