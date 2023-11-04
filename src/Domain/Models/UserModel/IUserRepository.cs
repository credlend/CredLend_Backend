using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.Identity;

namespace Domain.Models.UserModel
{
    public interface IUserRepository<T> where T : class
    {
        List<UserRole> GetUserRoles();
        Task<T> GetById(string id);
        Task<bool> SaveChangesAsync(); //Salvar
    }
}