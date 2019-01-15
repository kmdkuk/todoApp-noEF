using Microsoft.Extensions.Configuration;
using todoApp.Models.Repository;

namespace todoApp.Models.Persistance
{
    public class UnitOfWork:IUnitOfWork
    {
        private ITodoItemRepository todoItems;

        public ITodoItemRepository TodoItems
        {
            get
            {

                if (todoItems == null)
                {
                    todoItems = new TodoItemRepository(_configuration);
                }
                return todoItems;
            }
        }

        private IConfiguration _configuration;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    }
}
