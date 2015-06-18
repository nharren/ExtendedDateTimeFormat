using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class RecurringTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("R/19501010T121212/19501010T121212", RecurringTimeInterval.Parse("R/19501010T121212/19501010T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R15/1950100T121212/1950100T121212", RecurringTimeInterval.Parse("R15/1950100T121212/1950100T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R/19501010T121212/P12341212T121212", RecurringTimeInterval.Parse("R/19501010T121212/P12341212T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R100/1950100T121212/P12341212T121212", RecurringTimeInterval.Parse("R100/1950100T121212/P12341212T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("RP12341212T121212/19501010T121212", RecurringTimeInterval.Parse("RP12341212T121212/19501010T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R10P12341212T121212/1950100T121212", RecurringTimeInterval.Parse("R10P12341212T121212/1950100T121212").ToString(new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601Options { UseComponentSeparators = false, UseUtcOffset = false }));
        }
    }
}