using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class WeekDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("1950W101121212", WeekDateTime.Parse("1950W101121212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-112:12:12", WeekDateTime.Parse("1950-W10-112:12:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W101T121212", WeekDateTime.Parse("1950W101T121212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-1T12:12:12", WeekDateTime.Parse("1950-W10-1T12:12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W101121212+1212", WeekDateTime.Parse("1950W101121212+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112:12:12+12:12", WeekDateTime.Parse("1950-W10-112:12:12+12:12").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950W101T121212+1212", WeekDateTime.Parse("1950W101T121212+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12:12:12+12:12", WeekDateTime.Parse("1950-W10-1T12:12:12+12:12").ToString(true, true, DecimalSeparator.Comma, true));

            // Reduced
            Assert.AreEqual("1950W1011212", WeekDateTime.Parse("1950W1011212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-112:12", WeekDateTime.Parse("1950-W10-112:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W101T1212", WeekDateTime.Parse("1950W101T1212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-1T12:12", WeekDateTime.Parse("1950-W10-1T12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W10112", WeekDateTime.Parse("1950W10112").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-112", WeekDateTime.Parse("1950-W10-112").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W101T12", WeekDateTime.Parse("1950W101T12").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-W10-1T12", WeekDateTime.Parse("1950-W10-1T12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950W1011212Z", WeekDateTime.Parse("1950W1011212Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112:12Z", WeekDateTime.Parse("1950-W10-112:12Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950W101T1212Z", WeekDateTime.Parse("1950W101T1212Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12:12Z", WeekDateTime.Parse("1950-W10-1T12:12Z").ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950W10112Z", WeekDateTime.Parse("1950W10112Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112Z", WeekDateTime.Parse("1950-W10-112Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950W101T12Z", WeekDateTime.Parse("1950W101T12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12Z", WeekDateTime.Parse("1950-W10-1T12Z").ToString(true, true, DecimalSeparator.Comma, true));

            // Fractional
            Assert.AreEqual("1950W101121212,12+1212", WeekDateTime.Parse("1950W101121212,12+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112:12:12.12+12:12", WeekDateTime.Parse("1950-W10-112:12:12.12+12:12").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950W101T121212,12+1212", WeekDateTime.Parse("1950W101T121212,12+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12:12:12.12+12:12", WeekDateTime.Parse("1950-W10-1T12:12:12.12+12:12").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950W1011212,12Z", WeekDateTime.Parse("1950W1011212,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112:12.12Z", WeekDateTime.Parse("1950-W10-112:12.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950W101T1212,12Z", WeekDateTime.Parse("1950W101T1212,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12:12.12Z", WeekDateTime.Parse("1950-W10-1T12:12.12Z").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950W10112,12Z", WeekDateTime.Parse("1950W10112,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-112.12Z", WeekDateTime.Parse("1950-W10-112.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950W101T12,12Z", WeekDateTime.Parse("1950W101T12,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-W10-1T12.12Z", WeekDateTime.Parse("1950-W10-1T12.12Z").ToString(true, true, DecimalSeparator.Dot, true));

            // Expanded
            Assert.AreEqual("+11950W101121212+1212", WeekDateTime.Parse("+11950W101121212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-W10-112:12:12+12:12", WeekDateTime.Parse("+11950-W10-112:12:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950W101T121212+1212", WeekDateTime.Parse("+11950W101T121212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-W10-1T12:12:12+12:12", WeekDateTime.Parse("+11950-W10-1T12:12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950W1011212+1212", WeekDateTime.Parse("+11950W1011212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-W10-112:12+12:12", WeekDateTime.Parse("+11950-W10-112:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950W101T1212+1212", WeekDateTime.Parse("-11950W101T1212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-W10-1T12:12+12:12", WeekDateTime.Parse("-11950-W10-1T12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950W10112+1212", WeekDateTime.Parse("-11950W10112+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-W10-112+12:12", WeekDateTime.Parse("-11950-W10-112+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950W101T12+1212", WeekDateTime.Parse("-11950W101T12+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-W10-1T12+12:12", WeekDateTime.Parse("-11950-W10-1T12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
        }
    }
}
