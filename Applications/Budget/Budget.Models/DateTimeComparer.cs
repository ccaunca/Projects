using System;
using System.Collections.Generic;

namespace Budget.Models
{
    public class DateTimeComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Month == y.Month && x.Day == y.Day && x.Year == y.Year;
        }

        public int GetHashCode(DateTime obj)
        {
            return base.GetHashCode();
        }
    }
}
