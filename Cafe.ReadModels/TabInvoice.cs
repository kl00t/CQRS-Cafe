using System;
using System.Collections.Generic;

namespace Cafe.ReadModels
{
    public partial class OpenTabs
    {
        public class TabInvoice
        {
            public Guid TabId;
            public int TableNumber;
            public List<TabItem> Items;
            public decimal Total;
            public bool HasUnservedItems;
        }
    }
}
