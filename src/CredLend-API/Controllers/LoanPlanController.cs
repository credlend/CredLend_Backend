using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _loanPlanRepository.GetAll();

            if (plans == null)
            {
                return NotFound("Nenhum palno de empréstimo cadatrado");
            }

            var activeLoanPlan = plans.Where(p => p.IsActive == true).ToList();
            
            if(activeLoanPlan.Count == 0){
                return NotFound("Nenhum plano ativo encontrado");
            }

            return Ok(activeLoanPlan);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] LoanPlanViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var loanPlan = _mapper.Map<LoanPlan>(request);

            _loanPlanRepository.Add(loanPlan, loanPlan.Id);

            await _uow.SaveChangesAsync();
            return Ok(loanPlan);
        }


        
        [HttpGet("{LoanPlanId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetById(Guid LoanPlanId)
        {
            var loanPlan = await _loanPlanRepository.GetById(LoanPlanId);
            return Ok(loanPlan);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] LoanPlanViewModel loanPlan)
        {

            var entity = await _loanPlanRepository.GetById(loanPlan.Id);

            if (loanPlan.Id != entity.Id)
            {
                return BadRequest();
            }

            if (entity == null) return NotFound();

            _mapper.Map(loanPlan, entity);

            _loanPlanRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut("{LoanPlanId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SwitchLoanPlan(Guid LoanPlanId){
            var existingPlan = await _loanPlanRepository.GetById(LoanPlanId);

            if(LoanPlanId != existingPlan.Id) {
                return BadRequest();
            }

            if(existingPlan.IsActive) {
                existingPlan.IsActive = false;
            } else {
                existingPlan.IsActive = true;
            }

            _loanPlanRepository.SwitchLoanPlan(existingPlan);
            await _uow.SaveChangesAsync();
            return Ok(existingPlan);
        }
    }
}