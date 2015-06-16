using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("1950100121212", OrdinalDateTime.Parse("1950100121212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-10012:12:12", OrdinalDateTime.Parse("1950-10012:12:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950100T121212", OrdinalDateTime.Parse("1950100T121212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-100T12:12:12", OrdinalDateTime.Parse("1950-100T12:12:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("1950100121212+1212", OrdinalDateTime.Parse("1950100121212+1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012:12:12+12:12", OrdinalDateTime.Parse("1950-10012:12:12+12:12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950100T121212+1212", OrdinalDateTime.Parse("1950100T121212+1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12:12:12+12:12", OrdinalDateTime.Parse("1950-100T12:12:12+12:12").ToString());

            // Reduced
            Assert.AreEqual("19501001212", OrdinalDateTime.Parse("19501001212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012:12", OrdinalDateTime.Parse("1950-10012:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950100T1212", OrdinalDateTime.Parse("1950100T1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-100T12:12", OrdinalDateTime.Parse("1950-100T12:12").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("195010012", OrdinalDateTime.Parse("195010012").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012", OrdinalDateTime.Parse("1950-10012").ToString(new DateTimeFormatInfo { UseUtcOffset = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950100T12", OrdinalDateTime.Parse("1950100T12").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950-100T12", OrdinalDateTime.Parse("1950-100T12").ToString(new DateTimeFormatInfo { UseUtcOffset = false }));
            Assert.AreEqual("19501001212Z", OrdinalDateTime.Parse("19501001212Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012:12Z", OrdinalDateTime.Parse("1950-10012:12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950100T1212Z", OrdinalDateTime.Parse("1950100T1212Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12:12Z", OrdinalDateTime.Parse("1950-100T12:12Z").ToString());
            Assert.AreEqual("195010012Z", OrdinalDateTime.Parse("195010012Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012Z", OrdinalDateTime.Parse("1950-10012Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false }));
            Assert.AreEqual("1950100T12Z", OrdinalDateTime.Parse("1950100T12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12Z", OrdinalDateTime.Parse("1950-100T12Z").ToString());

            // Fractional
            Assert.AreEqual("1950100121212,12+1212", OrdinalDateTime.Parse("1950100121212,12+1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012:12:12.12+12:12", OrdinalDateTime.Parse("1950-10012:12:12.12+12:12").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("1950100T121212,12+1212", OrdinalDateTime.Parse("1950100T121212,12+1212").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12:12:12.12+12:12", OrdinalDateTime.Parse("1950-100T12:12:12.12+12:12").ToString(new DateTimeFormatInfo { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("19501001212,12Z", OrdinalDateTime.Parse("19501001212,12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012:12.12Z", OrdinalDateTime.Parse("1950-10012:12.12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("1950100T1212,12Z", OrdinalDateTime.Parse("1950100T1212,12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12:12.12Z", OrdinalDateTime.Parse("1950-100T12:12.12Z").ToString(new DateTimeFormatInfo { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("195010012,12Z", OrdinalDateTime.Parse("195010012,12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false }));
            Assert.AreEqual("1950-10012.12Z", OrdinalDateTime.Parse("1950-10012.12Z").ToString(new DateTimeFormatInfo { UseTimeDesignator = false, FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("1950100T12,12Z", OrdinalDateTime.Parse("1950100T12,12Z").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100T12.12Z", OrdinalDateTime.Parse("1950-100T12.12Z").ToString(new DateTimeFormatInfo { FractionLength = 2, DecimalSeparator = '.' }));

            // Expanded
            Assert.AreEqual("+11950100121212+1212", OrdinalDateTime.Parse("+11950100121212+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+11950-10012:12:12+12:12", OrdinalDateTime.Parse("+11950-10012:12:12+12:12", 5).ToString(new DateTimeFormatInfo { UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+11950100T121212+1212", OrdinalDateTime.Parse("+11950100T121212+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+11950-100T12:12:12+12:12", OrdinalDateTime.Parse("+11950-100T12:12:12+12:12", 5).ToString(new DateTimeFormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+119501001212+1212", OrdinalDateTime.Parse("+119501001212+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+11950-10012:12+12:12", OrdinalDateTime.Parse("+11950-10012:12+12:12", 5).ToString(new DateTimeFormatInfo { UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-11950100T1212+1212", OrdinalDateTime.Parse("-11950100T1212+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-11950-100T12:12+12:12", OrdinalDateTime.Parse("-11950-100T12:12+12:12", 5).ToString(new DateTimeFormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-1195010012+1212", OrdinalDateTime.Parse("-1195010012+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-11950-10012+12:12", OrdinalDateTime.Parse("-11950-10012+12:12", 5).ToString(new DateTimeFormatInfo { UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-11950100T12+1212", OrdinalDateTime.Parse("-11950100T12+1212", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("-11950-100T12+12:12", OrdinalDateTime.Parse("-11950-100T12+12:12", 5).ToString(new DateTimeFormatInfo { IsExpanded = true, YearLength = 5 }));
        }
    }
}