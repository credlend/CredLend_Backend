using CredLend.Domain.Dto;
using CredLend.Domain.DTOs;
using CredLend.Domain.Requests;
using Domain.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetById(Guid id);
        Task<RegisterResponseDTO> Register(UserRequest dto);
        Task<LoginResponseDTO> Login (UserLoginDTO loginDto);
        void Delete(Guid id);
        Task<string> GenerateJWToken(User user);
    }
}
