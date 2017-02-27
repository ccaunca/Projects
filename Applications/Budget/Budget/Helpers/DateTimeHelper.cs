using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime PstNow()
        {
            return DateTime.UtcNow.AddHours(-8);
        }
    }
}
