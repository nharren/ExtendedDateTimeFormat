using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class TimeDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("PT202020", TimeDuration.Parse("PT202020").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("PT20:20:20", TimeDuration.Parse("PT20:20:20").ToString());

            // Reduced
            Assert.AreEqual("PT2020", TimeDuration.Parse("PT2020").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));
            Assert.AreEqual("PT20:20", TimeDuration.Parse("PT20:20").ToString());
            Assert.AreEqual("PT20", TimeDuration.Parse("PT20").ToString(new DateTimeFormatInfo { UseComponentSeparators = false }));

            // Fractional
            Assert.AreEqual("PT202020,20", TimeDuration.Parse("PT202020,20").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("PT20:20:20.20", TimeDuration.Parse("PT20:20:20.20").ToString(new DateTimeFormatInfo { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("PT2020,20", TimeDuration.Parse("PT2020,20").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
            Assert.AreEqual("PT20:20.20", TimeDuration.Parse("PT20:20.20").ToString(new DateTimeFormatInfo { FractionLength = 2, DecimalSeparator = '.' }));
            Assert.AreEqual("PT20,20", TimeDuration.Parse("PT20,20").ToString(new DateTimeFormatInfo { UseComponentSeparators = false, FractionLength = 2 }));
        }
    }
}