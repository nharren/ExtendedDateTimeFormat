using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class ExtendedDateTimeCollectionTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("{1667,1668,1670..1672}", ExtendedDateTimeCollection.Parse("{1667,1668,1670..1672}").ToString());
            Assert.AreEqual("{1960,1961-12}", ExtendedDateTimeCollection.Parse("{1960,1961-12}").ToString());
        }
    }
}