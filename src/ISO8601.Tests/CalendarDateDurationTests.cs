using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P12341212", CalendarDateDuration.Parse("P12341212").ToString(false));
            Assert.AreEqual("P1234-12-12", CalendarDateDuration.Parse("P1234-12-12").ToString(true));

            // Reduced
            Assert.AreEqual("P1234-12", CalendarDateDuration.Parse("P1234-12").ToString(true));
            Assert.AreEqual("P1234", CalendarDateDuration.Parse("P1234").ToString(true));
            Assert.AreEqual("P12", CalendarDateDuration.Parse("P12").ToString(false));

            // Expanded
            Assert.AreEqual("P+112341212", CalendarDateDuration.Parse("P+112341212", 5).ToString(false));
            Assert.AreEqual("P+11234-12-12", CalendarDateDuration.Parse("P+11234-12-12", 5).ToString(true));
            Assert.AreEqual("P+11234-12", CalendarDateDuration.Parse("P+11234-12", 5).ToString(true));
            Assert.AreEqual("P+11234", CalendarDateDuration.Parse("P+11234", 5).ToString(true));
            Assert.AreEqual("P+112", CalendarDateDuration.Parse("P+112", 5).ToString(false));
        }
    }
}
