using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("19501010", CalendarDate.Parse("19501010").ToString(false));
            Assert.AreEqual("1950-10-10", CalendarDate.Parse("1950-10-10").ToString(true));

            // Reduced
            Assert.AreEqual("1950-10", CalendarDate.Parse("1950-10").ToString(true));
            Assert.AreEqual("1950", CalendarDate.Parse("1950").ToString(false));
            Assert.AreEqual("19", CalendarDate.Parse("19").ToString(false));

            // Expanded
            Assert.AreEqual("+19501010", CalendarDate.Parse("+19501010").ToString(false, true));
            Assert.AreEqual("+119501010", CalendarDate.Parse("+119501010", 5).ToString(false, true, 5));
            Assert.AreEqual("+1950-10-10", CalendarDate.Parse("+1950-10-10").ToString(true, true));
            Assert.AreEqual("+11950-10-10", CalendarDate.Parse("+11950-10-10", 5).ToString(true, true, 5));
            Assert.AreEqual("+1950-10", CalendarDate.Parse("+1950-10").ToString(true, true));
            Assert.AreEqual("+11950-10", CalendarDate.Parse("+11950-10", 5).ToString(true, true, 5));
            Assert.AreEqual("+1950", CalendarDate.Parse("+1950").ToString(false, true));
            Assert.AreEqual("+11950", CalendarDate.Parse("+11950", 5).ToString(false, true, 5));
            Assert.AreEqual("+19", CalendarDate.Parse("+19").ToString(false, true));
            Assert.AreEqual("+019", CalendarDate.Parse("+019", 5).ToString(false, true, 5));
        }
    }
}