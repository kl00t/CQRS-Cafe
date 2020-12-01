using System.Collections.Generic;

namespace Cafe.ReadModels
{
    public partial class OpenTabs
    {
        private class Tab
        {
            public int TableNumber;
            public string Waiter;
            public List<TabItem> ToServe;
            public List<TabItem> InPreparation;
            public List<TabItem> Served;
        }
    }
}
