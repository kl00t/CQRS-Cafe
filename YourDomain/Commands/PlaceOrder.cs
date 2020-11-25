using Events.Cafe;
using System;
using System.Collections.Generic;

namespace YourDomain.Commands
{
    public class PlaceOrder
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }
}
