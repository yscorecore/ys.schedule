using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
namespace YS.Schedule.Expressions
{
    [TestClass]
    public class CodeTimeExpressionTest
    {
        //[DataRow("", "2020-03-02", true)]
        [DataRow("y=2020", "2020-03-02", true)]
        [DataRow("y=2020", "2019-03-02", false)]
        [DataTestMethod]
        public void ShouldReturnExpectedValueWhenMatch(string expression, string dateTimeText, bool expected)
        {
            var timeExpression = CodeTimeExpression.Parse(expression);
            var dateTime = DateTimeOffset.Parse(dateTimeText);
            var matched = timeExpression.CanMatch(dateTime);
            if (expected)
            {
                Assert.IsTrue(matched, $"[{expression}] should match datetime '{dateTimeText}'.");
            }
            else
            {
                Assert.IsFalse(matched, $"[{expression}] should not match datetime '{dateTimeText}'.");
            }
        }
    }
}
