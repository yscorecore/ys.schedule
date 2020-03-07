using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
namespace YS.Schedule.Expressions.Code
{
    [TestClass]
    public class TimePartsTest
    {
        [DataRow("2019-02-01", 28)]
        [DataRow("2019-02-28", 1)]
        [DataRow("2020-02-01", 29)]
        [DataRow("2020-02-29", 1)]
        [DataRow("2020-03-01", 31)]
        [DataRow("2020-03-31", 1)]
        [DataTestMethod]
        public void ShouldGetExpectedValueWhenGetLastDayOfMonth(string date, int expected)
        {
            var dateOffset = DateTimeOffset.Parse(date);
            Assert.AreEqual(expected, TimeParts.LastDayOfMonth(dateOffset));
        }

        [DataRow("2019-01-01", 365)]
        [DataRow("2019-12-31", 1)]
        [DataRow("2020-01-01", 366)]
        [DataRow("2020-12-31", 1)]
        [DataTestMethod]
        public void ShouldGetExpectedValueWhenGetLastDayOfYear(string date, int expected)
        {
            var dateOffset = DateTimeOffset.Parse(date);
            Assert.AreEqual(expected, TimeParts.LastDayOfYear(dateOffset));
        }

  
  
        [DataRow("2020-12-31", 1)]
        [DataRow("2020-12-30", 1)]
        [DataRow("2020-12-29", 1)]
        [DataRow("2020-12-28", 1)]
        [DataRow("2020-12-27", 1)]
        [DataRow("2020-12-26", 1)]
        [DataRow("2020-12-25", 1)]
        [DataRow("2020-12-24", 2)]
        [DataRow("2020-12-23", 2)]
        [DataRow("2020-12-22", 2)]
        [DataRow("2020-12-04", 4)]
        [DataRow("2020-12-03", 5)]
        [DataTestMethod]
        public void ShouldGetExpectedValueWhenGetLastWeekDayOfMonth(string date, int expected)
        {
            var dateOffset = DateTimeOffset.Parse(date);
            Assert.AreEqual(expected, TimeParts.LastWeekDayOfMonth(dateOffset));
        }

        [DataRow("2020-01-01", 1)]
        [DataRow("2020-01-02", 1)]
        [DataRow("2020-01-03", 1)]
        [DataRow("2020-01-04", 1)]
        [DataRow("2020-01-05", 1)]
        [DataRow("2020-01-06", 1)]
        [DataRow("2020-01-07", 1)]
        [DataRow("2020-01-08", 2)]
        [DataRow("2020-01-09", 2)]
        [DataRow("2020-01-10", 2)]
        [DataRow("2020-01-28", 4)]
        [DataRow("2020-01-29", 5)]
        [DataRow("2020-01-30", 5)]
        [DataRow("2020-01-31", 5)]
        [DataRow("2020-02-01", 5)]
        [DataRow("2020-02-02", 5)]
        [DataRow("2020-02-03", 5)]
        [DataRow("2020-02-04", 5)]
        [DataRow("2020-02-05", 6)]
        [DataTestMethod]
        public void ShouldGetExpectedValueWhenGetWeekDayOfYear(string date, int expected)
        {
            var dateOffset = DateTimeOffset.Parse(date);
            Assert.AreEqual(expected, TimeParts.WeekDayOfYear(dateOffset));
        }

        [DataRow("2020-12-31", 1)]
        [DataRow("2020-12-30", 1)]
        [DataRow("2020-12-29", 1)]
        [DataRow("2020-12-28", 1)]
        [DataRow("2020-12-27", 1)]
        [DataRow("2020-12-26", 1)]
        [DataRow("2020-12-25", 1)]
        [DataRow("2020-12-24", 2)]
        [DataRow("2020-12-23", 2)]
        [DataRow("2020-12-22", 2)]
        [DataRow("2020-12-04", 4)]
        [DataRow("2020-12-03", 5)]
        [DataRow("2020-12-02", 5)]
        [DataRow("2020-12-01", 5)]
        [DataRow("2020-11-30", 5)]
        [DataRow("2020-11-29", 5)]
        [DataRow("2020-11-28", 5)]
        [DataRow("2020-11-27", 5)]
        [DataRow("2020-11-26", 6)]
        [DataTestMethod]
        public void ShouldGetExpectedValueWhenGetLastWeekDayOfYear(string date, int expected)
        {
            var dateOffset = DateTimeOffset.Parse(date);
            Assert.AreEqual(expected, TimeParts.LastWeekDayOfYear(dateOffset));
        }
    }
}