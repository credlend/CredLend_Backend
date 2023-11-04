using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.RoleModel
{ 
    public interface IRoleRepository<T> where T : class
    {
          Task<IEnumerable<T>> GetAll();
    }
}