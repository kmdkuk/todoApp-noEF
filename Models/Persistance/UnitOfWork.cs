using Microsoft.Extensions.Configuration;
using todoApp.Models.Repository;

namespace todoApp.Models.Persistance
{
    public class UnitOfWork:IUnitOfWork
    {
        private ITodoItemRepository todoItemRepository;

        public ITodoItemRepository TodoItemRepository
        {
            get
            {

                if (todoItemRepository == null)
                {
                    todoItemRepository = new TodoItemRepository(_configuration);
                }
                return todoItemRepository;
            }
        }

        private IConfiguration _configuration;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    }
}
