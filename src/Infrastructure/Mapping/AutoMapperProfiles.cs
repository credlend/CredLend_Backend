using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Dto;
using Domain.Models.Identity;
using Domain.Models.OperationsModel;
using Domain.Models.PlanModel;
using Domain.Models.UserModel;
using Domain.ViewModels;

namespace Infrastructure.Mapping
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<OperationsLoanPlan, OperationsLoanPlanViewModel>().ReverseMap();
      CreateMap<OperationsInvestmentPlan, OperationsInvestmentPlanViewModel>().ReverseMap();
      CreateMap<LoanPlan, LoanPlanViewModel>().ReverseMap();
      CreateMap<InvestmentPlan, InvestmentPlanViewModel>().ReverseMap();
      CreateMap<User, UserDto>().ReverseMap();
      CreateMap<User, UserLoginDto>().ReverseMap();
      CreateMap<Role, RoleDto>().ReverseMap();
    }
  }
}