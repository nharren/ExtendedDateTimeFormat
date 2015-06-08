using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateTimeDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P12341212T121212", CalendarDateTimeDuration.Parse("P12341212T121212").ToString(false));
            Assert.AreEqual("P1234-12-12T12:12:12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12:12").ToString(true));

            // Reduced
            Assert.AreEqual("P12341212T1212", CalendarDateTimeDuration.Parse("P12341212T1212").ToString(false));
            Assert.AreEqual("P1234-12-12T12:12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12").ToString(true));
            Assert.AreEqual("P12341212T12", CalendarDateTimeDuration.Parse("P12341212T12").ToString(false));

            // Fractional
            Assert.AreEqual("P12341212T121212,12", CalendarDateTimeDuration.Parse("P12341212T121212,12").ToString(false, fractionLength: 2));
            Assert.AreEqual("P1234-12-12T12:12:12.12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12:12.12").ToString(true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P12341212T1212,12", CalendarDateTimeDuration.Parse("P12341212T1212,12").ToString(false, fractionLength: 2));
            Assert.AreEqual("P1234-12-12T12:12.12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12.12").ToString(true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P12341212T12,12", CalendarDateTimeDuration.Parse("P12341212T12,12").ToString(false, fractionLength: 2));

            // Expanded
            Assert.AreEqual("P+12341212T121212", CalendarDateTimeDuration.Parse("P+12341212T121212").ToString(false, true));
            Assert.AreEqual("P+11234-12-12T12:12:12", CalendarDateTimeDuration.Parse("P+11234-12-12T12:12:12", 5).ToString(true, true, 5));
            Assert.AreEqual("P+12341212T1212", CalendarDateTimeDuration.Parse("P+12341212T1212").ToString(false, true));
            Assert.AreEqual("P+11234-12-12T12:12", CalendarDateTimeDuration.Parse("P+11234-12-12T12:12", 5).ToString(true, true, 5));
            Assert.AreEqual("P+12341212T12", CalendarDateTimeDuration.Parse("P+12341212T12").ToString(false, true));
            Assert.AreEqual("P-112341212T121212,12", CalendarDateTimeDuration.Parse("P-112341212T121212,12", 5).ToString(false, true, 5, 2));
            Assert.AreEqual("P-1234-12-12T12:12:12.12", CalendarDateTimeDuration.Parse("P-1234-12-12T12:12:12.12").ToString(true, true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P-112341212T1212,12", CalendarDateTimeDuration.Parse("P-112341212T1212,12", 5).ToString(false, true, 5, 2));
            Assert.AreEqual("P-1234-12-12T12:12.12", CalendarDateTimeDuration.Parse("P-1234-12-12T12:12.12").ToString(true, true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P-112341212T12,12", CalendarDateTimeDuration.Parse("P-112341212T12,12", 5).ToString(false, true, 5, 2));
        }
    }
}
