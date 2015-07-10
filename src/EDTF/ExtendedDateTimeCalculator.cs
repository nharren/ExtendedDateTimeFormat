using System.Linq;

namespace System.EDTF
{
    public static class ExtendedDateTimeCalculator
    {
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth365 = { 0, 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

        public static ExtendedDateTime AddMonths(ExtendedDateTime e, int monthsToAdd)
        {
            int month;
            int year = e.Year + Math.DivRem(e.Month + monthsToAdd, 12, out month);

            if (e.Day > DaysInMonth(year, month))
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            if (e.Precision == ExtendedDateTimePrecision.Second)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Precision == ExtendedDateTimePrecision.Minute)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Precision == ExtendedDateTimePrecision.Hour)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Precision == ExtendedDateTimePrecision.Day)
            {
                return new ExtendedDateTime(year, month, e.Day, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Precision == ExtendedDateTimePrecision.Month)
            {
                return new ExtendedDateTime(year, month, e.YearFlags, e.MonthFlags);
            }

            return new ExtendedDateTime(year, e.YearFlags);
        }

        public static int DaysInMonth(int year, int month)                                                                                         // This removes the range restriction present in the DateTime.DaysInMonth() method.
        {
            return month == 2 && IsLeapYear(year) ? 29 : DaysInMonthArray[month];
        }

        public static int DaysInYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static bool IsLeapYear(int year)                                                                                                      // http://www.timeanddate.com/date/leapyear.html
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        public static void ToUniformPrecision(ExtendedDateTime[] extendedDateTimes, ExtendedDateTimePrecision? precision = null)
        {
            var maxPrecision = precision ?? extendedDateTimes.Max(e => e.Precision);

            for (int i = 0; i < extendedDateTimes.Length; i++)
            {
                extendedDateTimes[i].Precision = maxPrecision;
            }
        }

        private const int DaysPerYear = 365;
        private const int DaysPer4Years = DaysPerYear * 4 + 1;       // 1461
        private const int DaysPer100Years = DaysPer4Years * 25 - 1;  // 36524
        private const int DaysPer400Years = DaysPer100Years * 4 + 1; // 146097

        internal static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
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

            var t1 = e - new ExtendedDateTime(1, 1, 1);
  
            int totalDays = (int)(t1.TotalDays + t.TotalDays);

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

            int day = totalDays;
            int hour = (int)((t1.TotalHours + t.TotalHours) % 24);
            int minute = (int)((t1.TotalMinutes + t.TotalMinutes) % 60);
            int second = (int)((t1.TotalSeconds + t.TotalSeconds) % 60);

