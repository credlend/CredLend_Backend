using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Data;
using Domain.Models.Identity;

namespace Domain.Models.RoleModel
{ 
    public interface IRoleRepository: IRepository<Role, Guid>
    {
    }
}