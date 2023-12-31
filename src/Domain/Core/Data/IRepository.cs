using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    public interface IRepository<TEntity, Tkey> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity entity, Tkey id);
        Task<TEntity> GetById(Tkey id);
        void Update(TEntity entity);
        Task<IQueryable<TEntity>> GetAll();
        IUnitOfWork UnitOfWork { get; }
    }
}