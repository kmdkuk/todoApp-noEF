using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace todoApp.Models
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> FindAsync(String id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task Add(TEntity entity);
        Task Remove(TEntity entity);
        Task Update(TEntity entity);
        int Count();
    }
}
