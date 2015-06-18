using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class DurationEndTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("P12341212T121212/19501010T121212", DurationEndTimeInterval.Parse("P12341212T121212/19501010T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("P12341212T121212/1950100T121212", DurationEndTimeInterval.Parse("P12341212T121212/1950100T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
        }
    }
}