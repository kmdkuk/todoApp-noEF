using System.Threading.Tasks;
using todoApp.Controllers;
using todoApp.Models.Persistance;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;

namespace todoApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var config = new Mock<IConfiguration>;
            config.Setup(x => x.GetConnectionString("DefaultConnection"))
                .Returns("Data Source=127.0.0.1;Initial Catalog=todoAppTests;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=Password0123");
            var controller = new TodoController(new UnitOfWork(config.Object));
            var result = await controller.GetTodoItems();
        }
    }
}
