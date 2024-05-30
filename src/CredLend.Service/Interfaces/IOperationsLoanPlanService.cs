using CredLend.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service.Interfaces
{
    public interface IOperationsLoanPlanService
    {
        Task<OperationsLoanPlanDTO> Get(Guid id);
        void Add(OperationsLoanPlanDTO dto);
        void Delete(Guid id);
    }
}
