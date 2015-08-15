namespace System.EDTF
{
    public static class ExtendedDateTimeCalculator
    {
        private const int DaysPer100Years = DaysPer4Years * 25 - 1;
        private const int DaysPer400Years = DaysPer100Years * 4 + 1;
        private const int DaysPer4Years = DaysPerYear * 4 + 1;
        private const int DaysPerYear = 365;
        private static readonly int[] DayOfWeekCenturyKeys = { 6, 4, 2, 0 };
        private static readonly int[] DayOfWeekMonthKeys365 = { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DayOfWeekMonthKeys366 = { 0, 6, 2, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth365 = { 0, 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

        public static ExtendedDateTime AddMonths(ExtendedDateTime e, int monthsToAdd)
        {
            var monthTotal = e.Month + monthsToAdd;

            var month = monthTotal % 12;
            var year = e.Year + monthTotal / 12;

            if (e.Day > DaysInMonth(year, month))
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            var extendedDateTime = new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset.Hours, e.UtcOffset.Minutes);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        public static int CenturyOfYear(int year)
        {
            return year / 100;
        }

        public static int DaysInMonth(int year, int month)
        {
            return month == 2 && IsLeapYear(year) ? 29 : DaysInMonthArray[month];
        }

        public static int DaysInYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static int DaysToMonth(int year, int month)
        {
            return IsLeapYear(year) ? DaysToMonth366[month] : DaysToMonth365[month];
        }

        public static bool IsLeapYear(int year) // http://www.timeanddate.com/date/leapyear.html
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        public static int YearOfCentury(int year)
        {
            return year % 100;
        }

        internal static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
        {
            var extendedDateTime = TimeSpanToExtendedDateTime((e - new ExtendedDateTime(1, 1, 1)) + t, e.UtcOffset);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        internal static ExtendedDateTime AddYears(ExtendedDateTime e, int count)
        {
            if (e.Month == 2 && e.Day == 29 && IsLeapYear(e.Year) && !IsLeapYear(e.Year + count))
            {
                throw new InvalidOperationException("Years cannot be added to a leap day unless the resulting year also has a leap day.");
            }

            var extendedDateTime = new ExtendedDateTime(e.Year + count, e.Month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset.Hours, e.UtcOffset.Minutes);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        internal static DayOfWeek DayOfWeek(ExtendedDateTime e) // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
        {
            var yearOfCentury = YearOfCentury(e.Year);
            var centuryOfYear = CenturyOfYear(e.Year);

            var monthKey = IsLeapYear(e.Year) ? DayOfWeekMonthKeys366[e.Month] : DayOfWeekMonthKeys365[e.Month];
            var centuryKey = DayOfWeekCenturyKeys[centuryOfYear % 4 + centuryOfYear < 0 ? 4 : 0];

            var dayOfWeek = (e.Day + monthKey + yearOfCentury + yearOfCentury / 4 + centuryKey) % 7;

            return (DayOfWeek)dayOfWeek;
        }

        internal static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            var extendedDateTime = TimeSpanToExtendedDateTime((e - new ExtendedDateTime(1, 1, 1)) - t, e.UtcOffset);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        internal static TimeSpan Subtract(ExtendedDateTime later, ExtendedDateTime earlier)
        {
            return TimeSpan.FromDays(
                         (later.Year - earlier.Year) * 365
                       + (later.Year - 1) / 4 - (earlier.Year - 1) / 4
                       - (later.Year - 1) / 100 + (earlier.Year - 1) / 100
                       + (later.Year - 1) / 400 - (earlier.Year - 1) / 400
                       + DaysToMonth(later.Year, later.Month)
                       - DaysToMonth(earlier.Year, earlier.Month)
                       + later.Day
                       - earlier.Day)
                 + TimeSpan.FromHours(
                         later.Hour - earlier.Hour
                       + later.UtcOffset.Hours - earlier.UtcOffset.Hours)
                 + TimeSpan.FromMinutes(
                         later.Minute - earlier.Minute
                       + later.UtcOffset.Minutes - earlier.UtcOffset.Minutes)
                 + TimeSpan.FromSeconds(later.Second - earlier.Second);
        }

        internal static ExtendedDateTime SubtractMonths(ExtendedDateTime e, int count)
        {
            var month = e.Month - count % 12;
            var year = e.Year - count / 12;

            if (e.Day > DaysInMonth(year, month))
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            var extendedDateTime = new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset.Hours, e.UtcOffset.Minutes);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        internal static ExtendedDateTime SubtractYears(ExtendedDateTime e, int count)
        {
            if (e.Month == 2 && e.Day == 29 && IsLeapYear(e.Year) && !IsLeapYear(e.Year - count))
            {
                throw new InvalidOperationException("The years subtracted from a leap day must result in another leap day.");
            }

            var extendedDateTime = new ExtendedDateTime(e.Year - count, e.Month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset.Hours, e.UtcOffset.Minutes);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = e.Precision;

            return extendedDateTime;
        }

        internal static ExtendedDateTime TimeSpanToExtendedDateTime(TimeSpan t, TimeSpan utcOffset)
        {
            // To determine the precise date and time, we will use the following method:
            //     1. Add the number of days (rounded down) from 1 CE to the starting date to the number of days in the added duration.
            //     2. Determine the number of whole 400 years periods which have elapsed since 1 CE.
            //     3. Subtract the number of days elapsed during the 400 year periods from the day total.
            //     4. Repeat steps 2 and 3 for 100, 4, and 1 year periods. The remaining days will represent the number of days since the first of january.
            //     5. Add one to the day total to get the ordinal day of the year.
            //     6. To determine the month, we make an estimate by dividing the number of days by 32. We use 32 since it is greater
            //        than the number of days in any month and it is an easy number to divide using bitwise division (since 32 = 2^5).
            //        The actual month will be equal to or greater than the estimate because at the end of each 32 day cycle, the month
            //        will have already advanced.
            //     7. To find the actual month, we increment the month until the day total is greater than or equal to the
            //        number of days between the first of january and the start of the month.
            //     8. Subtract the number of days between the first of january and the start of the month from the day total to get
            //        the number of days from the first of the month to the day of the month.
            //     9. Add one to the day total to get the day of the month.
            //    10. The remainders of the total hours, minutes, and seconds when divided by their carry-over values will give
            //        the correct time.

            var totalDays = (int)t.TotalDays;

            int fourHundredYearPeriods = totalDays / DaysPer400Years;
            totalDays -= fourHundredYearPeriods * DaysPer400Years;

            int oneHundredYearPeriods = totalDays / DaysPer100Years;
            totalDays -= oneHundredYearPeriods * DaysPer100Years;

            int fourYearPeriods = totalDays / DaysPer4Years;
            totalDays -= fourYearPeriods * DaysPer4Years;

            int oneYearPeriods = totalDays / DaysPerYear;
            totalDays -= oneYearPeriods * DaysPerYear;

            int year = fourHundredYearPeriods * 400 + oneHundredYearPeriods * 100 + fourYearPeriods * 4 + oneYearPeriods + 1;

            int dayOfYear = totalDays + 1;

            int month = totalDays / 32 + 1;

            while (month < 12 && totalDays >= DaysToMonth(year, month + 1))
            {
                month++;
            }

            totalDays -= DaysToMonth(year, month);

            totalDays++;

            var day = totalDays;
            var hour = (int)(t.TotalHours % 24);
            var minute = (int)(t.TotalMinutes % 60);
            var second = (int)(t.TotalSeconds % 60);

            return new ExtendedDateTime(year, month, totalDays, hour, minute, second, utcOffset.Hours, utcOffset.Minutes);
        }

        internal static ExtendedDateTime ToRoundedPrecision(ExtendedDateTime e, ExtendedDateTimePrecision p, bool roundUp = false)
        {
            var year = e.Year;
            var month = e.Month;
            var day = e.Day;
            var hour = e.Hour;
            var minute = e.Minute;
            var second = e.Second;

            if (p > ExtendedDateTimePrecision.Second)
            {
                second = 0;
            }

            if (p > ExtendedDateTimePrecision.Minute)
            {
                minute = 0;
            }
            else if (roundUp)
            {
                minute++;
            }

            if (p > ExtendedDateTimePrecision.Hour)
            {
                hour = 0;
            }
            else if (roundUp)
            {
                hour++;
            }

            if (p > ExtendedDateTimePrecision.Day)
            {
                day = 1;
            }
            else if (roundUp)
            {
                day++;
            }

            if (p > ExtendedDateTimePrecision.Month)
            {
                month = 1;
            }
            else if (roundUp)
            {
                month++;
            }

            var extendedDateTime = new ExtendedDateTime(year, month, day, hour, minute, second, e.UtcOffset.Hours, e.UtcOffset.Minutes);
            extendedDateTime.YearFlags = e.YearFlags;
            extendedDateTime.MonthFlags = e.MonthFlags;
            extendedDateTime.DayFlags = e.DayFlags;
            extendedDateTime.Precision = p;

            return extendedDateTime;
        }
    }
}