using System;
using System.Collections.Generic;

namespace Cafe.ReadModels
{
    public interface IChefTodoListQueries
    {
        List<ChefTodoList.TodoListGroup> GetTodoList();
    }
}
