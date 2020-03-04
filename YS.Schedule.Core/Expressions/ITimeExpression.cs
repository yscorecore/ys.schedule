using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Schedule.Expressions
{
   public interface ITimeExpression
    {
        string Expression { get; }
        bool CanMatch(DateTimeOffset dateTimeOffset);
    }
}
