using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IUnitOfWork uow, IMapper mapper)
        {
            _userRepository = userRepository;
            _uow = uow;
            _mapper = mapper;
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

            var user = _mapper.Map<User>(request);

            var listUser = await _userRepository.GetAll();

            listUser.ToList();

            foreach (var item in listUser)
            {
                bool verifica = user.Email.Contains(item.Email, StringComparison.OrdinalIgnoreCase);
                if (verifica)
                {
                    return BadRequest("Este usuário já existe no banco de dados");
                }
            }

            _userRepository.Add(user);
            await _uow.SaveChangesAsync();
            return Ok(user);
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
        public async Task<IActionResult> Put([FromBody] UserViewModel user)
        {
            var entity = _userRepository.GetById(user.Id);

            if (user.Id != entity.Id)
            {
                return BadRequest();
            }

            if (entity == null) return NotFound();

            _mapper.Map(user, entity);

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
                return NotFound();
            }

            _userRepository.Delete(entity);
            await _uow.SaveChangesAsync();
            return Ok(entity);
        }

    }
}