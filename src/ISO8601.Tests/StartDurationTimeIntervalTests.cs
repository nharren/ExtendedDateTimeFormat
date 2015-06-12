using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class StartDurationTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("19501010T121212/P12341212T121212", StartDurationTimeInterval.Parse("19501010T121212/P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950100T121212/P12341212T121212", StartDurationTimeInterval.Parse("1950100T121212/P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
        }
    }
}