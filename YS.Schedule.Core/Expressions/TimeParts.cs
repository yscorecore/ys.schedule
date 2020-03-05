using System;
using System.Data;
using System.Collections.Generic;
namespace YS.Schedule.Expressions
{
    internal class TimeParts
    {
        public static Dictionary<string, Func<DateTimeOffset, int>> Handlers = new Dictionary<string, Func<DateTimeOffset, int>>
        {
            ["year"] = t => t.Year,
            ["y"] = t => t.Year,
            ["month"] = t => t.Month,
            ["M"] = t => t.Month,
            ["day"] = t => t.Day,
            ["d"] = t => t.Day,
            ["week"] = t => (int)t.DayOfWeek,
            ["w"] = t => (int)t.DayOfWeek,
            ["hour"] = t => t.Hour,
            ["h"] = t => t.Hour,
            ["minute"] = t => t.Minute,
            ["m"] = t => t.Minute,
            ["second"] = t => t.Second,
            ["s"] = t => t.Second,
        };
    }
}