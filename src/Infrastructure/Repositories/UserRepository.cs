using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.UserModel;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository: RepositoryBase<User, Guid>, IUserRepository
    {
        
        public UserRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }

        public void Delete(User user)
        {
            
            _entity.Remove(user);
        }
    }
}