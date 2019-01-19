using System.Threading.Tasks;
using todoApp.Controllers;
using todoApp.Models.Persistance;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Models;
using todoApp.Models.Repository;
using System;
using Xunit.Abstractions;

namespace todoApp.Tests
{
    public class TodoControllerTests
    {
        private readonly ITestOutputHelper _output;
        private List<TodoItem> itemlist;
        private Mock<IUnitOfWork> uowMoq;
        private Mock<ITodoItemRepository> todoItemRepoMoq;
        private TodoController controller;
        public TodoControllerTests(ITestOutputHelper output)
        {
            _output = output;
            itemlist = new List<TodoItem>()
            {
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test1", IsComplete = false},
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test2", IsComplete = false},
                new TodoItem{ Id = Guid.NewGuid().ToString("N"), Name = "Test3", IsComplete = false}
            };
            uowMoq = new Mock<IUnitOfWork>();
            todoItemRepoMoq = new Mock<ITodoItemRepository>();
            todoItemRepoMoq.Setup(x => x.FindAllAsync()).ReturnsAsync(itemlist);
            todoItemRepoMoq.Setup(x => x.FindAsync(It.IsAny<String>()))
                .ReturnsAsync((String id) => itemlist.Find(x => x.Id == id));
            todoItemRepoMoq.SetupAsync(x => x.Add(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item => itemlist.Add(item));
            todoItemRepoMoq.SetupAsync(x => x.Update(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item => 
                {
                    var index = itemlist.FindIndex(x => x.Id == item.Id);
                    itemlist[index] = item;
                });
            todoItemRepoMoq.SetupAsync(x => x.Remove(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item =>itemlist.Remove(item));
            todoItemRepoMoq.Setup(x => x.Count()).Returns(itemlist.Count);
            uowMoq.Setup(x => x.TodoItems).Returns(todoItemRepoMoq.Object);

            controller = new TodoController(uowMoq.Object);
        }

        [Fact(DisplayName = "GET api/todo/{id} 正常系")]
        public async Task OkGetTodoItemTest()
        {
            var targetId = itemlist[0].Id;
            var result = await controller.GetTodoItem(targetId);

            Assert.IsType<ActionResult<TodoItem>>(result);
            Assert.Equal(targetId, result.Value.Id);
        }

        [Fact(DisplayName = "GET api/todo 正常系")]
        public async Task OkGetTodoItemsTest()
        {
            var result = await controller.GetTodoItems();

            Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
        }

        [Fact(DisplayName = "POST api/todo 正常系")]
        public async Task OkPostTodoItemTest()
        {
            var item = new TodoItem { Id = Guid.NewGuid().ToString("N"), Name = "new item", IsComplete = false };
            var result = await controller.PostTodoItem(item);

            Assert.IsType<ActionResult<TodoItem>>(result);
            Assert.Equal(4, itemlist.Count);
        }

        [Fact(DisplayName = "PUT api/todo/{id} 正常系")]
        public async Task OkPutTodoItemTest()
        {
            var targetId = itemlist[0].Id;
            var item = new TodoItem { Id = targetId, Name = "updated item", IsComplete = true };
            var result = await controller.PutTodoItem(targetId, item);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(item.Name, itemlist[0].Name);
            Assert.Equal(item.IsComplete, itemlist[0].IsComplete);
        }

        [Fact(DisplayName = "DELETE api/todo/{id} 正常系")]
        public async Task OkDeleteTodoItemTest()
        {
            var targetId = itemlist[0].Id;
            var item = itemlist[0];

            var result = await controller.DeleteTodoItem(targetId);

            Assert.Equal(2, itemlist.Count);
            Assert.DoesNotContain<TodoItem>(item, itemlist);
        }
    }
}
