using System;

namespace Cafe.Domain.Commands
{
    /// <summary>
    ///  Open Tab Command
    /// </summary>
    public class OpenTab
    {
        public Guid Id;
        public int TableNumber;
        public string Waiter;
    }
}
