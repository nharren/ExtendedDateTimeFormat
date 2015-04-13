﻿using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class CalculationTestEntries
    {
        public static CalculationTestEntry[] TotalMonths =
        {
            new CalculationTestEntry(string.Format("{0}\\{1}", new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)), () => ExtendedDateTimeCalculator.TotalMonths(new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)).ToString("R"), "9.5988505747126442"),                   // 28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November = 9.5988505747126442 months. "R" in ToString prints the double with the full 17 digit precision. The default is 15 (http://stackoverflow.com/questions/2105096/why-is-tostring-rounding-my-double-value).
        };

        public static CalculationTestEntry[] TotalYears =
        {
            new CalculationTestEntry(string.Format("{0}\\{1}", new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)), () => ExtendedDateTimeCalculator.TotalYears(new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)).ToString("R"), "0.799904214559387"),                    // (28 days remaining / 29 days in February + 8 months from March to November + 19 days passed / 30 days in November) / 12 months per year = 0.799904214559387 years
        };

        public static CalculationTestEntry[] Difference =
        {
            new CalculationTestEntry(string.Format("{0}\\{1}", new ExtendedDateTime(2012, 2, 2), new ExtendedDateTime(2012, 11, 20)), () => (new ExtendedDateTime(2012, 11, 20) - new ExtendedDateTime(2012, 2, 2)).ToString(), "292.00:00:00"),                                                                // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November = 292 days
        };
    }
}