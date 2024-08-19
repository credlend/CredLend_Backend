using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CredLend.Domain.Dto;

namespace CredLend.Service.Interfaces
{
    public interface IRoleService
    {
        public Task Add(RoleDTO dto);
    }
}