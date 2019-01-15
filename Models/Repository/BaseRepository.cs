using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace todoApp.Models
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(String id)
        {
            throw new NotImplementedException();
        }

        public TEntity FindAsync(String id)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(String id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
