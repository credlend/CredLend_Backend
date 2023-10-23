using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanPlanController : ControllerBase
    {
        private readonly ILoanPlanRepository _loanPlanRepository;
        private readonly IUnitOfWork _uow;

        public LoanPlanController(ILoanPlanRepository loanPlanRepository, IUnitOfWork uow)
        {
            _loanPlanRepository = loanPlanRepository;
            _uow = uow;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _loanPlanRepository.GetAll();

            if (plans == null)
            {
                return NotFound("Nenhum palno de empréstimo cadatrado");
            }

            return Ok(plans);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LoanPlanViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var loanPlan = new LoanPlan
            {
                Id = Guid.NewGuid(),
                TypePlan = request.TypePlan,
                ValuePlan = request.ValuePlan,
                TransactionWay = request.TransactionWay,
                UserID = request.UserID,
                PaymentTerm = request.PaymentTerm,
                InterestRate = request.InterestRate,
            };

            _loanPlanRepository.Add(loanPlan);

            var response = new LoanPlanViewModel
            {
                Id = loanPlan.Id,
                TypePlan = loanPlan.TypePlan,
                ValuePlan = loanPlan.ValuePlan,
                TransactionWay = loanPlan.TransactionWay,
                UserID = loanPlan.UserID,
                PaymentTerm = loanPlan.PaymentTerm,
                InterestRate = loanPlan.InterestRate
            };

            await _uow.SaveChangesAsync();
            return Ok(response);
        }


        [HttpGet("{LoanPlanId}")]
        public IActionResult GetById(Guid LoanPlanId)
        {
            var entity = _loanPlanRepository.GetById(LoanPlanId);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] LoanPlanViewModel request)
        {

            var entity = _loanPlanRepository.GetById(request.Id);

            if (request.Id != entity.Id)
            {
                return BadRequest();
            }

            if (entity == null) return NotFound();



            entity.TypePlan = request.TypePlan;
            entity.ValuePlan = request.ValuePlan;
            entity.TransactionWay = request.TransactionWay;
            entity.UserID = request.UserID;
            entity.PaymentTerm = request.PaymentTerm;
            entity.InterestRate = request.InterestRate;


            _loanPlanRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }


        [HttpDelete("{LoanPlanId}")]

        public async Task<IActionResult> Delete(Guid LoanPlanId)
        {
            var entity = _loanPlanRepository.GetById(LoanPlanId);

            if (entity == null)
            {
                return BadRequest();
            }

            _loanPlanRepository.Delete(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }
    }
}