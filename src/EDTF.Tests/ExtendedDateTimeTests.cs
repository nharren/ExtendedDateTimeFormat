using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class ExtendedDateTimeTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Date
            Assert.AreEqual("2001-02-03", ExtendedDateTime.Parse("2001-02-03").ToString());
            Assert.AreEqual("2008-12", ExtendedDateTime.Parse("2008-12").ToString());
            Assert.AreEqual("2008", ExtendedDateTime.Parse("2008").ToString());
            Assert.AreEqual("-0999", ExtendedDateTime.Parse("-0999").ToString());
            Assert.AreEqual("0000", ExtendedDateTime.Parse("0000").ToString());

            // Date and Time
            Assert.AreEqual("2001-02-03T09:30:01-08", ExtendedDateTime.Parse("2001-02-03T09:30:01-08").ToString());
            Assert.AreEqual("2004-01-01T10:10:10Z", ExtendedDateTime.Parse("2004-01-01T10:10:10Z").ToString());
            Assert.AreEqual("2004-01-01T10:10:10+05", ExtendedDateTime.Parse("2004-01-01T10:10:10+05").ToString());

            // Uncertain and Approximate
            Assert.AreEqual("1984?", ExtendedDateTime.Parse("1984?").ToString());
            Assert.AreEqual("2004-06?", ExtendedDateTime.Parse("2004-06?").ToString());
            Assert.AreEqual("2004-06-11?", ExtendedDateTime.Parse("2004-06-11?").ToString());
            Assert.AreEqual("1984~", ExtendedDateTime.Parse("1984~").ToString());
            Assert.AreEqual("1984?~", ExtendedDateTime.Parse("1984?~").ToString());

            // Years Exceeding Four Digits
            Assert.AreEqual("y170000002", ExtendedDateTime.Parse("y170000002").ToString());
            Assert.AreEqual("y-170000002", ExtendedDateTime.Parse("y-170000002").ToString());

            // Season
            Assert.AreEqual("2001-21", ExtendedDateTime.Parse("2001-21").ToString());
            Assert.AreEqual("2001-22", ExtendedDateTime.Parse("2001-22").ToString());
            Assert.AreEqual("2001-23", ExtendedDateTime.Parse("2001-23").ToString());
            Assert.AreEqual("2001-24", ExtendedDateTime.Parse("2001-24").ToString());

            // Partial Uncertain or Approximate
            Assert.AreEqual("2004?-06-11", ExtendedDateTime.Parse("2004?-06-11").ToString());
            Assert.AreEqual("2004-06~-11", ExtendedDateTime.Parse("2004-06~-11").ToString());
            Assert.AreEqual("2004-(06)?-11", ExtendedDateTime.Parse("2004-(06)?-11").ToString());
            Assert.AreEqual("2004-06-(11)~", ExtendedDateTime.Parse("2004-06-(11)~").ToString());
            Assert.AreEqual("2004-(06)?~", ExtendedDateTime.Parse("2004-(06)?~").ToString());
            Assert.AreEqual("2004?-06-(11)~", ExtendedDateTime.Parse("2004?-06-(11)~").ToString());
            Assert.AreEqual("2004?-(06)?~", ExtendedDateTime.Parse("2004?-(06)?~").ToString());
            Assert.AreEqual("(2004)?-06-04~", ExtendedDateTime.Parse("(2004)?-06-04~").ToString());
            Assert.AreEqual("(2011)-06-04~", ExtendedDateTime.Parse("(2011)-06-04~").ToString());
            Assert.AreEqual("2011-23~", ExtendedDateTime.Parse("2011-23~").ToString());

            // Exponential Form of Years Exceeding Four Digits
            Assert.AreEqual("y17e7", ExtendedDateTime.Parse("y17e7").ToString());
            Assert.AreEqual("y-17e7", ExtendedDateTime.Parse("y-17e7").ToString());
            Assert.AreEqual("y17101e4p3", ExtendedDateTime.Parse("y17101e4p3").ToString());
        }
    }
}