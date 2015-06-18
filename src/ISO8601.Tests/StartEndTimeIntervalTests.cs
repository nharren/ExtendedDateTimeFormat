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
            Assert.AreEqual("19501010T121212/19501010T121212", StartEndTimeInterval.Parse("19501010T121212/19501010T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("1950100T121212/1950100T121212", StartEndTimeInterval.Parse("1950100T121212/1950100T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
        }
    }
}