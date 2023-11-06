using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.ViewModels;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentPlanController : ControllerBase
    {
        private readonly IInventmentPlanRepository _investmentPlan;
        private readonly IUnitOfWork _uow;

        public InvestmentPlanController(IInventmentPlanRepository inventmentPlanRepository, IUnitOfWork uow)
        {
            _investmentPlan = inventmentPlanRepository;
            _uow = uow;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _investmentPlan.GetAll();

            if (plans == null)
            {
                return NotFound("Nenhum palno de empréstimo cadatrado");
            }

            return Ok(plans);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvestmentPlanViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var investmentPlan = new InvestmentPlan
            {
                Id = Guid.NewGuid(),
                TypePlan = request.TypePlan,
                ValuePlan = request.ValuePlan,
                TransactionWay = request.TransactionWay,
                UserID = request.UserID,
                ReturnRate = request.ReturnRate,
                ReturnDeadLine = request.ReturnDeadLine,
            };

            _investmentPlan.Add(investmentPlan);

            var response = new InvestmentPlanViewModel
            {
                Id = investmentPlan.Id,
                TypePlan = investmentPlan.TypePlan,
                ValuePlan = investmentPlan.ValuePlan,
                TransactionWay = investmentPlan.TransactionWay,
                UserID = investmentPlan.UserID,
                ReturnRate = investmentPlan.ReturnRate,
                ReturnDeadLine = investmentPlan.ReturnDeadLine
            };

            await _uow.SaveChangesAsync();
            return Ok(response);
        }


        [HttpGet("{InvestmentPlanId}")]
        public IActionResult GetById(Guid InvestmentPlanId)
        {
            var entity = _investmentPlan.GetById(InvestmentPlanId);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] InvestmentPlanViewModel request)
        {

            var entity = _investmentPlan.GetById(request.Id);

            if (request.Id != entity.Id)
            {
                return BadRequest();
            }

            if (entity == null) return NotFound();



            entity.TypePlan = request.TypePlan;
            entity.ValuePlan = request.ValuePlan;
            entity.TransactionWay = request.TransactionWay;
            entity.UserID = request.UserID;
            entity.ReturnRate = request.ReturnRate;
            entity.ReturnDeadLine = request.ReturnDeadLine;


            _investmentPlan.Update(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }


        [HttpDelete("{InvestmentPlanId}")]

        public async Task<IActionResult> Delete(Guid InvestmentPlanId)
        {
            var entity = _investmentPlan.GetById(InvestmentPlanId);

            if (entity == null)
            {
                return BadRequest();
            }

            _investmentPlan.Delete(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }
    
    }
}