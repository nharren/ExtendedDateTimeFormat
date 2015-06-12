using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class DesignatedDurationTests
    {
        [TestMethod]
        public void CanRoundTrip()
        {
            // Complete
            Assert.AreEqual("P111195Y3M12DT6H53M2S", DesignatedDuration.Parse("P111195Y3M12DT6H53M2S").ToString());
            Assert.AreEqual("P113W", DesignatedDuration.Parse("P113W").ToString());

            // Reduced
            Assert.AreEqual("P5Y5M5DT5H5M", DesignatedDuration.Parse("P5Y5M5DT5H5M").ToString());
            Assert.AreEqual("P5Y5M5DT5H", DesignatedDuration.Parse("P5Y5M5DT5H").ToString());
            Assert.AreEqual("P5Y5M5D", DesignatedDuration.Parse("P5Y5M5D").ToString());
            Assert.AreEqual("P5Y5M", DesignatedDuration.Parse("P5Y5M").ToString());
            Assert.AreEqual("P5Y", DesignatedDuration.Parse("P5Y").ToString());
            Assert.AreEqual("P5M5DT5H5M5S", DesignatedDuration.Parse("P5M5DT5H5M5S").ToString());
            Assert.AreEqual("P5M5DT5H5M", DesignatedDuration.Parse("P5M5DT5H5M").ToString());
            Assert.AreEqual("P5M5DT5H", DesignatedDuration.Parse("P5M5DT5H").ToString());
            Assert.AreEqual("P5M5D", DesignatedDuration.Parse("P5M5D").ToString());
            Assert.AreEqual("P5M", DesignatedDuration.Parse("P5M").ToString());
            Assert.AreEqual("P5DT5H5M5S", DesignatedDuration.Parse("P5DT5H5M5S").ToString());
            Assert.AreEqual("P5DT5H5M", DesignatedDuration.Parse("P5DT5H5M").ToString());
            Assert.AreEqual("P5DT5H", DesignatedDuration.Parse("P5DT5H").ToString());
            Assert.AreEqual("P5D", DesignatedDuration.Parse("P5D").ToString());
            Assert.AreEqual("PT5H5M5S", DesignatedDuration.Parse("PT5H5M5S").ToString());
            Assert.AreEqual("PT5H5M", DesignatedDuration.Parse("PT5H5M").ToString());
            Assert.AreEqual("PT5H", DesignatedDuration.Parse("PT5H").ToString());
            Assert.AreEqual("PT5M5S", DesignatedDuration.Parse("PT5M5S").ToString());
            Assert.AreEqual("PT5M", DesignatedDuration.Parse("PT5M").ToString());
            Assert.AreEqual("PT5M5S", DesignatedDuration.Parse("PT5M5S").ToString());
            Assert.AreEqual("PT5S", DesignatedDuration.Parse("PT5S").ToString());

            // Fractional
            Assert.AreEqual("P5Y5M5DT5H5M5,55S", DesignatedDuration.Parse("P5Y5M5DT5H5M5,55S").ToString());
            Assert.AreEqual("P5Y5M5DT5H5.55S", DesignatedDuration.Parse("P5Y5M5DT5H5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5Y5M5DT5,55S", DesignatedDuration.Parse("P5Y5M5D5,55S").ToString());
            Assert.AreEqual("P5Y5MT5.55S", DesignatedDuration.Parse("P5Y5MT5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5YT5,55S", DesignatedDuration.Parse("P5YT5,55S").ToString());
            Assert.AreEqual("PT5.55S", DesignatedDuration.Parse("PT5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5M5DT5H5M5,55S", DesignatedDuration.Parse("P5M5DT5H5M5,55S").ToString());
            Assert.AreEqual("P5M5DT5H5.55S", DesignatedDuration.Parse("P5M5DT5H5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5M5DT5,55S", DesignatedDuration.Parse("P5M5DT5,55S").ToString());
            Assert.AreEqual("P5MT5.55S", DesignatedDuration.Parse("P5MT5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5DT5H5M5,55S", DesignatedDuration.Parse("P5DT5H5M5,55S").ToString());
            Assert.AreEqual("P5DT5H5.55S", DesignatedDuration.Parse("P5DT5H5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("P5DT5,55S", DesignatedDuration.Parse("P5DT5,55S").ToString());
            Assert.AreEqual("PT5H5M5,55S", DesignatedDuration.Parse("PT5H5M5,55S").ToString());
            Assert.AreEqual("PT5H5.55S", DesignatedDuration.Parse("PT5H5.55S").ToString(new ISO8601FormatInfo { FractionLength = 2, DecimalSeparator = DecimalSeparator.Dot }));
            Assert.AreEqual("PT5M5,55S", DesignatedDuration.Parse("PT5M5,55S").ToString());

            Assert.AreEqual("P5Y5M5DT5H5,55M", DesignatedDuration.Parse("P5Y5M5DT5H5,55M").ToString());
            Assert.AreEqual("P5Y5M5DT5,55M", DesignatedDuration.Parse("P5Y5M5DT5,55M").ToString());
            Assert.AreEqual("P5Y5MT5,55M", DesignatedDuration.Parse("P5Y5MT5,55M").ToString());
            Assert.AreEqual("P5YT5,55M", DesignatedDuration.Parse("P5YT5,55M").ToString());
            Assert.AreEqual("PT5,55M", DesignatedDuration.Parse("PT5,55M").ToString());
            Assert.AreEqual("P5M5DT5H5,55M", DesignatedDuration.Parse("P5M5DT5H5,55M").ToString());
            Assert.AreEqual("P5M5DT5,55M", DesignatedDuration.Parse("P5M5DT5,55M").ToString());
            Assert.AreEqual("P5MT5,55M", DesignatedDuration.Parse("P5MT5,55M").ToString());
            Assert.AreEqual("P5DT5H5,55M", DesignatedDuration.Parse("P5DT5H5,55M").ToString());
            Assert.AreEqual("P5DT5,55M", DesignatedDuration.Parse("P5DT5,55M").ToString());
            Assert.AreEqual("PT5H5,55M", DesignatedDuration.Parse("PT5H5,55M").ToString());

            Assert.AreEqual("P5Y5M5DT5,55H", DesignatedDuration.Parse("P5Y5M5DT5,55H").ToString());
            Assert.AreEqual("P5Y5MT5,55H", DesignatedDuration.Parse("P5Y5MT5,55H").ToString());
            Assert.AreEqual("P5YT5,55H", DesignatedDuration.Parse("P5YT5,55H").ToString());
            Assert.AreEqual("PT5,55H", DesignatedDuration.Parse("PT5,55H").ToString());
            Assert.AreEqual("P5M5DT5,55H", DesignatedDuration.Parse("P5M5DT5,55H").ToString());
            Assert.AreEqual("P5MT5,55H", DesignatedDuration.Parse("P5MT5,55H").ToString());
            Assert.AreEqual("P5DT5,55H", DesignatedDuration.Parse("P5DT5,55H").ToString());

            Assert.AreEqual("P5Y5M5,55D", DesignatedDuration.Parse("P5Y5M5,55D").ToString());
            Assert.AreEqual("P5Y5,55D", DesignatedDuration.Parse("P5Y5,55D").ToString());
            Assert.AreEqual("P5,55D", DesignatedDuration.Parse("P5,55D").ToString());
            Assert.AreEqual("P5M5,55D", DesignatedDuration.Parse("P5M5,55D").ToString());

            Assert.AreEqual("P5Y5,55M", DesignatedDuration.Parse("P5Y5,55M").ToString());
            Assert.AreEqual("P5,55M", DesignatedDuration.Parse("P5,55M").ToString());

            Assert.AreEqual("P5,55Y", DesignatedDuration.Parse("P5,55Y").ToString());
        }
    }
}