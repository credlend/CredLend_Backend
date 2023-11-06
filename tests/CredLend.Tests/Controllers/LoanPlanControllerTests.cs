using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CredLend_API.Controllers;
using Domain.Core.Data;
using Domain.Models.PlanModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CredLend.Tests.Controllers
{
    public class LoanPlanControllerTests
    {
        private readonly Mock<ILoanPlanRepository> _loanPlanMock;
        private readonly LoanPlanController _controller;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;

        public LoanPlanControllerTests()
        {
            _loanPlanMock = new Mock<ILoanPlanRepository>();
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _controller = new LoanPlanController(_loanPlanMock.Object, _uow.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await _controller.GetById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}