using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TodoApi.Models;

namespace todoApp.Models.Repository
{
    public class TodoItemRepository:IBaseRepository<TodoItem>,ITodoItemRepository
    {
        private string connectionString = null;

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public TodoItemRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public TodoItemRepository()
        {
        }

        public int Count()
        {
            return 0;
        }

        public void Delete(String id)
        {
            throw new NotImplementedException();
        }

        public TodoItem Get(String id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            IEnumerable<TodoItem> result;
            using (IDbConnection db = Connection)
            {
                db.Open();
                var query = "SELECT * FROM [todoApp].[dbo].[TodoItems]";
                result = await db.QueryAsync<TodoItem>(query);
                Console.Out.WriteLine(result);
            }
            return result;
        }

        public void Insert(TodoItem entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Open();
                using (var tran = db.BeginTransaction())
                {
                    try
                    {
                        entity.Id = Guid.NewGuid().ToString("N");
                        String sql = @"INSERT INTO [todoApp].[dbo].[TodoItems] (Id ,Name, IsComplete) VALUES (@Id, @Name, @IsComplete)";
                        db.Execute(sql, entity, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
        }

        public void Update(TodoItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
