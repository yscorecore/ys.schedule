using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace YS.Schedule.Expressions.Code
{
    public sealed class CodeTimeExpression : ITimeExpression, IDisposable
    {
        private const string TRUE_EXPRESSION = "1=1";
        private const string CALC_COLUMN_NAME = "result";
        private static Regex TIME_PART_REG = new Regex("[a-zA-Z]+");
        private static Dictionary<string, string> ReplaceDic = new Dictionary<string, string>()
        {
            ["=="] = "=",
            ["!="] = "<>",
            ["&&"] = " And ",
            ["&"] = " And ",
            ["||"] = " Or ",
            ["|"] = " Or ",
            ["!"] = "Not "
        };
        private static ISet<char> AllValidateChars = new HashSet<char>(new char[] { '<', '>', '!', '=', '%', '+', '-', '*', '/', '&', '|', '(', ')', ' ', '\t' });

        private DataTable dataTable;
        private DataRow timeRow;
        private DataColumn resultColumn;
        private Dictionary<DataColumn, Func<DateTimeOffset, int>> timeParts = new Dictionary<DataColumn, Func<DateTimeOffset, int>>();

        public CodeTimeExpression(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            this.InitDataTableObjects(expression);
            this.Expression = expression;
        }
        public string Expression { get; private set; }

        public bool CanMatch(DateTimeOffset dateTimeOffset)
        {
            lock (this)
            {
                this.FillData(dateTimeOffset);
                return Convert.ToBoolean(timeRow[resultColumn]);
            }
        }

        public void Dispose()
        {
            if (this.dataTable != null)
            {
                this.dataTable.Dispose();
            }
        }

        private void FillData(DateTimeOffset dateTimeOffset)
        {
            foreach (var kv in this.timeParts)
            {
                timeRow[kv.Key] = kv.Value(dateTimeOffset);
            }
        }
        private void InitDataTableObjects(string expression)
        {
            ValidateChars(expression);
            this.dataTable = new DataTable(expression);
            var parts = TIME_PART_REG.Matches(expression)
                        .Cast<Match>()
                        .Select(p => p.Value)
                        .Distinct()
                        .ToList();
            ValidateTimeParts(parts);

            var fieldColumns = parts.Select(p => new DataColumn(p, typeof(int))).ToArray();
            this.dataTable.Columns.AddRange(fieldColumns);
            Array.ForEach(fieldColumns, p => { timeParts[p] = TimeParts.Handlers[p.ColumnName]; });

            // Add Calc column
            this.CreateCalcDataColumn(expression);

            this.timeRow = dataTable.NewRow();
            this.dataTable.Rows.Add(timeRow);

            this.TestExpression();
        }
        private void CreateCalcDataColumn(string expression)
        {
            var dataTableExpression = string.IsNullOrWhiteSpace(expression) ?
                            TRUE_EXPRESSION :
                            expression.Replace(ReplaceDic, StringComparison.InvariantCultureIgnoreCase);
            try
            {
                this.resultColumn = dataTable.Columns.Add(CALC_COLUMN_NAME, typeof(object), dataTableExpression);
            }
            catch (Exception ex)
            {
                throw new CodeTimeExpressionException(ex.Message, ex);
            }
        }
        private static void ValidateTimeParts(IEnumerable<string> timeParts)
        {
            var unknowPart = timeParts.Except(TimeParts.Handlers.Keys).FirstOrDefault();
            if (!string.IsNullOrEmpty(unknowPart))
            {
                throw new CodeTimeExpressionException($"Unknow time part '{unknowPart}'.");
            }
        }
        private static void ValidateChars(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsLetterOrDigit(expression[i]))
                {
                    continue;
                }
                if (!AllValidateChars.Contains(expression[i]))
                {
                    throw new CodeTimeExpressionException($"Illegal character at position {i + 1}.");
                }
            }
        }
        private void TestExpression()
        {
            this.FillData(DateTimeOffset.Now);
            var testObj = timeRow[resultColumn];
            if(testObj.GetType()!=typeof(bool))
            {
                throw new CodeTimeExpressionException("The expression should return a bool value.");
            }
        }

    }
}
