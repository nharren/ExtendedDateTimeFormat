using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class ExtendedDateTimeIntervalTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Level Zero Interval
            Assert.AreEqual("1964/2008", ExtendedDateTimeInterval.Parse("1964/2008").ToString());
            Assert.AreEqual("2004-06/2006-08", ExtendedDateTimeInterval.Parse("2004-06/2006-08").ToString());
            Assert.AreEqual("2004-02-01/2005-02-08", ExtendedDateTimeInterval.Parse("2004-02-01/2005-02-08").ToString());
            Assert.AreEqual("2004-02-01/2005-02", ExtendedDateTimeInterval.Parse("2004-02-01/2005-02").ToString());
            Assert.AreEqual("2004-02-01/2005", ExtendedDateTimeInterval.Parse("2004-02-01/2005").ToString());
            Assert.AreEqual("2005/2006-02", ExtendedDateTimeInterval.Parse("2005/2006-02").ToString());

            // Level One Interval
            Assert.AreEqual("unknown/2006", ExtendedDateTimeInterval.Parse("unknown/2006").ToString());
            Assert.AreEqual("2004-06-01/unknown", ExtendedDateTimeInterval.Parse("2004-06-01/unknown").ToString());
            Assert.AreEqual("2004-01-01/open", ExtendedDateTimeInterval.Parse("2004-01-01/open").ToString());
            Assert.AreEqual("1984~/2004-06", ExtendedDateTimeInterval.Parse("1984~/2004-06").ToString());
            Assert.AreEqual("1984/2004-06~", ExtendedDateTimeInterval.Parse("1984/2004-06~").ToString());
            Assert.AreEqual("1984~/2004~", ExtendedDateTimeInterval.Parse("1984~/2004~").ToString());
            Assert.AreEqual("1984?/2004?~", ExtendedDateTimeInterval.Parse("1984?/2004?~").ToString());
            Assert.AreEqual("1984-06?/2004-08?", ExtendedDateTimeInterval.Parse("1984-06?/2004-08?").ToString());
            Assert.AreEqual("1984-06-02?/2004-08-08~", ExtendedDateTimeInterval.Parse("1984-06-02?/2004-08-08~").ToString());
            Assert.AreEqual("1984-06-02?/unknown", ExtendedDateTimeInterval.Parse("1984-06-02?/unknown").ToString());

            // Level Two Interval
            Assert.AreEqual("2004-06-(01)~/2004-06-(20)~", ExtendedDateTimeInterval.Parse("2004-06-(01)~/2004-06-(20)~").ToString());
            Assert.AreEqual("2004-06-uu/2004-07-03", ExtendedDateTimeInterval.Parse("2004-06-uu/2004-07-03").ToString());
        }
    }
}