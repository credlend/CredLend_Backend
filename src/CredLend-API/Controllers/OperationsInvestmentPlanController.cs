using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.OperationsModel;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Add([FromBody] OperationsInvestmentPlanViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var opInvestmentPlan = _mapper.Map<OperationsInvestmentPlan>(request);

            _operationsInvestmentPlan.Add(opInvestmentPlan, opInvestmentPlan.Id);

            await _uow.SaveChangesAsync();
            return Ok(opInvestmentPlan);
        }


        [HttpDelete("{UserId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            var entity = await _operationsInvestmentPlan.GetById(UserId);

            if (entity == null) return NotFound();

            entity.IsActive = false;

            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}