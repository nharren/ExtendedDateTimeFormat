using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeCalculator
    {
        public static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var second = e.Second;
            var minute = e.Minute;
            var hour = e.Hour;
            var day = e.Day;
            var month = e.Month;
            var year = e.Year;

            second += t.Seconds;

            while (second > 59)
            {
                second -= 60;

                minute++;
            }

            minute += t.Minutes;

            while (minute > 59)
            {
                minute -= 60;

                hour++;
            }

            hour += t.Hours;

            var daysToAdd = t.Days;

            while (hour > 23)
            {
                hour -= 24;

                daysToAdd++;
            }

            var daysInMonth = DaysInMonth(year, month);

            for (int i = 0; i < daysToAdd; i++)
            {
                if (daysInMonth < day + 1)
                {
                    if (month == 12)
                    {
                        year++;
                        month = 1;
                        daysInMonth = DaysInMonth(year, month);
                    }
                    else
                    {
                        month++;
                        daysInMonth = DaysInMonth(year, month);
                    }

                    day = 1;
                }
                else
                {
                    day++;
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

        public static ExtendedDateTime AddMonths(ExtendedDateTime e, int count)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var month = e.Month;
            var year = e.Year;

            for (int i = 0; i < count; i++)
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
            }

            if (DaysInMonth(year, month) > e.Day)
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

        public static ExtendedDateTime AddYears(ExtendedDateTime extendedDateTime, int count)
        {
            if (extendedDateTime.Month == 2 && extendedDateTime.Day == 29 && IsLeapYear(extendedDateTime.Year) && !IsLeapYear(extendedDateTime.Year + count))
            {
                throw new InvalidOperationException("The years added to a leap day must result in another leap day.");
            }

            var result = (ExtendedDateTime)extendedDateTime.Clone();

            result.Year += count;

            return result;
        }

        public static int DaysInMonth(int year, int month)                                                                                         // This removes the range restriction present in the DateTime.DaysInMonth() method.
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            else if (month == 2)
            {
                if (IsLeapYear(year))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }

            throw new ArgumentOutOfRangeException("month", "A month must be between 1 and 12.");
        }

        public static int DaysInYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static bool IsLeapYear(int year)                                                                                                      // http://www.timeanddate.com/date/leapyear.html
        {
            return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
        }

        public static ExtendedDateTime[] NormalizePrecision(ExtendedDateTime[] extendedDateTimes, ExtendedDateTimePrecision? precision = null)       // Normalizes precisions to the highest common precision.
        {
            var maxPrecision = precision ?? extendedDateTimes.Max(e => e.Precision);

            var clones = extendedDateTimes.Select(e => (ExtendedDateTime)e.Clone()).ToArray();

            foreach (var extendedDateTime in clones)
            {
                extendedDateTime.Precision = maxPrecision;
            }

            return clones;
        }

        public static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var second = e.Second;
            var minute = e.Minute;
            var hour = e.Hour;
            var day = e.Day;
            var month = e.Month;
            var year = e.Year;

            second -= t.Seconds;

            while (second < 0)
            {
                second += 60;

                minute--;
            }

            minute += t.Minutes;

            while (minute < 0)
            {
                minute += 60;

                hour--;
            }

            hour -= t.Hours;

            var daysToSubtract = t.Days;

            while (hour < 0)
            {
                hour += 24;

                daysToSubtract++;
            }

            for (int i = daysToSubtract; i >= 0 ; i--)
            {
                if (day - 1 < 1)
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

                    day = DaysInMonth(year, month);
                }
                else
                {
                    day--;
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

        public static TimeSpan Subtract(ExtendedDateTime e2, ExtendedDateTime e1)          // Use http://www.calculator.net/date-calculator.html to verify. (Beware of calculators which use the julian calender, as this will produce different results.)
        {
            if (e1 == null)
            {
                throw new ArgumentNullException("e1");
            }

            if (e2 == null)
            {
                throw new ArgumentNullException("e2");
            }

            var invert = e1 > e2;

            var e4 = invert ? e1 : e2;
            var e3 = invert ? e2 : e1;

            var days = 0;

            for (int year = e3.Year; year < e4.Year; year++)                             // Add the years between the dates, including the starting year and excluding the ending year.
            {
                days += DaysInYear(year);
            }

            for (int i = e3.Month - 1; i >= 1; i--)                                // Subtract the months before the starting month.
            {
                days -= DaysInMonth(e3.Year, i);
            }

            for (int i = e3.Day; i >= 1; i--)                                      // Subtract the starting day and the days before.
            {
                days--;
            }

            var hours = 23 - e3.Hour;                                              // Add the hours remaining in the starting day excluding the starting hour.
            var minutes = 59 - e3.Minute;                                          // Add the minutes remaining in the starting hour excluding the starting minute.
            var seconds = 60 - e3.Second;                                          // Add the seconds remaining in the starting minute.

            if (seconds > 59)
            {
                seconds -= 60;
                minutes++;
            }

            if (minutes > 59)
            {
                minutes -= 60;
                hours++;
            }

            if (hours > 23)
            {
                hours -= 24;
                days++;
            }

            for (int i = e4.Month - 1; i >= 1; i--)                             // Add the months in the ending year that are before the ending month.
            {
                days += DaysInMonth(e4.Year, i);
            }

            for (int i = e4.Day - 1; i >= 1; i--)                               // Add the days into the ending month.
            {
                days++;
            }

            for (int i = e4.Hour - 1; i >= 0; i--)                              // Add the hours into the ending day.
            {
                hours++;
            }

            for (int i = e4.Minute - 1; i >= 0; i--)                            // Add the minutes into the ending hour.
            {
                minutes++;
            }

            for (int i = e4.Second - 1; i >= 0; i--)                            // Add the seconds into the ending minute.
            {
                seconds++;
            }

            return invert ? new TimeSpan(-days, -hours, -minutes, -seconds) : new TimeSpan(days, hours, minutes, seconds);
        }

        public static ExtendedDateTime SubtractMonths(ExtendedDateTime e, int count)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var month = e.Month;
            var year = e.Year;

            for (int i = 0; i < count; i++)
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
            }

            if (DaysInMonth(year, month) > e.Day)
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

        public static ExtendedDateTime SubtractYears(ExtendedDateTime extendedDateTime, int count)
        {
            if (extendedDateTime.Month == 2 && extendedDateTime.Day == 29 && IsLeapYear(extendedDateTime.Year) && !IsLeapYear(extendedDateTime.Year - count))
            {
                throw new InvalidOperationException("The years subtracted from a leap day must result in another leap day.");
            }

            var result = (ExtendedDateTime)extendedDateTime.Clone();

            result.Year -= count;

            return result;
        }

        public static ExtendedDateTime ToPrecision(ExtendedDateTime originalDate, ExtendedDateTimePrecision precision, bool roundUp)
        {
            var roundedDate = (ExtendedDateTime)null;

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
        public static double TotalMonths(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            if (e1 == null)
            {
                throw new ArgumentNullException("e1");
            }

            if (e2 == null)
            {
                throw new ArgumentNullException("e2");
            }

            var span = BaseSubtract(e1, e2);

            return span[0] * 12 + span[1] + (span[2] + (span[3] + (span[4] + span[5] / 60) / 60) / 24) / span[6];
        }

        public static double TotalYears(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            if (e1 == null)
            {
                throw new ArgumentNullException("e1");
            }

            if (e2 == null)
            {
                throw new ArgumentNullException("e2");
            }

            var span = BaseSubtract(e1, e2);

            return span[0] + (span[1] + (span[2] + (span[3] + (span[4] + span[5] / 60) / 60) / 24) / span[6]) / 12;
        }

        private static double[] BaseSubtract(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            if (e1 == null)
            {
                throw new ArgumentNullException("e1");
            }

            if (e2 == null)
            {
                throw new ArgumentNullException("e2");
            }

            var invert = e2 > e1;

            var e3 = invert ? e1 : e2;
            var e4 = invert ? e2 : e1;

            var difference = e4 - e3;

            var years = 0;
            var months = 0;
            var days = difference.Days;

            for (int year = e3.Year + 1; year < e4.Year; year++)                         // Add the years between the dates, excluding the starting year and ending year.
            {
                days -= DaysInYear(year);
                years++;
            }

            for (int i = e3.Month + 1; i <= 12; i++)                               // Add the months remaining in the starting year, excluding the starting month.
            {
                days -= DaysInMonth(e3.Year, i);
                months++;
            }

            for (int i = e4.Month - 1; i >= 1; i--)                                // Add the months in the ending year that are before the ending month.
            {
                days -= DaysInMonth(e4.Year, i);
                months++;
            }

            return invert ? new double[] { -years, -months, -days, -difference.Hours, -difference.Minutes, -difference.Seconds, DaysInMonth(e4.Year, e4.Month) } : new double[] { years, months, days, difference.Hours, difference.Minutes, difference.Seconds, DaysInMonth(e4.Year, e4.Month) };
        }
    }
}