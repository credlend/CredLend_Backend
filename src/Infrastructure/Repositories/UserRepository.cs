using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Domain.Models.UserModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
     public class UserRepository<T> : IUserRepository<T> where T : class
    {
        private readonly ApplicationDataContext _context;

        public UserRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(string id)
        {
        
         return await _context.Set<T>().FindAsync(id);
        
        }


        public List<UserRole> GetUserRoles()
        {
            IQueryable<UserRole> entity =  _context.UserRole
            .Include(p=>p.User)
            .Include(p=>p.Role);
           
            return entity.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}