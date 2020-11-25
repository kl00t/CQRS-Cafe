using Cafe.Events;
using System;
using System.Collections.Generic;

namespace Cafe.Domain.Commands
{
    public class PlaceOrder
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }
}
