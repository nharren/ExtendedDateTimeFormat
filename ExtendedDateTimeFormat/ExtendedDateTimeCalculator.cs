using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeCalculator
    {
        private static readonly int[] DaysInMonthArray = { 29, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static ExtendedDateTime AddMonths(ExtendedDateTime e, int count)                                                                   // Called from ExtendedDateTime.
        {
            int month = e.Month + count % 12;
            int year = e.Year + (int)Math.Floor(count / 12d);

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

        public static int DaysInMonth(int year, int month)                                                                                         // This removes the range restriction present in the DateTime.DaysInMonth() method.
        {
            return month == 2 && IsLeapYear(year) ? DaysInMonthArray[0] : DaysInMonthArray[month];
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

        internal static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)                                                                  // Called from ExtendedDateTime.
        {
            int second = e.Second + t.Seconds;
            int minute = e.Minute + t.Minutes;
            int hour = e.Hour + t.Hours;
            int day = e.Day;
            int month = e.Month;
            int year = e.Year;
            int daysToAdd = t.Days;

            while (second > 59)
            {
                second -= 60;
                minute++;
            }

            while (minute > 59)
            {
                minute -= 60;
                hour++;
            }

            while (hour > 23)
            {
                hour -= 24;
                daysToAdd++;
            }

            while (daysToAdd > 0)
            {
                int daysToNextMonth = DaysInMonth(year, month) - day;

                if (daysToAdd >= daysToNextMonth)
                {
                    if (month == 12)
                    {
                        year++;
                        month = 1;
                    }
                    else
                    {
                        month++;
                    }

                    day = 1;
                    daysToAdd -= daysToNextMonth;
                }
                else
                {
                    day += daysToAdd;
                    daysToAdd = 0;
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

        internal static ExtendedDateTime AddYears(ExtendedDateTime extendedDateTime, int count)                                                     // Called from ExtendedDateTime.
        {
            if (extendedDateTime.Month == 2 && extendedDateTime.Day == 29 && IsLeapYear(extendedDateTime.Year) && !IsLeapYear(extendedDateTime.Year + count))
            {
                throw new InvalidOperationException("The years added to a leap day must result in another leap day.");
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

        internal static ExtendedTimeSpan Subtract(ExtendedDateTime minuend, ExtendedDateTime subtrahend)
        {
            var invert = subtrahend > minuend;

            var e2 = invert ? subtrahend : minuend;
            var e1 = invert ? minuend : subtrahend;

            var years = 0;
            var months = 0;
            var totalMonths = 0d;
            var days = 0;
            var exclusiveDays = 0;
            var offsetDifference = e2.UtcOffset - e1.UtcOffset;
            var hours = e2.Hour - e1.Hour + offsetDifference.Hours;
            var minutes = e2.Minute - e1.Minute + offsetDifference.Minutes;
            var seconds = e2.Second - e1.Second;

            var includeStartDay = e2.Hour >= e1.Hour;                                                                                                  // When there are more or equal hours added to the end day than hours passed in the start day, then we have a full day.
            var includeStartMonth = e1.Day == 1 && includeStartDay;
            var includeStartYear = e1.Month == 1 && includeStartMonth;

            var yearsEqual = e1.Year == e2.Year;
            var yearsMonthsEqual = yearsEqual && e1.Month == e2.Month;

            if (yearsEqual)
            {
                for (int i = e1.Month + (includeStartMonth ? 0 : 1); i < e2.Month; i++)                                                                // Add the difference between two months in the same year.
                {
                    months++;
                    totalMonths++;
                    days += DaysInMonth(e1.Year, i);
                }
            }
            else
            {
                for (int year = e1.Year + (includeStartYear ? 0 : 1); year < e2.Year; year++)                                                          // Add the years between.
                {
                    years++;
                    days += DaysInYear(year);
                }

                if (!includeStartYear)
                {
                    for (int i = e1.Month + (includeStartMonth ? 0 : 1); i <= 12; i++)                                                                 // Add the months remaining in the starting year.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e1.Year, i);
                    }

                    for (int i = e2.Month - 1; i >= 1; i--)                                                                                            // Add the months into the ending year excluding the ending month.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e2.Year, i);
                    }
                }
            }

            if (yearsMonthsEqual)
            {
                totalMonths += (e2.Day - e1.Day) / DaysInMonth(e1.Year, e1.Month);

                for (int i = e1.Day + (includeStartDay ? 0 : 1); i < e2.Day; i++)                                                                      // Add the difference between two days in the same month of the same year.
                {
                    exclusiveDays++;
                    days++;
                }
            }
            else if (!includeStartMonth)
            {
                var daysInMonth = DaysInMonth(e1.Year, e1.Month);

                totalMonths += ((daysInMonth - (e1.Day - 1)) / (double)daysInMonth) + ((e2.Day - 1) / (double)DaysInMonth(e2.Year, e2.Month));         // The -1 is because the start day is excluded (e.g. If the start day is Dec. 2, then only one day has passed in the month, so there are 30 remaining days.)

                for (int i = e1.Day + (includeStartDay ? 0 : 1); i <= daysInMonth; i++)                                                                // Add the days remaining in the starting month.
                {
                    exclusiveDays++;
                    days++;
                }

                for (int i = e2.Day - 1; i >= 1; i--)                                                                                                  // Add the days into the ending month excluding the ending day.
                {
                    exclusiveDays++;
                    days++;
                }
            }

            return invert ? new ExtendedTimeSpan(-years, -totalMonths / 12, -months, -totalMonths, -exclusiveDays, new TimeSpan(-days, -hours, -minutes, -seconds)) : new ExtendedTimeSpan(years, totalMonths / 12, months, totalMonths, exclusiveDays, new TimeSpan(days, hours, minutes, seconds));
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