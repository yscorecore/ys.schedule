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
        [DataRow("     ", "2020-03-02", true)]
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

        [DataRow(null, typeof(ArgumentNullException), null)]
        [DataRow("Abc>5", typeof(CodeTimeExpressionException), "Unknow time part 'Abc'.")]

        [DataTestMethod]
        public void ShouldThrowExceptionWhenNewCodeTimeExpression(string expression, Type exceptionType, string exceptionMessage)
        {
            try
            {
                using (new CodeTimeExpression(expression))
                {
                }
            }
#pragma warning disable CA1031 // 不捕获常规异常类型
            catch (Exception ex)
#pragma warning restore CA1031 // 不捕获常规异常类型
            {
                Assert.AreEqual(exceptionType, ex.GetType());
                if (exceptionMessage != null)
                {
                    Assert.AreEqual(exceptionMessage, ex.Message);
                }
            }


        }
    }
}
