using CredLend.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service.Interfaces
{
    public interface ILoanPlanService
    {
        Task<ICollection<LoanPlanDTO>> Get();
        Task<LoanPlanDTO> Get(Guid id);
        void Add(LoanPlanDTO dto);
        void Update(LoanPlanDTO dto);
        void Delete(Guid id);
    }
}
