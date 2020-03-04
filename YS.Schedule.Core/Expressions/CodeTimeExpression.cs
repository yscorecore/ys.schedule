using System;
using System.Data;
namespace YS.Schedule.Expressions
{
    public class CodeTimeExpression : ITimeExpression
    {
        const string MatchResultColumnName = "matchres";
        private DataRow timeRow;
        private DataColumn resultColumn;
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
            timeRow["y"]=dateTimeOffset.Year;
        }
        private void BuildDataTable(string expression)
        {
            var calctable=  new DataTable();
            calctable.Columns.Add(new DataColumn("y",typeof(int)));
            resultColumn= calctable.Columns.Add("res",typeof(bool));
            resultColumn.Expression=expression;
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