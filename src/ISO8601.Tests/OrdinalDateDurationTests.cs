using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P1234111", OrdinalDateDuration.Parse("P1234111").ToString(false));
            Assert.AreEqual("P1234-111", OrdinalDateDuration.Parse("P1234-111").ToString(true));

            // Extended
            Assert.AreEqual("P+11234111", OrdinalDateDuration.Parse("P+11234111", 5).ToString(false));
            Assert.AreEqual("P+11234-111", OrdinalDateDuration.Parse("P+11234-111", 5).ToString(true));
        }
    }
}
