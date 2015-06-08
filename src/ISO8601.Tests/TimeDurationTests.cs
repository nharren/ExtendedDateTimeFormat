using System;
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
            Assert.AreEqual("PT202020", TimeDuration.Parse("PT202020").ToString(false, 0, DecimalSeparator.Comma));
            Assert.AreEqual("PT20:20:20", TimeDuration.Parse("PT20:20:20").ToString(true, 0, DecimalSeparator.Comma));

            // Reduced
            Assert.AreEqual("PT2020", TimeDuration.Parse("PT2020").ToString(false, 0, DecimalSeparator.Comma));
            Assert.AreEqual("PT20:20", TimeDuration.Parse("PT20:20").ToString(true, 0, DecimalSeparator.Comma));
            Assert.AreEqual("PT20", TimeDuration.Parse("PT20").ToString(false, 0, DecimalSeparator.Comma));

            // Fractional
            Assert.AreEqual("PT202020,20", TimeDuration.Parse("PT202020,20").ToString(false, 2, DecimalSeparator.Comma));
            Assert.AreEqual("PT20:20:20.20", TimeDuration.Parse("PT20:20:20.20").ToString(true, 2, DecimalSeparator.Dot));
            Assert.AreEqual("PT2020,20", TimeDuration.Parse("PT2020,20").ToString(false, 2, DecimalSeparator.Comma));
            Assert.AreEqual("PT20:20.20", TimeDuration.Parse("PT20:20.20").ToString(true, 2, DecimalSeparator.Dot));
            Assert.AreEqual("PT20,20", TimeDuration.Parse("PT20,20").ToString(false, 2, DecimalSeparator.Comma));
        }
    }
}
