using System;
using System.Collections.Generic;

namespace Cafe.ReadModels
{
    public partial class ChefTodoList
    {
        public class TodoListGroup
        {
            public Guid Tab;
            public List<TodoListItem> Items;
        }
    }
}
