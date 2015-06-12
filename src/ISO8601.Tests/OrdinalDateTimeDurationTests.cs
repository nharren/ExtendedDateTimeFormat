using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateTimeDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P1234111T121212", OrdinalDateTimeDuration.Parse("P1234111T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("P1234-111T12:12:12", OrdinalDateTimeDuration.Parse("P1234-111T12:12:12").ToString());

            // Reduced
            Assert.AreEqual("P1234111T1212", OrdinalDateTimeDuration.Parse("P1234111T1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("P1234-111T12:12", OrdinalDateTimeDuration.Parse("P1234-111T12:12").ToString());
            Assert.AreEqual("P1234111T12", OrdinalDateTimeDuration.Parse("P1234111T12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));

            // Fractional
            Assert.AreEqual("P1234111T121212,12", OrdinalDateTimeDuration.Parse("P1234111T121212,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("P1234-111T12:12:12.12", OrdinalDateTimeDuration.Parse("P1234-111T12:12:12.12").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P1234111T1212,12", OrdinalDateTimeDuration.Parse("P1234111T1212,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("P1234-111T12:12.12", OrdinalDateTimeDuration.Parse("P1234-111T12:12.12").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P1234111T12,12", OrdinalDateTimeDuration.Parse("P1234111T12,12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, FractionLength = 2 }));

            // Expanded
            Assert.AreEqual("P+1234111T121212", OrdinalDateTimeDuration.Parse("P+1234111T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P+11234-111T12:12:12", OrdinalDateTimeDuration.Parse("P+11234-111T12:12:12", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("P+1234111T1212", OrdinalDateTimeDuration.Parse("P+1234111T1212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P+11234-111T12:12", OrdinalDateTimeDuration.Parse("P+11234-111T12:12", 5).ToString(new ISO8601FormatInfo { IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("P+1234111T12", OrdinalDateTimeDuration.Parse("P+1234111T12").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("P-11234111T121212,12", OrdinalDateTimeDuration.Parse("P-11234111T121212,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
            Assert.AreEqual("P-1234-111T12:12:12.12", OrdinalDateTimeDuration.Parse("P-1234-111T12:12:12.12").ToString(new ISO8601FormatInfo { IsExpanded = true, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P-11234111T1212,12", OrdinalDateTimeDuration.Parse("P-11234111T1212,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
            Assert.AreEqual("P-1234-111T12:12.12", OrdinalDateTimeDuration.Parse("P-1234-111T12:12.12").ToString(new ISO8601FormatInfo { IsExpanded = true, FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P-11234111T12,12", OrdinalDateTimeDuration.Parse("P-11234111T12,12", 5).ToString(new ISO8601FormatInfo { UseComponentSeparators = false, IsExpanded = true, YearLength = 5, FractionLength = 2 }));
        }
    }
}