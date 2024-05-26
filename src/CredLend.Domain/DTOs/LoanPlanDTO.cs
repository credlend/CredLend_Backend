using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class LoanPlanDTO
    {
        public Guid Id { get; set; }
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public DateTime PaymentTerm { get; set; }
        public double InterestRate { get; set; }
        public bool IsActive { get; set; }
    }
}
