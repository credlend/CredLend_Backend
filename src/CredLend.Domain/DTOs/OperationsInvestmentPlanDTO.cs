﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class OperationsInvestmentPlanDTO
    {
        public Guid Id { get; set; }
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime OperationDate { get; set; }
        public double ReturnRate { get; set; }
        public DateTime ReturnDeadLine { get; set; }
        public bool IsActive { get; set; }
    }
}
