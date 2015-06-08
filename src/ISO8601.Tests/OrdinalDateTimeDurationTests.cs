using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateTimeDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P1234111T121212", OrdinalDateTimeDuration.Parse("P1234111T121212").ToString(false));
            Assert.AreEqual("P1234-111T12:12:12", OrdinalDateTimeDuration.Parse("P1234-111T12:12:12").ToString(true));

            // Reduced
            Assert.AreEqual("P1234111T1212", OrdinalDateTimeDuration.Parse("P1234111T1212").ToString(false));
            Assert.AreEqual("P1234-111T12:12", OrdinalDateTimeDuration.Parse("P1234-111T12:12").ToString(true));
            Assert.AreEqual("P1234111T12", OrdinalDateTimeDuration.Parse("P1234111T12").ToString(false));

            // Fractional
            Assert.AreEqual("P1234111T121212,12", OrdinalDateTimeDuration.Parse("P1234111T121212,12").ToString(false, fractionLength: 2));
            Assert.AreEqual("P1234-111T12:12:12.12", OrdinalDateTimeDuration.Parse("P1234-111T12:12:12.12").ToString(true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P1234111T1212,12", OrdinalDateTimeDuration.Parse("P1234111T1212,12").ToString(false, fractionLength: 2));
            Assert.AreEqual("P1234-111T12:12.12", OrdinalDateTimeDuration.Parse("P1234-111T12:12.12").ToString(true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P1234111T12,12", OrdinalDateTimeDuration.Parse("P1234111T12,12").ToString(false, fractionLength: 2));

            // Expanded
            Assert.AreEqual("P+1234111T121212", OrdinalDateTimeDuration.Parse("P+1234111T121212").ToString(false, true));
            Assert.AreEqual("P+11234-111T12:12:12", OrdinalDateTimeDuration.Parse("P+11234-111T12:12:12", 5).ToString(true, true, 5));
            Assert.AreEqual("P+1234111T1212", OrdinalDateTimeDuration.Parse("P+1234111T1212").ToString(false, true));
            Assert.AreEqual("P+11234-111T12:12", OrdinalDateTimeDuration.Parse("P+11234-111T12:12", 5).ToString(true, true, 5));
            Assert.AreEqual("P+1234111T12", OrdinalDateTimeDuration.Parse("P+1234111T12").ToString(false, true));
            Assert.AreEqual("P-11234111T121212,12", OrdinalDateTimeDuration.Parse("P-11234111T121212,12", 5).ToString(false, true, 5, 2));
            Assert.AreEqual("P-1234-111T12:12:12.12", OrdinalDateTimeDuration.Parse("P-1234-111T12:12:12.12").ToString(true, true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P-11234111T1212,12", OrdinalDateTimeDuration.Parse("P-11234111T1212,12", 5).ToString(false, true, 5, 2));
            Assert.AreEqual("P-1234-111T12:12.12", OrdinalDateTimeDuration.Parse("P-1234-111T12:12.12").ToString(true, true, fractionLength: 2, decimalSeparator: DecimalSeparator.Dot));
            Assert.AreEqual("P-11234111T12,12", OrdinalDateTimeDuration.Parse("P-11234111T12,12", 5).ToString(false, true, 5, 2));
        }
    }
}
