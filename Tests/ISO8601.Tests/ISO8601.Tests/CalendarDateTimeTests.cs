using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateTimeTests
    {
        [TestMethod]
        public void RoundTrip()
        {
            // Complete
            Assert.AreEqual("19501010121212", CalendarDateTime.Parse("19501010121212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-1012:12:12", CalendarDateTime.Parse("1950-10-1012:12:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("19501010T121212", CalendarDateTime.Parse("19501010T121212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-10T12:12:12", CalendarDateTime.Parse("1950-10-10T12:12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("19501010121212+1212", CalendarDateTime.Parse("19501010121212+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012:12:12+12:12", CalendarDateTime.Parse("1950-10-1012:12:12+12:12").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("19501010T121212+1212", CalendarDateTime.Parse("19501010T121212+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12:12:12+12:12", CalendarDateTime.Parse("1950-10-10T12:12:12+12:12").ToString(true, true, DecimalSeparator.Comma, true));

            // Reduced
            Assert.AreEqual("195010101212", CalendarDateTime.Parse("195010101212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-1012:12", CalendarDateTime.Parse("1950-10-1012:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("19501010T1212", CalendarDateTime.Parse("19501010T1212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-10T12:12", CalendarDateTime.Parse("1950-10-10T12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950101012", CalendarDateTime.Parse("1950101012").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-1012", CalendarDateTime.Parse("1950-10-1012").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("19501010T12", CalendarDateTime.Parse("19501010T12").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10-10T12", CalendarDateTime.Parse("1950-10-10T12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("195010101212Z", CalendarDateTime.Parse("195010101212Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012:12Z", CalendarDateTime.Parse("1950-10-1012:12Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("19501010T1212Z", CalendarDateTime.Parse("19501010T1212Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12:12Z", CalendarDateTime.Parse("1950-10-10T12:12Z").ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950101012Z", CalendarDateTime.Parse("1950101012Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012Z", CalendarDateTime.Parse("1950-10-1012Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("19501010T12Z", CalendarDateTime.Parse("19501010T12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12Z", CalendarDateTime.Parse("1950-10-10T12Z").ToString(true, true, DecimalSeparator.Comma, true));

            // Fractional
            Assert.AreEqual("19501010121212,12+1212", CalendarDateTime.Parse("19501010121212,12+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012:12:12.12+12:12", CalendarDateTime.Parse("1950-10-1012:12:12.12+12:12").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("19501010T121212,12+1212", CalendarDateTime.Parse("19501010T121212,12+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12:12:12.12+12:12", CalendarDateTime.Parse("1950-10-10T12:12:12.12+12:12").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("195010101212,12Z", CalendarDateTime.Parse("195010101212,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012:12.12Z", CalendarDateTime.Parse("1950-10-1012:12.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("19501010T1212,12Z", CalendarDateTime.Parse("19501010T1212,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12:12.12Z", CalendarDateTime.Parse("1950-10-10T12:12.12Z").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950101012,12Z", CalendarDateTime.Parse("1950101012,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-1012.12Z", CalendarDateTime.Parse("1950-10-1012.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("19501010T12,12Z", CalendarDateTime.Parse("19501010T12,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10-10T12.12Z", CalendarDateTime.Parse("1950-10-10T12.12Z").ToString(true, true, DecimalSeparator.Dot, true));

            // Expanded
            Assert.AreEqual("+119501010121212+1212", CalendarDateTime.Parse("+119501010121212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-10-1012:12:12+12:12", CalendarDateTime.Parse("+11950-10-1012:12:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+119501010T121212+1212", CalendarDateTime.Parse("+119501010T121212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-10-10T12:12:12+12:12", CalendarDateTime.Parse("+11950-10-10T12:12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+1195010101212+1212", CalendarDateTime.Parse("+1195010101212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-10-1012:12+12:12", CalendarDateTime.Parse("+11950-10-1012:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-119501010T1212+1212", CalendarDateTime.Parse("-119501010T1212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-10-10T12:12+12:12", CalendarDateTime.Parse("-11950-10-10T12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950101012+1212", CalendarDateTime.Parse("-11950101012+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-10-1012+12:12", CalendarDateTime.Parse("-11950-10-1012+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-119501010T12+1212", CalendarDateTime.Parse("-119501010T12+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-10-10T12+12:12", CalendarDateTime.Parse("-11950-10-10T12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
        }
    }
}
