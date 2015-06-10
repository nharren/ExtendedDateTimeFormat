using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class UnspecifiedExtendedDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Unspecified
            Assert.AreEqual("199u", UnspecifiedExtendedDateTime.Parse("199u").ToString());
            Assert.AreEqual("19uu", UnspecifiedExtendedDateTime.Parse("19uu").ToString());
            Assert.AreEqual("1999-uu", UnspecifiedExtendedDateTime.Parse("1999-uu").ToString());
            Assert.AreEqual("1999-01-uu", UnspecifiedExtendedDateTime.Parse("1999-01-uu").ToString());
            Assert.AreEqual("1999-uu-uu", UnspecifiedExtendedDateTime.Parse("1999-uu-uu").ToString());

            // Partial Unspecified
            Assert.AreEqual("156u-12-25", UnspecifiedExtendedDateTime.Parse("156u-12-25").ToString());
            Assert.AreEqual("15uu-12-25", UnspecifiedExtendedDateTime.Parse("15uu-12-25").ToString());
            Assert.AreEqual("15uu-12-uu", UnspecifiedExtendedDateTime.Parse("15uu-12-uu").ToString());
            Assert.AreEqual("1560-uu-25", UnspecifiedExtendedDateTime.Parse("1560-uu-25").ToString());
        }
    }
}