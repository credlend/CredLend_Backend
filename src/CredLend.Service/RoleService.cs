using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CredLend.Domain.Dto;
using CredLend.Service.Interfaces;
using Domain.Models.Identity;
using Domain.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace CredLend.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Add(RoleDTO roleDto)
        {
            await _roleManager.CreateAsync(new Role { Name = roleDto.Name });
        }

        public async Task Update(UpdateUserDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if (user != null)
            {
                if (userDto.Deleted)
                    await _userManager.RemoveFromRoleAsync(user, userDto.Role);
                else
                    await _userManager.AddToRoleAsync(user, userDto.Role);
            }
        }
    }
}