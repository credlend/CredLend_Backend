using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.BaseModel
{
    public abstract class Base
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}