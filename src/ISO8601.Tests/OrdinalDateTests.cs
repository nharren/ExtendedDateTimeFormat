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
            Assert.AreEqual("1950100", OrdinalDate.Parse("1950100").ToString(false));
            Assert.AreEqual("1950-100", OrdinalDate.Parse("1950-100").ToString(true));

            Assert.AreEqual("+1950100", OrdinalDate.Parse("+1950100").ToString(false));
            Assert.AreEqual("+11950100", OrdinalDate.Parse("+11950100", 5).ToString(false));
            Assert.AreEqual("+1950-100", OrdinalDate.Parse("+1950-100").ToString(true));
            Assert.AreEqual("+11950-100", OrdinalDate.Parse("+11950-100", 5).ToString(true));
        }
    }
}