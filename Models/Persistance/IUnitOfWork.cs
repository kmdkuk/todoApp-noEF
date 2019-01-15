using System;
using todoApp.Models.Repository;

namespace todoApp.Models.Persistance
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItems { get;}
    }
}
