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
            Assert.AreEqual("P12341212T121212", CalendarDateTimeDuration.Parse("P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("P1234-12-12T12:12:12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12:12").ToString());

            // Reduced
            Assert.AreEqual("P12341212T1212", CalendarDateTimeDuration.Parse("P12341212T1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("P1234-12-12T12:12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12").ToString());
            Assert.AreEqual("P12341212T12", CalendarDateTimeDuration.Parse("P12341212T12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));

            // Fractional
            Assert.AreEqual("P12341212T121212,12", CalendarDateTimeDuration.Parse("P12341212T121212,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("P1234-12-12T12:12:12.12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12:12.12").ToString(new ISO8601FormatInfo { DecimalSeparator = DecimalSeparator.Dot, FractionLength = 2 }));
            Assert.AreEqual("P12341212T1212,12", CalendarDateTimeDuration.Parse("P12341212T1212,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("P1234-12-12T12:12.12", CalendarDateTimeDuration.Parse("P1234-12-12T12:12.12").ToString(new ISO8601FormatInfo { DecimalSeparator = DecimalSeparator.Dot, FractionLength = 2 }));
            Assert.AreEqual("P12341212T12,12", CalendarDateTimeDuration.Parse("P12341212T12,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));

            // Expanded
            Assert.AreEqual("P+12341212T121212", CalendarDateTimeDuration.Parse("P+12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P+11234-12-12T12:12:12", CalendarDateTimeDuration.Parse("P+11234-12-12T12:12:12", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("P+12341212T1212", CalendarDateTimeDuration.Parse("P+12341212T1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P+11234-12-12T12:12", CalendarDateTimeDuration.Parse("P+11234-12-12T12:12", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("P+12341212T12", CalendarDateTimeDuration.Parse("P+12341212T12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P-112341212T121212,12", CalendarDateTimeDuration.Parse("P-112341212T121212,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
            Assert.AreEqual("P-1234-12-12T12:12:12.12", CalendarDateTimeDuration.Parse("P-1234-12-12T12:12:12.12").ToString(new ISO8601FormatInfo { IsExpanded = true, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P-112341212T1212,12", CalendarDateTimeDuration.Parse("P-112341212T1212,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
            Assert.AreEqual("P-1234-12-12T12:12.12", CalendarDateTimeDuration.Parse("P-1234-12-12T12:12.12").ToString(new ISO8601FormatInfo { IsExpanded = true, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P-112341212T12,12", CalendarDateTimeDuration.Parse("P-112341212T12,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
        }
    }
}