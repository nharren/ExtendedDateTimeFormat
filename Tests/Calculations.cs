using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class Calculations
    {
        public static Calculation[] TotalMonthsCalculations =
        {
            new Calculation(() => ExtendedDateTimeCalculator.TotalMonths(new ExtendedDateTime(1963, 6, 1, 23, 50, 55, TimeSpan.Zero), new ExtendedDateTime(1999, 5, 3, 15, 5, 2, TimeSpan.Zero)).ToString(), string.Format("Start Date: {0}\nEnd Date: {1}", new ExtendedDateTime(1963, 6, 1, 23, 50, 55, TimeSpan.Zero), new ExtendedDateTime(1999, 5, 3, 15, 5, 2, TimeSpan.Zero))),
            new Calculation(() => ExtendedDateTimeCalculator.TotalMonths(new ExtendedDateTime(1934, 11, 30, 13, 49, 35, TimeSpan.Zero), new ExtendedDateTime(1988, 7, 12, 23, 0, 0, TimeSpan.Zero)).ToString(), string.Format("Start Date: {0}\nEnd Date: {1}", new ExtendedDateTime(1934, 11, 30, 13, 49, 35, TimeSpan.Zero), new ExtendedDateTime(1988, 7, 12, 23, 0, 0, TimeSpan.Zero)))
        };

        public static Calculation[] TotalYearsCalculations =
        {
            new Calculation(() => ExtendedDateTimeCalculator.TotalYears(new ExtendedDateTime(1963, 6, 1, 23, 50, 55, TimeSpan.Zero), new ExtendedDateTime(1999, 5, 3, 15, 5, 2, TimeSpan.Zero)).ToString(), string.Format("Start Date: {0}\nEnd Date: {1}", new ExtendedDateTime(1963, 6, 1, 23, 50, 55, TimeSpan.Zero), new ExtendedDateTime(1999, 5, 3, 15, 5, 2, TimeSpan.Zero))),
            new Calculation(() => ExtendedDateTimeCalculator.TotalYears(new ExtendedDateTime(1934, 11, 30, 13, 49, 35, TimeSpan.Zero), new ExtendedDateTime(1988, 7, 12, 23, 0, 0, TimeSpan.Zero)).ToString(), string.Format("Start Date: {0}\nEnd Date: {1}", new ExtendedDateTime(1934, 11, 30, 13, 49, 35, TimeSpan.Zero), new ExtendedDateTime(1988, 7, 12, 23, 0, 0, TimeSpan.Zero)))
        };
    }
}