            if (second != 0 || e.Precision == ExtendedDateTimePrecision.Second)
            {
                return new ExtendedDateTime(year, month, totalDays, hour, minute, second, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (minute != 0 || e.Precision == ExtendedDateTimePrecision.Minute)
            {
                return new ExtendedDateTime(year, month, totalDays, hour, minute, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (hour != 0 || e.Precision == ExtendedDateTimePrecision.Hour)
            {
                return new ExtendedDateTime(year, month, totalDays, hour, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (totalDays != 1 || e.Precision == ExtendedDateTimePrecision.Day)
            {
                return new ExtendedDateTime(year, month, totalDays, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (month != 1 || e.Precision == ExtendedDateTimePrecision.Month)
            {
                return new ExtendedDateTime(year, month, e.YearFlags, e.MonthFlags);
            }

            return new ExtendedDateTime(year, e.YearFlags);
        }

        internal static ExtendedDateTime AddYears(ExtendedDateTime extendedDateTime, int count)                                                     // Called from ExtendedDateTime.
        {
            if (extendedDateTime.Month == 2 && extendedDateTime.Day == 29 && IsLeapYear(extendedDateTime.Year) && !IsLeapYear(extendedDateTime.Year + count))
            {
                extendedDateTime.Month = 3;
                extendedDateTime.Day = 1;
            }

            extendedDateTime.Year += count;

            return extendedDateTime;
        }

        internal static DayOfWeek DayOfWeek(ExtendedDateTime extendedDateTime)                               // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
        {
            var yearFirstHalf = int.Parse(extendedDateTime.Year.ToString().Substring(0, 2));
            var yearSecondHalf = int.Parse(extendedDateTime.Year.ToString().Substring(2, 2));
            var monthKeyTable = new int[] { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
            var monthKey = monthKeyTable[extendedDateTime.Month];
            var centuryKeyTable = new int[] { 6, 4, 2, 0 };
            var centuryKey = centuryKeyTable[yearFirstHalf % 4];

            if (IsLeapYear(extendedDateTime.Year))
            {
                if (extendedDateTime.Month == 1)
                {
                    monthKey = 6;
                }
                else if (extendedDateTime.Month == 2)
                {
                    monthKey = 2;
                }
            }

            return (DayOfWeek)((extendedDateTime.Day + monthKey + yearSecondHalf + Math.Floor(yearSecondHalf / 4d) + centuryKey) % 7);
        }

        internal static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            int second = e.Second - t.Seconds;
            int minute = e.Minute - t.Minutes;
            int hour = e.Hour - t.Hours;
            int day = e.Day;
            int month = e.Month;
            int year = e.Year;
            int daysToSubtract = t.Days;

            while (second < 0)
            {
                second += 60;
                minute--;
            }

            while (minute < 0)
            {
                minute += 60;
                hour--;
            }

            while (hour < 0)
            {
                hour += 24;
                daysToSubtract++;
            }

            while (daysToSubtract > 0)
            {
                if (daysToSubtract >= day)
                {
                    if (month == 1)
                    {
                        year--;
                        month = 12;
                    }
                    else
                    {
                        month--;
                    }

                    daysToSubtract -= day;
                    day = DaysInMonth(year, month);
                }
                else
                {
                    day += daysToSubtract;
                    daysToSubtract = 0;
                }
            }

            if (second != 0 || e.Precision == ExtendedDateTimePrecision.Second)
            {
                return new ExtendedDateTime(year, month, day, hour, minute, second, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (minute != 0 || e.Precision == ExtendedDateTimePrecision.Minute)
            {
                return new ExtendedDateTime(year, month, day, hour, minute, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (hour != 0 || e.Precision == ExtendedDateTimePrecision.Hour)
            {
                return new ExtendedDateTime(year, month, day, hour, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (day != 1 || e.Precision == ExtendedDateTimePrecision.Day)
            {
                return new ExtendedDateTime(year, month, day, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (month != 1 || e.Precision == ExtendedDateTimePrecision.Month)
            {
                return new ExtendedDateTime(year, month, e.YearFlags, e.MonthFlags);
            }

            return new ExtendedDateTime(year, e.YearFlags);
        }

        public static int DaysToMonth(int year, int month)
        {
            return IsLeapYear(year) ? DaysToMonth366[month] : DaysToMonth365[month];
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

        internal static ExtendedDateTime SubtractMonths(ExtendedDateTime e, int count)                                               // Called from ExtendedDateTime.
        {
            int month = e.Month - count % 12;
            int year = e.Year - (int)Math.Floor(count / 12d);

            if (e.Day > DaysInMonth(year, month))
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            if (e.Second != 0 || e.Precision == ExtendedDateTimePrecision.Second)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Minute != 0 || e.Precision == ExtendedDateTimePrecision.Minute)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Hour != 0 || e.Precision == ExtendedDateTimePrecision.Hour)
            {
                return new ExtendedDateTime(year, month, e.Day, e.Hour, e.UtcOffset, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (e.Day != 1 || e.Precision == ExtendedDateTimePrecision.Day)
            {
                return new ExtendedDateTime(year, month, e.Day, e.YearFlags, e.MonthFlags, e.DayFlags);
            }

            if (month != 1 || e.Precision == ExtendedDateTimePrecision.Month)
            {
                return new ExtendedDateTime(year, month, e.YearFlags, e.MonthFlags);
            }

            return new ExtendedDateTime(year, e.YearFlags);
        }

        internal static ExtendedDateTime SubtractYears(ExtendedDateTime extendedDateTime, int count)                                      // Called from ExtendedDateTime.
        {
            if (extendedDateTime.Month == 2 && extendedDateTime.Day == 29 && IsLeapYear(extendedDateTime.Year) && !IsLeapYear(extendedDateTime.Year - count))
            {
                throw new InvalidOperationException("The years subtracted from a leap day must result in another leap day.");
            }

            extendedDateTime.Year -= count;

            return extendedDateTime;
        }

        internal static ExtendedDateTime ToPrecision(ExtendedDateTime originalDate, ExtendedDateTimePrecision precision, bool roundUp)    // Called from ExtendedDateTime.
        {
            var roundedDate = new ExtendedDateTime();

            switch (precision)
            {
                case ExtendedDateTimePrecision.Year:

                    roundedDate = new ExtendedDateTime(originalDate.Year);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        return new ExtendedDateTime(originalDate.Year + 1);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Month:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month + 1;

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, month);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Day:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month, originalDate.Day);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month;
                        var day = originalDate.Day + 1;

                        if (day > DaysInMonth(year, month))
                        {
                            day = 1;
                            month++;
                        }

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, month, day);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Hour:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, originalDate.UtcOffset);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month;
                        var day = originalDate.Day;
                        var hour = originalDate.Hour + 1;

                        if (hour > 23)
                        {
                            hour = 0;
                            day++;
                        }

                        if (day > DaysInMonth(year, month))
                        {
                            day = 1;
                            month++;
                        }

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, month, day, hour, originalDate.UtcOffset);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Minute:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, originalDate.Minute, originalDate.UtcOffset);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month;
                        var day = originalDate.Day;
                        var hour = originalDate.Hour;
                        var minute = originalDate.Minute + 1;

                        if (minute > 59)
                        {
                            minute = 0;
                            hour++;
                        }

                        if (hour > 23)
                        {
                            hour = 0;
                            day++;
                        }

                        if (day > DaysInMonth(year, month))
                        {
                            day = 1;
                            month++;
                        }

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, month, day, hour, minute, originalDate.UtcOffset);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Second:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, originalDate.Minute, originalDate.Second, originalDate.UtcOffset);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month;
                        var day = originalDate.Day;
                        var hour = originalDate.Hour;
                        var minute = originalDate.Minute;
                        var second = originalDate.Second + 1;

                        if (second > 59)
                        {
                            second = 0;
                            minute++;
                        }

                        if (minute > 59)
                        {
                            minute = 0;
                            hour++;
                        }

                        if (hour > 23)
                        {
                            hour = 0;
                            day++;
                        }

                        if (day > DaysInMonth(year, month))
                        {
                            day = 1;
                            month++;
                        }

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, month, day, hour, minute, second, originalDate.UtcOffset);
                    }

                    return roundedDate;

                default:

                    break;
            }

            return roundedDate;
        }
    }
}