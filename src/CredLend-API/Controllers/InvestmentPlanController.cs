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
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvestmentPlanController : ControllerBase
    {
        private readonly IInvestmentPlanService _service;
        public InvestmentPlanController(IInvestmentPlanService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _service.Get();

                var plans = response.Select(response => new InvestmentPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    ReturnDeadLine = response.ReturnDeadLine,
                    ReturnRate = response.ReturnRate,
                    IsActive = response.IsActive
                });   

                if (plans == null)
                {
                    return NotFound("Nenhum plano de investimento cadatrado");
                }

                return Ok(plans);
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
                var response = await _service.Get(id);

                var investmentPlan = new InvestmentPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    ReturnDeadLine = response.ReturnDeadLine,
                    ReturnRate = response.ReturnRate,
                    IsActive = response.IsActive,
                };

                if (investmentPlan == null)
                {
                    return NotFound("Plano não encontrado");
                }

                return Ok(investmentPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(InvestmentPlanRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("O objeto de solicitação é nulo");
                }

                var investmentPlan = new InvestmentPlanDTO
                {
                    ValuePlan = request.ValuePlan,
                    TransactionWay = request.TransactionWay,
                    ReturnDeadLine = request.ReturnDeadLine,
                    ReturnRate = request.ReturnRate,
                    IsActive = request.IsActive
                };

                _service.Add(investmentPlan);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(InvestmentPlanRequest investmentPlan, Guid id)
        {
            try
            {
                var entity = await _service.Get(id);

                if (id != entity.Id)
                {
                    return NotFound("O ID da solicitação não corresponde ao ID existente.");
                }


                if (entity == null) return NotFound("Plano de investimento não encontrado.");
      
                entity.ValuePlan = investmentPlan.ValuePlan;
                entity.TransactionWay = investmentPlan.TransactionWay;
                entity.ReturnDeadLine = investmentPlan.ReturnDeadLine;
                entity.ReturnRate = investmentPlan.ReturnRate;
                entity.IsActive = investmentPlan.IsActive;

                _service.Update(entity);
             
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        /* [HttpPut]
         [Route("{id:Guid}/delete")]
         public async Task<IActionResult> Delete(Guid id)
         {
             try
             {
                 var existingPlan = await _service.Get(id);

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

                 _investmentPlan.PatchInvestmentPlan(existingPlan);
                 await _uow.SaveChangesAsync();
                 return Ok(existingPlan);
             }
             catch (Exception ex)
             {
                 return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
             }
         }*/
    }
}