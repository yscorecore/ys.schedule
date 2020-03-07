using System;
using System.Data;
using System.Collections.Generic;
namespace YS.Schedule.Expressions.Code
{
    internal static class Extentions
    {
        internal static Dictionary<string, string> ReplaceDic = new Dictionary<string, string>()
        {
            ["=="] = "=",
            ["!="] = "<>",
            ["&&"] = " And ",
            ["&"] = " And ",
            ["||"] = " Or ",
            ["|"] = " Or "
        };
        public static string ToDataTableExpression(this string originExpression)
        {
            return originExpression.Replace(ReplaceDic, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
