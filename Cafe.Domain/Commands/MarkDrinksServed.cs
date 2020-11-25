using System;
using System.Collections.Generic;

namespace Cafe.Domain.Commands
{
    public class MarkDrinksServed
    {
        public Guid Id;
        public List<int> MenuNumbers;
    }
}
