using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Domain.Models.UserModel
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        void Delete (User user);
    }
}