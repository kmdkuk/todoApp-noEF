using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace todoApp.Models
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Get(String id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Insert(TEntity entity);
        void Delete(String id);
        void Update(TEntity entity);
        int Count();
    }
}
