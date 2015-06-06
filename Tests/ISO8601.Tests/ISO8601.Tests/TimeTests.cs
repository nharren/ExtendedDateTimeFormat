using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void RoundTrip()
        {
            // Complete
            Assert.AreEqual("121212", Time.Parse("121212").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("240000", Time.Parse("240000").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T121212", Time.Parse("T121212").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T240000", Time.Parse("T240000").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("12:12:12", Time.Parse("12:12:12").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("24:00:00", Time.Parse("24:00:00").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T12:12:12", Time.Parse("T12:12:12").ToString(true, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T24:00:00", Time.Parse("T24:00:00").ToString(true, DecimalSeparator.Comma, true, false));

            // Reduced
            Assert.AreEqual("1212", Time.Parse("1212").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("2400", Time.Parse("2400").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T1212", Time.Parse("T1212").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T2400", Time.Parse("T2400").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("12:12", Time.Parse("12:12").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("24:00", Time.Parse("24:00").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T12:12", Time.Parse("T12:12").ToString(true, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T24:00", Time.Parse("T24:00").ToString(true, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("12", Time.Parse("12").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("24", Time.Parse("24").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T12", Time.Parse("T12").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T24", Time.Parse("T24").ToString(true, DecimalSeparator.Comma, false, false));

            // Fractional
            Assert.AreEqual("121212,12", Time.Parse("121212,12").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("121212.12", Time.Parse("121212.12").ToString(false, DecimalSeparator.Dot, false, false));
            Assert.AreEqual("T121212,12", Time.Parse("T121212,12").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T121212.12", Time.Parse("T121212.12").ToString(true, DecimalSeparator.Dot, false, false));
            Assert.AreEqual("12:12:12,12", Time.Parse("12:12:12,12").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("12:12:12.12", Time.Parse("12:12:12.12").ToString(false, DecimalSeparator.Dot, true, false));
            Assert.AreEqual("T12:12:12,12", Time.Parse("T12:12:12,12").ToString(true, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T12:12:12.12", Time.Parse("T12:12:12.12").ToString(true, DecimalSeparator.Dot, true, false));
            Assert.AreEqual("1212,12", Time.Parse("1212,12").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("1212.12", Time.Parse("1212.12").ToString(false, DecimalSeparator.Dot, false, false));
            Assert.AreEqual("T1212,12", Time.Parse("T1212,12").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T1212.12", Time.Parse("T1212.12").ToString(true, DecimalSeparator.Dot, false, false));
            Assert.AreEqual("12:12,12", Time.Parse("12:12,12").ToString(false, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("12:12.12", Time.Parse("12:12.12").ToString(false, DecimalSeparator.Dot, true, false));
            Assert.AreEqual("T12:12,12", Time.Parse("T12:12,12").ToString(true, DecimalSeparator.Comma, true, false));
            Assert.AreEqual("T12:12.12", Time.Parse("T12:12.12").ToString(true, DecimalSeparator.Dot, true, false));
            Assert.AreEqual("12,12", Time.Parse("12,12").ToString(false, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("12.12", Time.Parse("12.12").ToString(false, DecimalSeparator.Dot, false, false));
            Assert.AreEqual("T12,12", Time.Parse("T12,12").ToString(true, DecimalSeparator.Comma, false, false));
            Assert.AreEqual("T12.12", Time.Parse("T12.12").ToString(true, DecimalSeparator.Dot, false, false));

            // UTC and UTC Offset
            Assert.AreEqual("121212Z", Time.Parse("121212Z").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("121212+05", Time.Parse("121212+05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("121212+0530", Time.Parse("121212+0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("121212-05", Time.Parse("121212-05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("121212-0530", Time.Parse("121212-0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12:12:12Z", Time.Parse("12:12:12Z").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12:12+05", Time.Parse("12:12:12+05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12:12+05:30", Time.Parse("12:12:12+05:30").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12:12-05", Time.Parse("12:12:12-05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12:12-05:30", Time.Parse("12:12:12-05:30").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("1212Z", Time.Parse("1212Z").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("1212+05", Time.Parse("1212+05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("1212+0530", Time.Parse("1212+0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("1212-05", Time.Parse("1212-05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("1212-0530", Time.Parse("1212-0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12:12Z", Time.Parse("12:12Z").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12+05", Time.Parse("12:12+05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12+05:30", Time.Parse("12:12+05:30").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12-05", Time.Parse("12:12-05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12:12-05:30", Time.Parse("12:12-05:30").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12Z", Time.Parse("12Z").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12+05", Time.Parse("12+05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12+0530", Time.Parse("12+0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12-05", Time.Parse("12-05").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12-0530", Time.Parse("12-0530").ToString(false, DecimalSeparator.Comma, false, true));
            Assert.AreEqual("12Z", Time.Parse("12Z").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12+05", Time.Parse("12+05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12+05:30", Time.Parse("12+05:30").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12-05", Time.Parse("12-05").ToString(false, DecimalSeparator.Comma, true, true));
            Assert.AreEqual("12-05:30", Time.Parse("12-05:30").ToString(false, DecimalSeparator.Comma, true, true));
        }
    }
}
