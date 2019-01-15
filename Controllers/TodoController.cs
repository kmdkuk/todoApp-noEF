using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using todoApp.Models;
using todoApp.Models.Persistance;
using todoApp.Models.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
       
        private readonly IUnitOfWork _uow;

        public TodoController(IUnitOfWork uow)
        {
            _uow = uow;
            if (_uow.TodoItemRepository.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _uow.TodoItemRepository.Insert(new TodoItem { Name = "Item1" , IsComplete = false});
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _uow.TodoItemRepository.GetAllAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(String id)
        {
            var todoItem = _uow.TodoItemRepository.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }



}
