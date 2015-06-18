using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("1950100", OrdinalDate.Parse("1950100").ToString(new ISO8601Options { UseComponentSeparators = false }));
            Assert.AreEqual("1950-100", OrdinalDate.Parse("1950-100").ToString());

            // Expanded
            Assert.AreEqual("+1950100", OrdinalDate.Parse("+1950100").ToString(new ISO8601Options { UseComponentSeparators = false, IsExpanded = true }));
            Assert.AreEqual("+11950100", OrdinalDate.Parse("+11950100", 5).ToString(new ISO8601Options { UseComponentSeparators = false, IsExpanded = true, YearLength = 5 }));
            Assert.AreEqual("+1950-100", OrdinalDate.Parse("+1950-100").ToString(new ISO8601Options { IsExpanded = true }));
            Assert.AreEqual("+11950-100", OrdinalDate.Parse("+11950-100", 5).ToString(new ISO8601Options { IsExpanded = true, YearLength = 5 }));
        }
    }
}