using System.Threading.Tasks;
using todoApp.Controllers;
using todoApp.Models.Persistance;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Models;
using todoApp.Models.Repository;
using System;

namespace todoApp.Tests
{
    public class TodoControllerTests
    {
        [Fact]
        public async Task Test1Async()
        {
            var itemlist = new List<TodoItem>()
            {
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test1", IsComplete = false},
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test2", IsComplete = false},
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test3", IsComplete = false}
            };
            var uowMoq = new Mock<IUnitOfWork>();
            var todoItemsRepoMoq = new Mock<ITodoItemRepository>();
            todoItemsRepoMoq.Setup(x => x.FindAllAsync()).ReturnsAsync(itemlist);
            uowMoq.Setup(x => x.TodoItems).Returns(todoItemsRepoMoq.Object);

            var controller = new TodoController(uowMoq.Object);


            var result = await controller.GetTodoItems();

            Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
        }
    }
}
