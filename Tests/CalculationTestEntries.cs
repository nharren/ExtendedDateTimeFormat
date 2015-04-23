using System;
using System.ExtendedDateTimeFormat;

namespace Tests
{
    public static class CalculationTestEntries
    {
        public static CalculationTestEntry[] Difference =
        {
            new CalculationTestEntry("2012 - 2012", () => (new ExtendedDateTime(2012) - new ExtendedDateTime(2012)).ToString(), "00:00:00"),
            new CalculationTestEntry("2015 - 2012", () => (new ExtendedDateTime(2015) - new ExtendedDateTime(2012)).ToString(), "1096.00:00:00"),                                                                                                                 // 366 for 2012 (leap year) + 365 for 2013 + 365 for 2014 = 1096 days
            new CalculationTestEntry("2012-01 - 2012-01", () => (new ExtendedDateTime(2012, 1) - new ExtendedDateTime(2012, 1)).ToString(), "00:00:00"),
            new CalculationTestEntry("2012-02 - 2012-01", () => (new ExtendedDateTime(2012, 2) - new ExtendedDateTime(2012, 1)).ToString(), "31.00:00:00"),
            new CalculationTestEntry("2013-03 - 2012-03", () => (new ExtendedDateTime(2013, 3) - new ExtendedDateTime(2012, 3)).ToString(), "365.00:00:00"),                                                                                                      // 31 days for 3/2012 + 30 days for 4/2012 + 31 days for 5/2012 + 30 days for 6/2012 + 31 days for 7/2012 + 31 days for 8/2012 + 30 days for 9/2012 + 31 days for 10/2012 + 30 days for 11/2012 + 31 days for 12/2012 + 31 days for 1/2013 + 28 days for 2/2013 = 365 days
            new CalculationTestEntry("2012-02-02 - 2012-02-02", () => (new ExtendedDateTime(2012, 2, 2) - new ExtendedDateTime(2012, 2, 2)).ToString(), "00:00:00"),
            new CalculationTestEntry("2012-11-20 - 2012-02-02", () => (new ExtendedDateTime(2012, 11, 20) - new ExtendedDateTime(2012, 2, 2)).ToString(), "292.00:00:00"),                                                                                        // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November = 292 days
            new CalculationTestEntry("2012-03-03T03Z - 2012-03-03T03Z", () => (new ExtendedDateTime(2012, 3, 3, 3, TimeSpan.Zero) - new ExtendedDateTime(2012, 3, 3, 3, TimeSpan.Zero)).ToString(), "00:00:00"),
            new CalculationTestEntry("2012-03-03T03:03Z - 2012-03-03T03:03Z", () => (new ExtendedDateTime(2012, 3, 3, 3, 3, TimeSpan.Zero) - new ExtendedDateTime(2012, 3, 3, 3, 3, TimeSpan.Zero)).ToString(), "00:00:00"),
            new CalculationTestEntry("2012-03-03T03:03:03Z - 2012-03-03T03:03:03Z", () => (new ExtendedDateTime(2012, 3, 3, 3, 3, 3, TimeSpan.Zero) - new ExtendedDateTime(2012, 3, 3, 3, 3, 3, TimeSpan.Zero)).ToString(), "00:00:00"),
            new CalculationTestEntry("2012-11-20T00:00:00-08:00 - 2012-02-02T00:00:00Z", () => (new ExtendedDateTime(2012, 11, 20, 0, 0, 0, new TimeSpan(-8, 0, 0)) - new ExtendedDateTime(2012, 2, 2, 0, 0, 0, TimeSpan.Zero)).ToString(), "291.16:00:00"),      // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November - 8 hours behind = 291.16 days
        };

        public static CalculationTestEntry[] TotalMonths =
        {
            new CalculationTestEntry("2012-02-02\\2012-11-20", () => ExtendedDateTimeCalculator.TotalMonths(new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)).ToString("R"), "9.5988505747126442"),                                           // 28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November = 9.5988505747126442 months. "R" in ToString prints the double with the full 17 digit precision. The default is 15 (http://stackoverflow.com/questions/2105096/why-is-tostring-rounding-my-double-value).
        };

        public static CalculationTestEntry[] TotalYears =
        {
            new CalculationTestEntry("2012-02-02\\2012-11-20", () => ExtendedDateTimeCalculator.TotalYears(new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)).ToString("R"), "0.799904214559387"),                                             // (28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November) / 12 months per year = 0.799904214559387 years
        };

        public static CalculationTestEntry[] AddMonths =
        {
            new CalculationTestEntry("2012-02-02 + 0 months", () => new ExtendedDateTime(2012, 2, 2).AddMonths(0).ToString(), "2012-02-02"),
            new CalculationTestEntry("2012-02-02 + 5 months", () => new ExtendedDateTime(2012, 2, 2).AddMonths(5).ToString(), "2012-07-02"),
            new CalculationTestEntry("2012-02-02 + 15 months", () => new ExtendedDateTime(2012, 2, 2).AddMonths(15).ToString(), "2013-05-02"),                                                                                                                    // 11 months = 2012-02-02 to 2013-01-02 + 4 months = 2013-01-02 to 2013-05-02
            new CalculationTestEntry("2012-02-02 + 30 months", () => new ExtendedDateTime(2012, 2, 2).AddMonths(30).ToString(), "2014-08-02")                                                                                                                     // 12 months = 2012-02-02 to 2013-02-02 + 12 months = 2013-02-02 to 2014-02-02 + 6 months = 2014-02-02 to 2014-08-02
        };

        public static CalculationTestEntry[] AddYears =
        {
            new CalculationTestEntry("2012-02-02 + 0 years", () => new ExtendedDateTime(2012, 2, 2).AddYears(0).ToString(), "2012-02-02"),
            new CalculationTestEntry("2012-02-02 + 5 years", () => new ExtendedDateTime(2012, 2, 2).AddYears(5).ToString(), "2017-02-02"),
            new CalculationTestEntry("2012-02-02 + 15 years", () => new ExtendedDateTime(2012, 2, 2).AddYears(15).ToString(), "2027-02-02"),
            new CalculationTestEntry("2012-02-02 + 30 years", () => new ExtendedDateTime(2012, 2, 2).AddYears(30).ToString(), "2042-02-02")
        };
    }
}