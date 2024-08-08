using AutoMapper;
using CredLend.Domain.Dto;
using CredLend.Domain.DTOs;
using CredLend.Domain.Requests;
using CredLend.Service.Interfaces;
using Domain.Core.Data;
using Domain.Models.UserModel;
using Google.Apis.Auth;
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
        private readonly IConfigurationSection _goolgeSettings;

        public UserService(IConfiguration config,
            UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDataContext context)
        {
            _config = config;
            _userManager = userManager;
            _SignInManager = signInManager;
            _context = context;
            _goolgeSettings = _config.GetSection("GoogleAuthSettings");
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

        public async Task<RegisterResponseDTO> Register(UserRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                return new RegisterResponseDTO
                {
                    Id = Guid.Empty,
                    UserName = existingUser.UserName,
                    CompleteName = existingUser.CompleteName,
                    Email = existingUser.Email,
                    Token = null,
                    IsSucceded = false,
                    UserAlreadyExists = true,
                    ExternalLogin = false
                };
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                CompleteName = request.CompleteName,
                CPF = request.CPF,
                BirthDate = request.BirthDate,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            var response = new RegisterResponseDTO();

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                await _userManager.AddToRoleAsync(appUser, "USER");

                response.Id = Guid.Parse(appUser.Id);
                response.UserName = appUser.UserName;
                response.CompleteName = appUser.CompleteName;
                response.Email = appUser.Email;
                response.Token = GenerateJWToken(appUser).Result;
                response.IsSucceded = true;
                response.UserAlreadyExists = false;
                response.ExternalLogin = false;

                return response;
            }

            response.Id = Guid.Empty;
            response.UserName = null;
            response.CompleteName = null;
            response.Email = null;
            response.Token = null;
            response.IsSucceded = false;
            response.UserAlreadyExists = false;
            response.ExternalLogin = false;

            return response;
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
                        Email = appUser.Email,
                        Token = GenerateJWToken(appUser).Result,
                        IsSucceded = true,
                        ExternalLogin = false
                    };

                    return userToReturn;
                }
                else
                {
                    var userToReturn = new LoginResponseDTO
                    {
                        Id = Guid.Empty,
                        UserName = null,
                        CompleteName = null,
                        Email = null,
                        Token = null,
                        IsSucceded = false,
                        ExternalLogin = false
                    };

                    return userToReturn;
                }
            }

            var response = new LoginResponseDTO
            {
                Id = Guid.Empty,
                UserName = null,
                CompleteName = null,
                Email = null,
                Token = null,
                IsSucceded = false,
                ExternalLogin = false
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

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDTO externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }

        public async Task<AuthResponseDTO> CreateExternalLogin(ExternalAuthDTO externalAuth)
        {
            var payload = await VerifyGoogleToken(externalAuth);

            if (payload == null)
                throw new Exception("Autenticação externa inválida.");

            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    string givenName = string.Join("", payload.GivenName.Split(new char[] { ' ' }));

                    string familyname = string.Join("", payload.FamilyName.Split(new char[] { ' ' }));

                    user = new User
                    {
                        Email = payload.Email,
                        CompleteName = payload.Name,
                        UserName = givenName + familyname.Trim(),
                        IsActive = true,
                        NormalizedEmail = payload.Email.ToUpper(),
                        NormalizedUserName = (givenName + familyname.Trim()).ToUpper(),
                    };
                    var userResult = await _userManager.CreateAsync(user);

                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Ocorreu um erro ao criar um usuário.");
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, "USER");

                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Ocorreu um erro ao adicionar usuário à role USER.");
                    }

                    var loginResult = await _userManager.AddLoginAsync(user, info);

                    if (!loginResult.Succeeded)
                    {
                        throw new Exception($"Ocorreu um erro ao adicionar login externo ao usuário.");
                    }
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                throw new Exception("Autenticação externa inválida..");

            var token = await GenerateJWToken(user);

            var response = new AuthResponseDTO
            {
                Token = token,
                UserName = user.UserName,
                CompleteName = user.CompleteName,
                Email = user.Email,
                Id = Guid.Parse(user.Id),
                IsAuthSuccessful = true,
                ExternalLogin = true,
            };

            return response;
        }
    }
}
