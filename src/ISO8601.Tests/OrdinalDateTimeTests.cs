using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class OrdinalDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("1950100121212", OrdinalDateTime.Parse("1950100121212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10012:12:12", OrdinalDateTime.Parse("1950-10012:12:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950100T121212", OrdinalDateTime.Parse("1950100T121212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-100T12:12:12", OrdinalDateTime.Parse("1950-100T12:12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950100121212+1212", OrdinalDateTime.Parse("1950100121212+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012:12:12+12:12", OrdinalDateTime.Parse("1950-10012:12:12+12:12").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950100T121212+1212", OrdinalDateTime.Parse("1950100T121212+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12:12:12+12:12", OrdinalDateTime.Parse("1950-100T12:12:12+12:12").ToString(true, true, DecimalSeparator.Comma, true));

            // Reduced
            Assert.AreEqual("19501001212", OrdinalDateTime.Parse("19501001212").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10012:12", OrdinalDateTime.Parse("1950-10012:12").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950100T1212", OrdinalDateTime.Parse("1950100T1212").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-100T12:12", OrdinalDateTime.Parse("1950-100T12:12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("195010012", OrdinalDateTime.Parse("195010012").ToString(false, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-10012", OrdinalDateTime.Parse("1950-10012").ToString(false, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950100T12", OrdinalDateTime.Parse("1950100T12").ToString(true, false, DecimalSeparator.Comma, false));
            Assert.AreEqual("1950-100T12", OrdinalDateTime.Parse("1950-100T12").ToString(true, true, DecimalSeparator.Comma, false));
            Assert.AreEqual("19501001212Z", OrdinalDateTime.Parse("19501001212Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012:12Z", OrdinalDateTime.Parse("1950-10012:12Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950100T1212Z", OrdinalDateTime.Parse("1950100T1212Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12:12Z", OrdinalDateTime.Parse("1950-100T12:12Z").ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("195010012Z", OrdinalDateTime.Parse("195010012Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012Z", OrdinalDateTime.Parse("1950-10012Z").ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950100T12Z", OrdinalDateTime.Parse("1950100T12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12Z", OrdinalDateTime.Parse("1950-100T12Z").ToString(true, true, DecimalSeparator.Comma, true));

            // Fractional
            Assert.AreEqual("1950100121212,12+1212", OrdinalDateTime.Parse("1950100121212,12+1212").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012:12:12.12+12:12", OrdinalDateTime.Parse("1950-10012:12:12.12+12:12").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950100T121212,12+1212", OrdinalDateTime.Parse("1950100T121212,12+1212").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12:12:12.12+12:12", OrdinalDateTime.Parse("1950-100T12:12:12.12+12:12").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("19501001212,12Z", OrdinalDateTime.Parse("19501001212,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012:12.12Z", OrdinalDateTime.Parse("1950-10012:12.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950100T1212,12Z", OrdinalDateTime.Parse("1950100T1212,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12:12.12Z", OrdinalDateTime.Parse("1950-100T12:12.12Z").ToString(true, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("195010012,12Z", OrdinalDateTime.Parse("195010012,12Z").ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-10012.12Z", OrdinalDateTime.Parse("1950-10012.12Z").ToString(false, true, DecimalSeparator.Dot, true));
            Assert.AreEqual("1950100T12,12Z", OrdinalDateTime.Parse("1950100T12,12Z").ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("1950-100T12.12Z", OrdinalDateTime.Parse("1950-100T12.12Z").ToString(true, true, DecimalSeparator.Dot, true));

            // Expanded
            Assert.AreEqual("+11950100121212+1212", OrdinalDateTime.Parse("+11950100121212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-10012:12:12+12:12", OrdinalDateTime.Parse("+11950-10012:12:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950100T121212+1212", OrdinalDateTime.Parse("+11950100T121212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-100T12:12:12+12:12", OrdinalDateTime.Parse("+11950-100T12:12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("+119501001212+1212", OrdinalDateTime.Parse("+119501001212+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("+11950-10012:12+12:12", OrdinalDateTime.Parse("+11950-10012:12+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950100T1212+1212", OrdinalDateTime.Parse("-11950100T1212+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-100T12:12+12:12", OrdinalDateTime.Parse("-11950-100T12:12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-1195010012+1212", OrdinalDateTime.Parse("-1195010012+1212", 5).ToString(false, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-10012+12:12", OrdinalDateTime.Parse("-11950-10012+12:12", 5).ToString(false, true, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950100T12+1212", OrdinalDateTime.Parse("-11950100T12+1212", 5).ToString(true, false, DecimalSeparator.Comma, true));
            Assert.AreEqual("-11950-100T12+12:12", OrdinalDateTime.Parse("-11950-100T12+12:12", 5).ToString(true, true, DecimalSeparator.Comma, true));
        }
    }
}
