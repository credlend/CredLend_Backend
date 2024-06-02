using CredLend.Domain.DTOs;
using CredLend.Service.Interfaces;
using Domain.Requests;
using Domain.ViewModels;
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
            _service = service ?? throw new ArgumentNullException(nameof(service));
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

                var investmentPlan = new InvestmentPlanViewModel
                {
                    Id = response.Id,
                    ValuePlan = response.ValuePlan,
                    TransactionWay = response.TransactionWay,
                    ReturnDeadLine = response.ReturnDeadLine,
                    ReturnRate = response.ReturnRate,
                    IsActive = response.IsActive,
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

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, InvestmentPlanRequest investmentPlan)
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

                var investmentPlanDTO = new InvestmentPlanDTO
                {
                    ValuePlan = investmentPlan.ValuePlan,
                    TransactionWay = investmentPlan.TransactionWay,
                    ReturnDeadLine = investmentPlan.ReturnDeadLine,
                    ReturnRate = investmentPlan.ReturnRate,
                    IsActive = investmentPlan.IsActive
                };

                _service.Update(investmentPlanDTO);

                return Ok(entity);
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

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}