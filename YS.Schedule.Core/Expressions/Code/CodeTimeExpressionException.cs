using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Schedule.Expressions.Code
{

    [Serializable]
    public class CodeTimeExpressionException : Exception
    {
        public CodeTimeExpressionException() { }
        public CodeTimeExpressionException(string message) : base(message) { }
        public CodeTimeExpressionException(string message, Exception inner) : base(message, inner) { }
        protected CodeTimeExpressionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
