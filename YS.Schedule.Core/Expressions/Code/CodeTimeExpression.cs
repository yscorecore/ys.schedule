using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace YS.Schedule.Expressions.Code
{
    public class CodeTimeExpression : ITimeExpression
    {
        const string MatchResultColumnName = "matchres";
        private DataRow timeRow;
        private DataColumn resultColumn;
        private Dictionary<DataColumn, Func<DateTimeOffset, int>> timeParts = new Dictionary<DataColumn, Func<DateTimeOffset, int>>();

        private CodeTimeExpression()
        {
        }
        public string Expression { get; private set; }

        public bool CanMatch(DateTimeOffset dateTimeOffset)
        {
            this.FillData(dateTimeOffset);
            return Convert.ToBoolean(timeRow[resultColumn]);
        }
        private void FillData(DateTimeOffset dateTimeOffset)
        {
            foreach (var kv in this.timeParts)
            {
                timeRow[kv.Key] = kv.Value(dateTimeOffset);
            }
        }
        private void BuildDataTable(string expression)
        {
            var calctable = new DataTable();
            var parts = Regex.Matches(expression, "[a-zA-z]+").Cast<Match>().Select(p => p.Value).Distinct().ToList();
            
            foreach(var part in parts)
            {
                var handler = TimeParts.Handlers[part];
                var column = calctable.Columns.Add(part, typeof(int));
                timeParts.Add(column,handler);
            }
            
            resultColumn = calctable.Columns.Add("res", typeof(bool));
            resultColumn.Expression = expression;
            timeRow = calctable.NewRow();
            calctable.Rows.Add(timeRow);
        }


        public static CodeTimeExpression Parse(string code)
        {
            var res = new CodeTimeExpression();
            res.BuildDataTable(code);
            return res;
        }

    }
}