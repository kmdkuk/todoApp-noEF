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
            if (_uow.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _uow.TodoItems.Add(new TodoItem { Name = "Item1" , IsComplete = false});
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _uow.TodoItems.FindAllAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(String id)
        {
            var todoItem = await _uow.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _uow.TodoItems.Add(todoItem);

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(String id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            await _uow.TodoItems.Update(todoItem);

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(string id)
        {
            var todoItem = await _uow.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _uow.TodoItems.Remove(todoItem);

            return todoItem;
        }
    }
}
