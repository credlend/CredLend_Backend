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
        public async Task<IActionResult> Add([FromBody] OperationsLoanPlanViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var opLoanPlan = _mapper.Map<OperationsLoanPlan>(request);

            _operationsLoanPlan.Add(opLoanPlan, opLoanPlan.Id);

            await _uow.SaveChangesAsync();
            return Ok(opLoanPlan);
        }


        [HttpDelete("{UserId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            var entity = await _operationsLoanPlan.GetById(UserId);

            if (entity == null) return NotFound();

            entity.IsActive = false;

            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}