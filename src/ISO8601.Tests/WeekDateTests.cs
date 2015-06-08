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
            Assert.AreEqual("1950W101", WeekDate.Parse("1950W101").ToString(false));
            Assert.AreEqual("1950-W10-1", WeekDate.Parse("1950-W10-1").ToString(true));

            // Reduced
            Assert.AreEqual("1950W10", WeekDate.Parse("1950W10").ToString(false));
            Assert.AreEqual("1950-W10", WeekDate.Parse("1950-W10").ToString(true));

            // Expanded
            Assert.AreEqual("+1950W101", WeekDate.Parse("+1950W101").ToString(false, true));
            Assert.AreEqual("+11950W101", WeekDate.Parse("+11950W101", 5).ToString(false, true, 5));
            Assert.AreEqual("+1950-W10-1", WeekDate.Parse("+1950-W10-1").ToString(true, true));
            Assert.AreEqual("+11950-W10-1", WeekDate.Parse("+11950-W10-1", 5).ToString(true, true, 5));
            Assert.AreEqual("+1950W10", WeekDate.Parse("+1950W10").ToString(false, true));
            Assert.AreEqual("+11950W10", WeekDate.Parse("+11950W10", 5).ToString(false, true, 5));
            Assert.AreEqual("+1950-W10", WeekDate.Parse("+1950-W10").ToString(true, true));
            Assert.AreEqual("+11950-W10", WeekDate.Parse("+11950-W10", 5).ToString(true, true, 5));
        }
    }
}