using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.UserModel;
using Domain.ViewModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public UserController(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            if (users == null)
            {
                return NotFound("Nenhum usuário encontrado");
            }

            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserViewModel request)
        {
            if (request == null)
            {
                return BadRequest("O objeto de solicitação é nulo");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                BirthDate = request.BirthDate,
                IsAdm = request.IsAdm,
                Email = request.Email,
                Password = request.Password,
                BankAccount = request.BankAccount,
                AgencyNumber = request.AgencyNumber
            };

            _userRepository.Add(user);

            var response = new UserViewModel
            {
                Name = user.Name,
                BirthDate = user.BirthDate,
                IsAdm = user.IsAdm,
                Email = user.Email,
                Password = user.Password,
                BankAccount = user.BankAccount,
                AgencyNumber = user.AgencyNumber
            };

            await _uow.SaveChangesAsync();
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetById(Guid UserId)
        {
            var entity = _userRepository.GetById(UserId);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserViewModel request)
        {

            // _userRepository.Update(user);
            // await _uow.SaveChangesAsync();
            // return NoContent();

            var entity = _userRepository.GetById(request.Id);

            if (request.Id != entity.Id)
            {
                return BadRequest();
            }

            if (entity == null) return NotFound();



            entity.Name = request.Name;
            entity.BirthDate = request.BirthDate;
            entity.IsAdm = request.IsAdm;
            entity.Email = request.Email;
            entity.Password = request.Password;
            entity.BankAccount = request.BankAccount;
            entity.AgencyNumber = request.AgencyNumber;


            _userRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{UserId}")]

        public async Task<IActionResult> Delete(Guid UserId)
        {
            var entity = _userRepository.GetById(UserId);

            if (entity == null)
            {
                return BadRequest();
            }

            _userRepository.Delete(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }

    }
}