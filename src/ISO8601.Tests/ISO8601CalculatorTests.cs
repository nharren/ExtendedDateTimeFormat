using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ISO8601;

namespace ISO8601.Tests
{
    [TestClass]
    public class ISO8601CalculatorTests
    {
        [TestMethod]
        public void CalculatesCorrectCalendarDateDifference()
        {
            Assert.AreEqual(1, (new CalendarDate(2015, 6, 24) - new CalendarDate(2015, 6, 23)).Days);
            Assert.AreEqual(31, (new CalendarDate(2015, 2, 1) - new CalendarDate(2015, 1, 1)).Days);
            Assert.AreEqual(2922, (new CalendarDate(2004, 6, 24) - new CalendarDate(1996, 6, 24)).Days);
            Assert.AreEqual(3287, (new CalendarDate(2005, 6, 24) - new CalendarDate(1996, 6, 24)).Days);
            Assert.AreEqual(50547, (new CalendarDate(2015, 6, 23) - new CalendarDate(1877, 1, 30)).Days);
        }
    }
}