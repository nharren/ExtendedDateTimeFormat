using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("121212", Time.Parse("121212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("240000", Time.Parse("240000").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T121212", Time.Parse("T121212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("T240000", Time.Parse("T240000").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("12:12:12", Time.Parse("12:12:12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("24:00:00", Time.Parse("24:00:00").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T12:12:12", Time.Parse("T12:12:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("T24:00:00", Time.Parse("T24:00:00").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));

            // Reduced
            Assert.AreEqual("1212", Time.Parse("1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("2400", Time.Parse("2400").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T1212", Time.Parse("T1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("T2400", Time.Parse("T2400").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("12:12", Time.Parse("12:12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("24:00", Time.Parse("24:00").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T12:12", Time.Parse("T12:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("T24:00", Time.Parse("T24:00").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("12", Time.Parse("12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("24", Time.Parse("24").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T12", Time.Parse("T12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("T24", Time.Parse("T24").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));

            // Fractional
            Assert.AreEqual("121212,12", Time.Parse("121212,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("121212.12", Time.Parse("121212.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T121212,12", Time.Parse("T121212,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("T121212.12", Time.Parse("T121212.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("12:12:12,12", Time.Parse("12:12:12,12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("12:12:12.12", Time.Parse("12:12:12.12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, DecimalSeparator = '.', FractionLength = 2, UseUtcOffset = false }));
            Assert.AreEqual("T12:12:12,12", Time.Parse("T12:12:12,12").ToString(new DateTimeFormatInfo { UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("T12:12:12.12", Time.Parse("T12:12:12.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseUtcOffset = false }));
            Assert.AreEqual("1212,12", Time.Parse("1212,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("1212.12", Time.Parse("1212.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T1212,12", Time.Parse("T1212,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("T1212.12", Time.Parse("T1212.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("12:12,12", Time.Parse("12:12,12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("12:12.12", Time.Parse("12:12.12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, DecimalSeparator = '.', FractionLength = 2, UseUtcOffset = false }));
            Assert.AreEqual("T12:12,12", Time.Parse("T12:12,12").ToString(new DateTimeFormatInfo { UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("T12:12.12", Time.Parse("T12:12.12").ToString(new DateTimeFormatInfo { UseUtcOffset = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("12,12", Time.Parse("12,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("12.12", Time.Parse("12.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("T12,12", Time.Parse("T12,12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false, FractionLength = 2 }));
            Assert.AreEqual("T12.12", Time.Parse("T12.12").ToString(new DateTimeFormatInfo { DecimalSeparator = '.', FractionLength = 2, UseComponentSeparators = false, UseUtcOffset = false }));

            // UTC and UTC Offset
            Assert.AreEqual("121212Z", Time.Parse("121212Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("121212+05", Time.Parse("121212+05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("121212+0530", Time.Parse("121212+0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("121212-05", Time.Parse("121212-05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("121212-0530", Time.Parse("121212-0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12:12:12Z", Time.Parse("12:12:12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12:12+05", Time.Parse("12:12:12+05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12:12+05:30", Time.Parse("12:12:12+05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12:12-05", Time.Parse("12:12:12-05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12:12-05:30", Time.Parse("12:12:12-05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1212Z", Time.Parse("1212Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1212+05", Time.Parse("1212+05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1212+0530", Time.Parse("1212+0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1212-05", Time.Parse("1212-05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1212-0530", Time.Parse("1212-0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12:12Z", Time.Parse("12:12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12+05", Time.Parse("12:12+05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12+05:30", Time.Parse("12:12+05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12-05", Time.Parse("12:12-05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12:12-05:30", Time.Parse("12:12-05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12Z", Time.Parse("12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12+05", Time.Parse("12+05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12+0530", Time.Parse("12+0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12-05", Time.Parse("12-05").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12-0530", Time.Parse("12-0530").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("12Z", Time.Parse("12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12+05", Time.Parse("12+05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12+05:30", Time.Parse("12+05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12-05", Time.Parse("12-05").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("12-05:30", Time.Parse("12-05:30").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
        }
    }
}