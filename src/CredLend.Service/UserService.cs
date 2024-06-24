using AutoMapper;
using CredLend.Domain.Dto;
using CredLend.Domain.DTOs;
using CredLend.Domain.Requests;
using CredLend.Service.Interfaces;
using Domain.Core.Data;
using Domain.Models.UserModel;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        public SignInManager<User> _SignInManager;
        private readonly ApplicationDataContext _context;

        public UserService(IConfiguration config,
            UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDataContext context)
        {
            _config = config;
            _userManager = userManager;
            _SignInManager = signInManager;
            _context = context;
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null || !user.IsActive)
            {
                return null;
            }

            var userDTO = new UserDTO
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                Email = user.Email,
                CompleteName = user.CompleteName,
                CPF = user.CPF,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive
            };

            return userDTO;
        }

        public async Task<RegisterResponseDTO> Register(UserRequest dto)
        {

            var user = new User
            {
                UserName = dto.UserName,
                NormalizedEmail = dto.Email,
                Email = dto.Email,
                CompleteName = dto.CompleteName,
                CPF = dto.CPF,
                BirthDate = dto.BirthDate,
                IsActive = true
            };


            var result = await _userManager.CreateAsync(
                user, dto.Password
            );

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());
                var response = new RegisterResponseDTO
                {
                    IsSucceded = true,
                    AuthToken = GenerateJWToken(appUser).Result
                };

                return response;

            } else {
                var response = new RegisterResponseDTO
                {
                    IsSucceded = false,
                    AuthToken = null
                };

                return response;
            }
        }


        public async Task<LoginResponseDTO> Login(UserLoginDTO userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user.IsActive)
            {
                var result = await _SignInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == user.Email.ToUpper());

                    var userToReturn = new LoginResponseDTO
                    {
                        Id = Guid.Parse(appUser.Id),
                        UserName = appUser.UserName,
                        CompleteName = appUser.CompleteName,
                        Token = GenerateJWToken(appUser).Result,
                        IsSucceded = true,
                        IsActive = true,
                    };

                    return userToReturn;
                }
                else {
                    var userToReturn = new LoginResponseDTO
                    {
                        Id = Guid.Empty,
                        UserName = null,
                        CompleteName = null,
                        Token = null,
                        IsSucceded = false,
                        IsActive = false,
                    };

                    return userToReturn;
                }
            }

            var response = new LoginResponseDTO
            {
                UserName = null,
                Token = null,
                IsSucceded = false,
                IsActive = false,
            };

            return response;
        }

        public async void Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
           {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
           };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
