using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Domain.Models.RoleModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role, Guid>, IRoleRepository
    {
        public RoleRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}