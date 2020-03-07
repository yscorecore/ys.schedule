using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
using YS.Schedule.Expressions.Code;
using System.Globalization;
namespace YS.Schedule.Expressions
{
    [TestClass]
    public class CodeTimeExpressionTest
    {
        [DataRow("", "2020-03-02", true)]
        [DataRow("     \t ", "2020-03-02", true)]
        [DataRow("y=2020", "2020-03-02", true)]
        [DataRow("y=2020", "2019-03-02", false)]
        [DataRow("y!=2020", "2020-03-02", false)]
        [DataRow("y>2020", "2020-03-02", false)]
        [DataRow("y>=2020", "2020-03-02", true)]
        [DataRow("y<2020", "2020-03-02", false)]
        [DataRow("y<=2020", "2020-03-02", true)]
        [DataRow("y+1>2020", "2020-03-02", true)]
        [DataRow("y-1<2020", "2020-03-02", true)]
        [DataRow("y*2=4040", "2020-03-02", true)]
        [DataRow("y/2=1010", "2020-03-02", true)]
        [DataRow("y%100=20", "2020-03-02", true)]
        [DataRow("y=2020&M=3&d=2", "2020-03-02", true)]
        [DataRow("y+(M+d)*2=2030", "2020-03-02", true)]
        [DataRow("M=3&d=2&h=8&m=40&s=23", "2020-03-02 08:40:23", true)] 
        [DataTestMethod]
        public void ShouldReturnExpectedValueWhenMatch(string expression, string dateTimeText, bool expected)
        {
            using (var timeExpression = new CodeTimeExpression(expression))
            {
                var dateTime = DateTimeOffset.Parse(dateTimeText, CultureInfo.InvariantCulture);
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

        //[DataRow(null, typeof(ArgumentNullException), null)]
        [DataRow("Abc>5", "Unknow time part 'Abc'.")]
        [DataRow("[y>2015]", "Illegal character at position 1.")]
        [DataRow("(y>2015))", null)]
        [DataRow("y*2", "The expression should return a bool value.")]
        [DataTestMethod]
        public void ShouldThrowExceptionWhenNewCodeTimeExpression(string expression, string exceptionMessage)
        {
            var exception = Assert.ThrowsException<CodeTimeExpressionException>(() =>
            {
                using (new CodeTimeExpression(expression))
                {
                }
            });
            if (exceptionMessage != null)
            {
                Assert.AreEqual(exceptionMessage, exception.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionWhenNewCodeTimeExpressionGiveExpressionIsNull()
        {
            using (new CodeTimeExpression(null))
            {
            }
        }
    }
}
