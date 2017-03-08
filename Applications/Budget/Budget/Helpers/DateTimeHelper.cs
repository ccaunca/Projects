using RoyT.TimePicker;
using System;

namespace Budget.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime PstNow()
        {
            return DateTime.UtcNow.AddHours(-8);
        }
        public static DateTime SetDateTime(DateTime date, DigitalTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
        }
    }
}
