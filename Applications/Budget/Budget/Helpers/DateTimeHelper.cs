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
        public static bool AreEqual(DateTime date1, DateTime date2)
        {
            return date1.Month == date2.Month && date1.Day == date2.Day && date1.Year == date2.Year;
        }
    }
}
