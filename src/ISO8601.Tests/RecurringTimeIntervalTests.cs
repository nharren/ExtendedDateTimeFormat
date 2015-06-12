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
            Assert.AreEqual("R/19501010T121212/19501010T121212", RecurringTimeInterval.Parse("R/19501010T121212/19501010T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R15/1950100T121212/1950100T121212", RecurringTimeInterval.Parse("R15/1950100T121212/1950100T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R/P111195Y3M12DT6H53M2S", RecurringTimeInterval.Parse("R/P111195Y3M12DT6H53M2S").ToString());
            Assert.AreEqual("R5/P12341212T121212", RecurringTimeInterval.Parse("R5/P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R/19501010T121212/P12341212T121212", RecurringTimeInterval.Parse("R/19501010T121212/P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R100/1950100T121212/P12341212T121212", RecurringTimeInterval.Parse("R100/1950100T121212/P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("RP12341212T121212/19501010T121212", RecurringTimeInterval.Parse("RP12341212T121212/19501010T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
            Assert.AreEqual("R10P12341212T121212/1950100T121212", RecurringTimeInterval.Parse("R10P12341212T121212/1950100T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }, new ISO8601FormatInfo { UseComponentSeparators = false, UseUtcOffset = false }));
        }
    }
}