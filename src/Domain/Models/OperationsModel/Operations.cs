using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.OperationsModel
{
    public class Operations
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime OperationDate { get; set; }  = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }
}