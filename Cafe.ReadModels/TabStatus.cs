using System;
using System.Collections.Generic;

namespace Cafe.ReadModels
{
    public partial class OpenTabs
    {
        public class TabStatus
        {
            public Guid TabId;
            public int TableNumber;
            public List<TabItem> ToServe;
            public List<TabItem> InPreparation;
            public List<TabItem> Served;
        }
    }
}
