using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.BaseModel;
using Domain.Models.UserModel;

namespace Domain.Models.OperationsModel
{
    public class Operations : Base
    {
        public double ValuePlan { get; set; }
        public string TransactionWay { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime OperationDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}