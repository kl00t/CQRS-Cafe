using System;

namespace Cafe.Events
{
    /// <summary>
    /// Tab Opened Event
    /// </summary>
    public class TabOpened
    {
        public Guid Id;
        public int TableNumber;
        public string Waiter;
    }
}
