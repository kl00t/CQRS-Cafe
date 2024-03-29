﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cafe.Events;
using Edument.CQRS;

namespace Cafe.ReadModels
{
    public partial class ChefTodoList : IChefTodoListQueries,
        ISubscribeTo<FoodOrdered>,
        ISubscribeTo<FoodPrepared>
    {
        private List<TodoListGroup> todoList = new List<TodoListGroup>();

        public List<TodoListGroup> GetTodoList()
        {
            lock (todoList)
                return (from grp in todoList
                        select new TodoListGroup
                        {
                            Tab = grp.Tab,
                            Items = new List<TodoListItem>(grp.Items)
                        }).ToList();
        }

        public void Handle(FoodOrdered e)
        {
            var group = new TodoListGroup
            {
                Tab = e.Id,
                Items = new List<TodoListItem>(
                    e.Items.Select(i => new TodoListItem
                    {
                        MenuNumber = i.MenuNumber,
                        Description = i.Description
                    }))
            };

            lock (todoList)
                todoList.Add(group);
        }

        public void Handle(FoodPrepared e)
        {
            lock (todoList)
            {
                var group = todoList.First(g => g.Tab == e.Id);
                
                foreach (var num in e.MenuNumbers)
                    group.Items.Remove(
                        group.Items.First(i => i.MenuNumber == num));

                if (group.Items.Count == 0)
                    todoList.Remove(group);
            }
        }
    }
}
