using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateTests
    {
        [TestMethod]
        public void RoundTrip()
        {
            Assert.AreEqual("19501010", CalendarDate.Parse("19501010").ToString(false));
            Assert.AreEqual("1950-10-10", CalendarDate.Parse("1950-10-10").ToString(true));

            Assert.AreEqual("1950-10", CalendarDate.Parse("1950-10").ToString(true));
            Assert.AreEqual("1950", CalendarDate.Parse("1950").ToString(false));
            Assert.AreEqual("19", CalendarDate.Parse("19").ToString(false));

            Assert.AreEqual("+19501010", CalendarDate.Parse("+19501010").ToString(false));
            Assert.AreEqual("+119501010", CalendarDate.Parse("+119501010", 5).ToString(false));
            Assert.AreEqual("+1950-10-10", CalendarDate.Parse("+1950-10-10").ToString(true));
            Assert.AreEqual("+11950-10-10", CalendarDate.Parse("+11950-10-10", 5).ToString(true));

            Assert.AreEqual("+1950-10", CalendarDate.Parse("+1950-10").ToString(true));
            Assert.AreEqual("+11950-10", CalendarDate.Parse("+11950-10", 5).ToString(true));
            Assert.AreEqual("+1950", CalendarDate.Parse("+1950").ToString(false));
            Assert.AreEqual("+11950", CalendarDate.Parse("+11950", 5).ToString(false));
            Assert.AreEqual("+19", CalendarDate.Parse("+19").ToString(false));
            Assert.AreEqual("+019", CalendarDate.Parse("+019", 5).ToString(false));
        }
    }
}