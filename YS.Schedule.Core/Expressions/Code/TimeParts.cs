using System;
using System.Data;
using System.Collections.Generic;
namespace YS.Schedule.Expressions.Code
{
    internal class TimeParts
    {
        static TimeParts()
        {
            RegisterHandler(p => p.Year, "year", "Year", "y");
            RegisterHandler(p => p.Month, "month", "Month", "M");
            RegisterHandler(p => p.Day, "day", "Day", "d");
            RegisterHandler(p => p.Hour, "hour", "Hour", "h");
            RegisterHandler(p => p.Minute, "minute", "Minute", "m");
            RegisterHandler(p => p.Second, "second", "second", "s");
            RegisterHandler(p => (int)p.DayOfWeek, "dayofweek", "DayOfWeek", "dow");
            RegisterHandler(p => p.DayOfYear, "dayofyear", "DayOfYear", "doy");
            RegisterHandler(HourOfYear, "hourofyear", "HourOfYear", "hoy");
            RegisterHandler(HourOfMonth, "hourofmonth", "HourOfMonth", "hoM");
            RegisterHandler(HourOfWeek, "hourofweek", "HourOfWeek", "how");
            RegisterHandler(MiniteOfYear, "miniteofyear", "MiniteOfYear", "moy");
            RegisterHandler(MiniteOfMonth, "miniteofmonth", "MiniteOfMonth", "moM");
            RegisterHandler(MiniteOfDay, "miniteofday", "MiniteOfDay", "mod");
            RegisterHandler(MiniteOfWeek, "miniteofweek", "MiniteOfWeek", "mow");
            RegisterHandler(SecondOfYear, "secondofyear", "SecondOfYear", "soy");
            RegisterHandler(SecondOfMonth, "secondofmonth", "SecondOfMonth", "soM");
            RegisterHandler(SecondOfDay, "secondofday", "SecondOfDay", "sod");
            RegisterHandler(SecondOfWeek, "secondofweek", "SecondOfWeek", "sow");
            RegisterHandler(SecondOfHour, "secondofhour", "SecondOfHour", "soh");
        }
        public static Dictionary<string, Func<DateTimeOffset, int>> Handlers = new Dictionary<string, Func<DateTimeOffset, int>>();

        private static void RegisterHandler(Func<DateTimeOffset, int> func, params string[] names)
        {
            Array.ForEach(names, name => { Handlers[name] = func; });
        }
        private static int HourOfYear(DateTimeOffset dateTime)
        {
            return dateTime.DayOfYear * dateTime.Hour;
        }
        private static int HourOfMonth(DateTimeOffset dateTime)
        {
            return dateTime.Day * 24 + dateTime.Hour;
        }
        private static int HourOfWeek(DateTimeOffset dateTime)
        {
            return (int)dateTime.DayOfWeek * 24 + dateTime.Hour;
        }
        private static int MiniteOfYear(DateTimeOffset dateTime)
        {
            return HourOfYear(dateTime) * 60 + MiniteOfDay(dateTime);
        }
        private static int MiniteOfMonth(DateTimeOffset dateTime)
        {
            return HourOfMonth(dateTime) * 60 + MiniteOfDay(dateTime);

        }
        private static int MiniteOfDay(DateTimeOffset dateTime)
        {
            return dateTime.Hour * 60 + dateTime.Minute;
        }
        private static int MiniteOfWeek(DateTimeOffset dateTime)
        {
            return HourOfWeek(dateTime) * 60 + MiniteOfDay(dateTime);
        }
        private static int SecondOfYear(DateTimeOffset dateTime)
        {
            return MiniteOfYear(dateTime) * 60 + dateTime.Second;
        }
        private static int SecondOfMonth(DateTimeOffset dateTime)
        {
            return MiniteOfMonth(dateTime) * 60 + dateTime.Second;
        }
        private static int SecondOfDay(DateTimeOffset dateTime)
        {
            return MiniteOfDay(dateTime) * 60 + dateTime.Second;
        }
        private static int SecondOfWeek(DateTimeOffset dateTime)
        {
            return MiniteOfWeek(dateTime) * 60 + dateTime.Second;
        }
        private static int SecondOfHour(DateTimeOffset dateTime)
        {
            return dateTime.Minute * 60 + dateTime.Second;
        }
       
    }
}