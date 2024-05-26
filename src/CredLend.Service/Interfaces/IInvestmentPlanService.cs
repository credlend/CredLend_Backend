using CredLend.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service.Interfaces
{
    public interface IInvestmentPlanService
    {
        Task<ICollection<InvestmentPlanDTO>> Get();
        Task<InvestmentPlanDTO> Get(Guid id);
        void Add(InvestmentPlanDTO dto);
        void Update(InvestmentPlanDTO dto);
        void Delete(Guid id);
    }
}
