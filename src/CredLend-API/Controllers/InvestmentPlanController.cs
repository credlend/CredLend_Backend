using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        private readonly IMapper _mapper;

        public InvestmentPlanController(IInventmentPlanRepository inventmentPlanRepository, IUnitOfWork uow, IMapper mapper)
        {
            _investmentPlan = inventmentPlanRepository;
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _investmentPlan.GetAll();

            if (plans == null)
            {
                return NotFound("Nenhum palno de empréstimo cadatrado");
            }

            var activeLoanPlan = plans.Where(plan => plan.IsActive == true).ToList();

            if (activeLoanPlan.Count == 0)
            {
                return NotFound("Nenhum plano ativo encontrado");
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

            var investmentPlan = _mapper.Map<InvestmentPlan>(request);

            var listInvestmentPlan = await _investmentPlan.GetAll();

            listInvestmentPlan.ToList();

            foreach (var item in listInvestmentPlan)
            {
                bool verifica = investmentPlan.TypePlan.Contains(item.TypePlan, StringComparison.OrdinalIgnoreCase);
                if (verifica)
                {
                    return BadRequest("Este plano já existe no banco de dados");
                }
            }

            _investmentPlan.Add(investmentPlan);

            await _uow.SaveChangesAsync();
            return Ok(investmentPlan);
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
        public async Task<IActionResult> Put([FromBody] InvestmentPlanViewModel investmentPlan)
        {
            var entity = _investmentPlan.GetById(investmentPlan.Id);

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

        [HttpPut("{InvestmentPlanId}")]
        public async Task<IActionResult> SwitchInvestmentPlan(Guid InvestmentPlanId){
            var existingPlan = _investmentPlan.GetById(InvestmentPlanId);

            if(InvestmentPlanId != existingPlan.Id) {
                return BadRequest();
            }

            if(!existingPlan.IsActive) {
                existingPlan.IsActive = true;
            } else {
                existingPlan.IsActive = false;
            }

            _investmentPlan.SwitchInvestmentPlan(existingPlan);
            await _uow.SaveChangesAsync();
            return Ok(existingPlan);
        }
    }
}