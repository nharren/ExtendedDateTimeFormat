using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P1234111", OrdinalDateDuration.Parse("P1234111").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("P1234-111", OrdinalDateDuration.Parse("P1234-111").ToString());

            // Extended
            Assert.AreEqual("P+11234111", OrdinalDateDuration.Parse("P+11234111", 5).ToString(new DateTimeFormatInfo { UseComponentSeparators = false, UseTimeDesignator = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("P+11234-111", OrdinalDateDuration.Parse("P+11234-111", 5).ToString(new DateTimeFormatInfo { IsExpanded = true, YearLength = 5 }));
        }
    }
}