using System;

namespace Events.Cafe
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
