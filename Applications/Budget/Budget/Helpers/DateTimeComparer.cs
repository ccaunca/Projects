using System;
using System.Collections.Generic;

namespace Budget.Helpers
{
    public class DateTimeComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Month == y.Month && x.Day == y.Day && x.Year == y.Year;
        }

        public int GetHashCode(DateTime obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashDateTime = obj.Date == null ? 0 : obj.Date.GetHashCode();
            return hashDateTime;
        }
    }
}
