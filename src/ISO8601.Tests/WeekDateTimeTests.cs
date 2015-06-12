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
            Assert.AreEqual("1950W101121212", WeekDateTime.Parse("1950W101121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-W10-112:12:12", WeekDateTime.Parse("1950-W10-112:12:12").ToString(new ISO8601FormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T121212", WeekDateTime.Parse("1950W101T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-W10-1T12:12:12", WeekDateTime.Parse("1950-W10-1T12:12:12").ToString(new ISO8601FormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("1950W101121212+1212", WeekDateTime.Parse("1950W101121212+1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112:12:12+12:12", WeekDateTime.Parse("1950-W10-112:12:12+12:12").ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T121212+1212", WeekDateTime.Parse("1950W101T121212+1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12:12:12+12:12", WeekDateTime.Parse("1950-W10-1T12:12:12+12:12").ToString());

            // Reduced
            Assert.AreEqual("1950W1011212", WeekDateTime.Parse("1950W1011212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-W10-112:12", WeekDateTime.Parse("1950-W10-112:12").ToString(new ISO8601FormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T1212", WeekDateTime.Parse("1950W101T1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-W10-1T12:12", WeekDateTime.Parse("1950-W10-1T12:12").ToString(new ISO8601FormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("1950W10112", WeekDateTime.Parse("1950W10112").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112", WeekDateTime.Parse("1950-W10-112").ToString(new ISO8601FormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T12", WeekDateTime.Parse("1950W101T12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-W10-1T12", WeekDateTime.Parse("1950-W10-1T12").ToString(new ISO8601FormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("1950W1011212Z", WeekDateTime.Parse("1950W1011212Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112:12Z", WeekDateTime.Parse("1950-W10-112:12Z").ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T1212Z", WeekDateTime.Parse("1950W101T1212Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12:12Z", WeekDateTime.Parse("1950-W10-1T12:12Z").ToString());
            Assert.AreEqual("1950W10112Z", WeekDateTime.Parse("1950W10112Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112Z", WeekDateTime.Parse("1950-W10-112Z").ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950W101T12Z", WeekDateTime.Parse("1950W101T12Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12Z", WeekDateTime.Parse("1950-W10-1T12Z").ToString());

            // Fractional
            Assert.AreEqual("1950W101121212,12+1212", WeekDateTime.Parse("1950W101121212,12+1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112:12:12.12+12:12", WeekDateTime.Parse("1950-W10-112:12:12.12+12:12").ToString(new ISO8601FormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("1950W101T121212,12+1212", WeekDateTime.Parse("1950W101T121212,12+1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12:12:12.12+12:12", WeekDateTime.Parse("1950-W10-1T12:12:12.12+12:12").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("1950W1011212,12Z", WeekDateTime.Parse("1950W1011212,12Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112:12.12Z", WeekDateTime.Parse("1950-W10-112:12.12Z").ToString(new ISO8601FormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("1950W101T1212,12Z", WeekDateTime.Parse("1950W101T1212,12Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12:12.12Z", WeekDateTime.Parse("1950-W10-1T12:12.12Z").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("1950W10112,12Z", WeekDateTime.Parse("1950W10112,12Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-W10-112.12Z", WeekDateTime.Parse("1950-W10-112.12Z").ToString(new ISO8601FormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("1950W101T12,12Z", WeekDateTime.Parse("1950W101T12,12Z").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-W10-1T12.12Z", WeekDateTime.Parse("1950-W10-1T12.12Z").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));

            // Expanded
            Assert.AreEqual("+11950W101121212+1212", WeekDateTime.Parse("+11950W101121212+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("+11950-W10-112:12:12+12:12", WeekDateTime.Parse("+11950-W10-112:12:12+12:12", 5).ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("+11950W101T121212+1212", WeekDateTime.Parse("+11950W101T121212+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("+11950-W10-1T12:12:12+12:12", WeekDateTime.Parse("+11950-W10-1T12:12:12+12:12", 5).ToString());
            Assert.AreEqual("+11950W1011212+1212", WeekDateTime.Parse("+11950W1011212+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("+11950-W10-112:12+12:12", WeekDateTime.Parse("+11950-W10-112:12+12:12", 5).ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("-11950W101T1212+1212", WeekDateTime.Parse("-11950W101T1212+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("-11950-W10-1T12:12+12:12", WeekDateTime.Parse("-11950-W10-1T12:12+12:12", 5).ToString());
            Assert.AreEqual("-11950W10112+1212", WeekDateTime.Parse("-11950W10112+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("-11950-W10-112+12:12", WeekDateTime.Parse("-11950-W10-112+12:12", 5).ToString(new ISO8601FormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("-11950W101T12+1212", WeekDateTime.Parse("-11950W101T12+1212", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("-11950-W10-1T12+12:12", WeekDateTime.Parse("-11950-W10-1T12+12:12", 5).ToString());
        }
    }
}