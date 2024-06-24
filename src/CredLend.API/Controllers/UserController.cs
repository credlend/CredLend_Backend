using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CredLend.Domain.Dto;
using CredLend.Domain.Requests;
using CredLend.Domain.ViewModels;
using CredLend.Service.Interfaces;
using Domain.Core.Data;
using Domain.Models.Identity;
using Domain.Models.UserModel;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _service.GetById(id);

                if (response == null)
                {
                    return NotFound("Plano não encontrado");
                }

                var user = new UserViewModel
                {
                    Id = id,
                    Email = response.Email,
                    CPF = response.CPF,
                    CompleteName = response.CompleteName,
                    UserName = response.UserName,
                    BirthDate = response.BirthDate,
                    IsActive = response.IsActive
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRequest request)
        {
            try
            {
                var token = await _service.Register(request);

                if (token.IsSucceded == true && token.AuthToken != null)
                {
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Falha ao registrar usuário. Verifique os dados e tente novamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar as alterações: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Exceção interna: " + ex.InnerException.Message);
                }
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            try
            {
                var user = await _service.Login(userLogin);

                if (user.IsSucceded == false && user.IsActive == false && user.Token == null && user.UserName == null)
                {
                    return BadRequest("Falha ao realizar o login do usuário. Verifique os dados e tente novamente.");

                }
                else
                {
                    return Ok(user);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _service.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}