using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class WeekDateTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("1950W101", WeekDate.Parse("1950W101").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1", WeekDate.Parse("1950-W10-1").ToString());

            // Reduced
            Assert.AreEqual("1950W10", WeekDate.Parse("1950W10").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10", WeekDate.Parse("1950-W10").ToString());

            // Expanded
            Assert.AreEqual("+1950W101", WeekDate.Parse("+1950W101").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("+11950W101", WeekDate.Parse("+11950W101", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+1950-W10-1", WeekDate.Parse("+1950-W10-1").ToString(new ISO8601FormatInfo { IsExpanded = true }));
            Assert.AreEqual("+11950-W10-1", WeekDate.Parse("+11950-W10-1", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+1950W10", WeekDate.Parse("+1950W10").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("+11950W10", WeekDate.Parse("+11950W10", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+1950-W10", WeekDate.Parse("+1950-W10").ToString(new ISO8601FormatInfo { IsExpanded = true }));
            Assert.AreEqual("+11950-W10", WeekDate.Parse("+11950-W10", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
        }
    }
}