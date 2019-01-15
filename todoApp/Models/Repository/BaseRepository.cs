﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace todoApp.Models
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        public Task Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
