using System;
using System.Collections.Generic;
namespace YS.Schedule.Expressions.Code
{
    public class TimeParts
    {
        static TimeParts()
        {
            RegisterHandler(p => p.Year, "year", "Year", "y");
            RegisterHandler(p => p.Month, "month", "Month", "M");
            RegisterHandler(p => p.Day, "day", "Day", "d");
            RegisterHandler(p => p.Hour, "hour", "Hour", "h");
            RegisterHandler(p => p.Minute, "minute", "Minute", "m");
            RegisterHandler(p => p.Second, "second", "second", "s");
            RegisterHandler(p => (int)p.DayOfWeek, "dayofweek", "DayOfWeek", "dw");
            RegisterHandler(p => p.DayOfYear, "dayofyear", "DayOfYear", "dy");
            RegisterHandler(LastDayOfMonth, "lastdayofmonth", "LastDayOfMonth", "ldM");
            RegisterHandler(LastDayOfYear, "lastdayofyear", "LastDayOfYear", "ldy");
            RegisterHandler(WeekDayOfMonth, "weekdayofmonth", "WeekDayOfMonth", "wdM");
            RegisterHandler(WeekDayOfYear, "weekdayofyear", "WeekDayOfYear", "wdy");
            RegisterHandler(LastWeekDayOfMonth, "lastweekdayofmonth", "LastWeekDayOfMonth", "lwdM");
            RegisterHandler(LastWeekDayOfYear, "lastweekdayofyear", "LastWeekDayOfYear", "lwdy");
        }

        public static Dictionary<string, Func<DateTimeOffset, int>> Handlers = new Dictionary<string, Func<DateTimeOffset, int>>();

        public static void RegisterHandler(Func<DateTimeOffset, int> func, params string[] names)
        {
            Array.ForEach(names, name => { Handlers[name] = func; });
        }
        public static int LastDayOfMonth(DateTimeOffset dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - dateTime.Day + 1;
        }
        public static int LastDayOfYear(DateTimeOffset dateTime)
        {
            var firstDayOnNextYear = new DateTimeOffset(new DateTime(dateTime.Year + 1, 1, 1));
            var firstDayofThisYear = new DateTimeOffset(new DateTime(dateTime.Year, 1, 1));
            return (firstDayOnNextYear - firstDayofThisYear).Days - dateTime.DayOfYear + 1;
        }
        public static int WeekDayOfMonth(DateTimeOffset dateTime)
        {
            return (dateTime.Day - 1) / 7 + 1;
        }
        public static int LastWeekDayOfMonth(DateTimeOffset dateTime)
        {
            return (LastDayOfMonth(dateTime) - 1) / 7 + 1;
        }
        public static int WeekDayOfYear(DateTimeOffset dateTime)
        {
            return (dateTime.DayOfYear - 1) / 7 + 1;
        }
        public static int LastWeekDayOfYear(DateTimeOffset dateTime)
        {
            return (LastWeekDayOfYear(dateTime) - 1) / 7 + 1;
        }
    }
}