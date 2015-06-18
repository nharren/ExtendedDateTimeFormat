using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class CalendarDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("19501010121212", CalendarDateTime.Parse("19501010121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10-1012:12:12", CalendarDateTime.Parse("1950-10-1012:12:12").ToString(new ISO8601Options { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("19501010T121212", CalendarDateTime.Parse("19501010T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10-10T12:12:12", CalendarDateTime.Parse("1950-10-10T12:12:12").ToString(new ISO8601Options { UseUtcOffset = false }));
            Assert.AreEqual("19501010121212+1212", CalendarDateTime.Parse("19501010121212+1212").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012:12:12+12:12", CalendarDateTime.Parse("1950-10-1012:12:12+12:12").ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("19501010T121212+1212", CalendarDateTime.Parse("19501010T121212+1212").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12:12:12+12:12", CalendarDateTime.Parse("1950-10-10T12:12:12+12:12").ToString());

            // Reduced
            Assert.AreEqual("195010101212", CalendarDateTime.Parse("195010101212").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10-1012:12", CalendarDateTime.Parse("1950-10-1012:12").ToString(new ISO8601Options { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("19501010T1212", CalendarDateTime.Parse("19501010T1212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10-10T12:12", CalendarDateTime.Parse("1950-10-10T12:12").ToString(new ISO8601Options { UseUtcOffset = false }));
            Assert.AreEqual("1950101012", CalendarDateTime.Parse("1950101012").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012", CalendarDateTime.Parse("1950-10-1012").ToString(new ISO8601Options { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("19501010T12", CalendarDateTime.Parse("19501010T12").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10-10T12", CalendarDateTime.Parse("1950-10-10T12").ToString(new ISO8601Options { UseUtcOffset = false }));
            Assert.AreEqual("195010101212Z", CalendarDateTime.Parse("195010101212Z").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012:12Z", CalendarDateTime.Parse("1950-10-1012:12Z").ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("19501010T1212Z", CalendarDateTime.Parse("19501010T1212Z").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12:12Z", CalendarDateTime.Parse("1950-10-10T12:12Z").ToString());
            Assert.AreEqual("1950101012Z", CalendarDateTime.Parse("1950101012Z").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012Z", CalendarDateTime.Parse("1950-10-1012Z").ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("19501010T12Z", CalendarDateTime.Parse("19501010T12Z").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12Z", CalendarDateTime.Parse("1950-10-10T12Z").ToString());

            // Fractional
            Assert.AreEqual("19501010121212,12+1212", CalendarDateTime.Parse("19501010121212,12+1212").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012:12:12.12+12:12", CalendarDateTime.Parse("1950-10-1012:12:12.12+12:12").ToString(new ISO8601Options { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("19501010T121212,12+1212", CalendarDateTime.Parse("19501010T121212,12+1212").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12:12:12.12+12:12", CalendarDateTime.Parse("1950-10-10T12:12:12.12+12:12").ToString(new ISO8601Options { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("195010101212,12Z", CalendarDateTime.Parse("195010101212,12Z").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012:12.12Z", CalendarDateTime.Parse("1950-10-1012:12.12Z").ToString(new ISO8601Options { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("19501010T1212,12Z", CalendarDateTime.Parse("19501010T1212,12Z").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12:12.12Z", CalendarDateTime.Parse("1950-10-10T12:12.12Z").ToString(new ISO8601Options { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("1950101012,12Z", CalendarDateTime.Parse("1950101012,12Z").ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10-1012.12Z", CalendarDateTime.Parse("1950-10-1012.12Z").ToString(new ISO8601Options { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("19501010T12,12Z", CalendarDateTime.Parse("19501010T12,12Z").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-10-10T12.12Z", CalendarDateTime.Parse("1950-10-10T12.12Z").ToString(new ISO8601Options { FractionLength = 2, DecimalSeparator = '.' }));

            // Expanded
            Assert.AreEqual("+119501010121212+1212", CalendarDateTime.Parse("+119501010121212+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("+11950-10-1012:12:12+12:12", CalendarDateTime.Parse("+11950-10-1012:12:12+12:12", 5).ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("+119501010T121212+1212", CalendarDateTime.Parse("+119501010T121212+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("+11950-10-10T12:12:12+12:12", CalendarDateTime.Parse("+11950-10-10T12:12:12+12:12", 5).ToString());
            Assert.AreEqual("+1195010101212+1212", CalendarDateTime.Parse("+1195010101212+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("+11950-10-1012:12+12:12", CalendarDateTime.Parse("+11950-10-1012:12+12:12", 5).ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("-119501010T1212+1212", CalendarDateTime.Parse("-119501010T1212+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("-11950-10-10T12:12+12:12", CalendarDateTime.Parse("-11950-10-10T12:12+12:12", 5).ToString());
            Assert.AreEqual("-11950101012+1212", CalendarDateTime.Parse("-11950101012+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("-11950-10-1012+12:12", CalendarDateTime.Parse("-11950-10-1012+12:12", 5).ToString(new ISO8601Options { UseTimeDesignator = false }));
            Assert.AreEqual("-119501010T12+1212", CalendarDateTime.Parse("-119501010T12+1212", 5).ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("-11950-10-10T12+12:12", CalendarDateTime.Parse("-11950-10-10T12+12:12", 5).ToString());
        }
    }
}