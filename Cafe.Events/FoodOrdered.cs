using System;
using System.Collections.Generic;

namespace Cafe.Events
{
    public class FoodOrdered
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }
}
