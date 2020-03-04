using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Schedule.Expressions
{
    public interface ITimePart
    {
        string Name { get; }

        bool IgnoreCase { get; }

        int GetPartValue(DateTimeOffset dateTimeOffset);
    }
}
