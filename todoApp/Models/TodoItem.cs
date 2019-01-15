using System;
using todoApp.Models;

namespace TodoApi.Models
{
    public class TodoItem:IBaseEntity
    {
        public TodoItem()
        {
        }

        public String Id { get; set; }
        public String Name { get; set; }
        public bool IsComplete { get; set; }
    }
}