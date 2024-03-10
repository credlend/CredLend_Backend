using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Dto;
using Domain.Models.Identity;
using Domain.Models.RoleModel;
using Domain.Models.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManagers;
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepository _roleRepository;

        public RoleController(RoleManager<Role> roleManagers, UserManager<User> userManager, IRoleRepository roleRepository)
        {
            _roleManagers = roleManagers;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entity = await _roleRepository.GetAll();

                if (entity == null)
                {
                    return NotFound("Nenhum papel encontrado");
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost("CreateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            try
            {
                var retorno = await _roleManagers.CreateAsync(new Role { Name = roleDto.Name });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }

        [HttpPut("UpdateUserRole")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (model.Delete)
                        await _userManager.RemoveFromRoleAsync(user, model.Role);
                    else
                        await _userManager.AddToRoleAsync(user, model.Role);
                }
                else
                {
                    return Ok("Usuário não encontrado");
                }

                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
    }
}