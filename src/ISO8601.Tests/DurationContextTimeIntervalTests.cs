using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class DurationContextTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("P111195Y3M12DT6H53M2S", DurationContextTimeInterval.Parse("P111195Y3M12DT6H53M2S").ToString());
            Assert.AreEqual("P12341212T121212", DurationContextTimeInterval.Parse("P12341212T121212").ToString(new ISO8601FormatInfo { UseComponentSeparators = false }));
        }
    }
}