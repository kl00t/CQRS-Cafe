using System;
using System.Collections.Generic;

namespace YourDomain.Commands
{
    public class MarkDrinksServed
    {
        public Guid Id;
        public List<int> MenuNumbers;
    }
}
