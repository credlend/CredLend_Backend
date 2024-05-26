using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    public interface IRepository<TEntity, Tkey> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(Tkey id);
        void Update(TEntity entity);
        Task<IQueryable<TEntity>> GetAll();
        void SaveChangesAsync();
    }
}