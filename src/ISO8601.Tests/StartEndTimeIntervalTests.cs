using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class StartEndTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("19501010121212/19501010121212", StartEndTimeInterval.Parse("19501010121212/19501010121212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950100121212/1950100121212", StartEndTimeInterval.Parse("1950100121212/1950100121212").ToString(false, false, DecimalSeparator.Comma, false));
        }
    }
}
