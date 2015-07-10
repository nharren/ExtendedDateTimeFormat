using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.EDTF;

namespace EDTF.Tests
{
    [TestClass]
    public class ExtendedDateTimeCalculatorTests
    {
        [TestMethod]
        public void CalculatesCorrectDifference()
        {
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012") - ExtendedDateTime.Parse("2012")));
            Assert.AreEqual(TimeSpan.FromDays(1096), (ExtendedDateTime.Parse("2015") - ExtendedDateTime.Parse("2012")));       // 366 for 2012 (leap year) + 365 for 2013 + 365 for 2014 = 1096 days
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012-01") - ExtendedDateTime.Parse("2012-01")));
            Assert.AreEqual(TimeSpan.FromDays(31), (ExtendedDateTime.Parse("2012-02") - ExtendedDateTime.Parse("2012-01")));
            Assert.AreEqual(TimeSpan.FromDays(365), (ExtendedDateTime.Parse("2013-03") - ExtendedDateTime.Parse("2012-03")));      // 31 days for 3/2012 + 30 days for 4/2012 + 31 days for 5/2012 + 30 days for 6/2012 + 31 days for 7/2012 + 31 days for 8/2012 + 30 days for 9/2012 + 31 days for 10/2012 + 30 days for 11/2012 + 31 days for 12/2012 + 31 days for 1/2013 + 28 days for 2/2013 = 365 days
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012-02-02") - ExtendedDateTime.Parse("2012-02-02")));
            Assert.AreEqual(TimeSpan.FromDays(292), (ExtendedDateTime.Parse("2012-11-20") - ExtendedDateTime.Parse("2012-02-02")));      // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November = 292 days
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012-03-03T03Z") - ExtendedDateTime.Parse("2012-03-03T03Z")));
            Assert.AreEqual(new TimeSpan(292, 18, 0, 0), (ExtendedDateTime.Parse("2012-11-20T20Z") - ExtendedDateTime.Parse("2012-02-02T02Z")));       // 20 additional hours passed after the end day - 2 hours in to the beginning day = 18 additional hours
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012-03-03T03:03Z") - ExtendedDateTime.Parse("2012-03-03T03:03Z")));
            Assert.AreEqual(new TimeSpan(292, 18, 18, 0), (ExtendedDateTime.Parse("2012-11-20T20:20Z") - ExtendedDateTime.Parse("2012-02-02T02:02Z")));        // 20 additional minutes passed after the end hour - 2 minutes in to the beginning hour = 18 additional minutes
            Assert.AreEqual(TimeSpan.Zero, (ExtendedDateTime.Parse("2012-03-03T03:03:03Z") - ExtendedDateTime.Parse("2012-03-03T03:03:03Z")));
            Assert.AreEqual(new TimeSpan(292, 18, 18, 18), (ExtendedDateTime.Parse("2012-11-20T20:20:20Z") - ExtendedDateTime.Parse("2012-02-02T02:02:02Z")));      // 20 additional seconds passed after the end minute - 2 seconds in to the beginning minute = 18 additional seconds
            Assert.AreEqual(new TimeSpan(291, 16, 0, 0), (ExtendedDateTime.Parse("2012-11-20T00:00:00-08:00") - ExtendedDateTime.Parse("2012-02-02T00:00:00Z")));      // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November - 8 hours behind = 291.16 days
        }

        //[TestMethod]
        //public void CalculatesCorrectTotalMonths()
        //{
        //    Assert.AreEqual(9.5988505747126442, (ExtendedDateTime.Parse("2012-11-20") - ExtendedDateTime.Parse("2012-02-02")).TotalMonths);     // 28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November = 9.5988505747126442 months. "R" in ToString prints the double with the full 17 digit precision. The default is 15 (http://stackoverflow.com/questions/2105096/why-is-tostring-rounding-my-double-value).
        //}

        //[TestMethod]
        //public void CalculatesCorrectTotalYears()
        //{
        //    Assert.AreEqual(0.799904214559387, (ExtendedDateTime.Parse("2012-11-20") - ExtendedDateTime.Parse("2012-02-02")).TotalYears);       // (28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November) / 12 months per year = 0.799904214559387 years
        //}

        [TestMethod]
        public void CalculatesCorrectAddMonths()
        {
            Assert.AreEqual("2012-02-02", new ExtendedDateTime(2012, 2, 2).AddMonths(0).ToString());
            Assert.AreEqual("2012-07-02", new ExtendedDateTime(2012, 2, 2).AddMonths(5).ToString());
            Assert.AreEqual("2013-05-02", new ExtendedDateTime(2012, 2, 2).AddMonths(15).ToString());       // 11 months = 2012-02-02 to 2013-01-02 + 4 months = 2013-01-02 to 2013-05-02
            Assert.AreEqual("2014-08-02", new ExtendedDateTime(2012, 2, 2).AddMonths(30).ToString());       // 12 months = 2012-02-02 to 2013-02-02 + 12 months = 2013-02-02 to 2014-02-02 + 6 months = 2014-02-02 to 2014-08-02
        }

        [TestMethod]
        public void CalculatesCorrectAddYears()
        {
            Assert.AreEqual("2012-02-02", new ExtendedDateTime(2012, 2, 2).AddYears(0).ToString());
            Assert.AreEqual("2017-02-02", new ExtendedDateTime(2012, 2, 2).AddYears(5).ToString());
            Assert.AreEqual("2027-02-02", new ExtendedDateTime(2012, 2, 2).AddYears(15).ToString());
            Assert.AreEqual("2042-02-02", new ExtendedDateTime(2012, 2, 2).AddYears(30).ToString());
        }
    }
}