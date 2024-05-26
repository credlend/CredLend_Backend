﻿using CredLend.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service.Interfaces
{
    public interface IOperationsInvestmentPlanService
    {
        void Add(OperationsInvestmentPlanDTO dto);
        void Delete(Guid id);
    }
}