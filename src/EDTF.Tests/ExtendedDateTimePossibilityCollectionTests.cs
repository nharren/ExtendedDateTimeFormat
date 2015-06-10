using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class ExtendedDateTimePossibilityCollectionTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            Assert.AreEqual("[1667,1668,1670..1672]", ExtendedDateTimePossibilityCollection.Parse("[1667,1668,1670..1672]").ToString());
            Assert.AreEqual("[..1760-12-03]", ExtendedDateTimePossibilityCollection.Parse("[..1760-12-03]").ToString());
            Assert.AreEqual("[1760-12..]", ExtendedDateTimePossibilityCollection.Parse("[1760-12..]").ToString());
            Assert.AreEqual("[1760-01,1760-02,1760-12..]", ExtendedDateTimePossibilityCollection.Parse("[1760-01,1760-02,1760-12..]").ToString());
            Assert.AreEqual("[1667,1760-12]", ExtendedDateTimePossibilityCollection.Parse("[1667,1760-12]").ToString());
        }
    }
}