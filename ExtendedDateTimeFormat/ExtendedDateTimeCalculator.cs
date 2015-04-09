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

            int? second = e.Second;
            int? minute = e.Minute;
            int? hour = e.Hour;
            int? day = e.Day;
            int? month = e.Month;
            int year = e.Year;

            if (t.Seconds != 0)
            {
                if (e.Second == null)
                {
                    second = 0;
                }

                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                second += t.Seconds;
            }

            while (second > 59)
            {
                second -= 60;

                minute++;
            }

            if (t.Minutes != 0)
            {
                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                minute += t.Minutes;
            }

            while (minute > 59)
            {
                minute -= 60;

                hour++;
            }

            if (t.Hours != 0)
            {
                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                hour += t.Hours;
            }

            var daysToAdd = 0;

            while (hour > 23)
            {
                hour -= 24;

                daysToAdd++;
            }

            if (t.Days != 0)
            {
                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                daysToAdd += t.Days;
            }

            for (int i = 0; i < daysToAdd; i++)
            {
                if (DaysInMonth(year, month.Value) < day + 1)
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
                }
                else
                {
                    day++;
                }
            }

            if (month == null)
            {
                return new ExtendedDateTime(year, yearFlags: e.YearFlags);
            }

            if (day == null)
            {
                return new ExtendedDateTime(year, (byte)month, yearFlags: e.YearFlags, monthFlags: e.MonthFlags);
            }

            if (hour == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (minute == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (second == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, (byte)second, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
        }

        public static ExtendedDateTime AddMonths(ExtendedDateTime extendedDateTime, int count)
        {
            if (extendedDateTime == null)
            {
                throw new ArgumentNullException("extendedDateTime");
            }

            var result = (ExtendedDateTime)extendedDateTime.Clone();

            if (result.Month == null)
            {
                result.Month = 1;
            }

            for (int i = 0; i < count; i++)
            {
                if (result.Month == 12)
                {
                    result.Year++;
                    result.Month = 1;
                }
                else
                {
                    result.Month++;
                }
            }

            if (DaysInMonth(result.Year, result.Month.Value) > result.Day)
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            return result;
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

        public static int DaysInMonth(int year, int month)                                 // This removes the range restriction present in the DateTime.DaysInMonth() method.
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

        public static bool IsLeapYear(int year)                                            // http://www.timeanddate.com/date/leapyear.html
        {
            return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
        }

        public static ExtendedDateTime[] NormalizePrecision(ExtendedDateTime[] extendedDateTimes, ExtendedDateTimePrecision? precision = null)
        {
            var maxPrecision = precision ?? extendedDateTimes.Max(e => e.Precision);

            var clones = extendedDateTimes.Select(e => (ExtendedDateTime)e.Clone()).ToArray();

            foreach (var extendedDateTime in clones)
            {
                while (extendedDateTime.Precision < maxPrecision)
                {
                    switch (extendedDateTime.Precision + 1)
                    {
                        case ExtendedDateTimePrecision.Year:
                            break;
                        case ExtendedDateTimePrecision.Month:
                            extendedDateTime.Month = 1;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Month;
                            break;
                        case ExtendedDateTimePrecision.Day:
                            extendedDateTime.Day = 1;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Day;
                            break;
                        case ExtendedDateTimePrecision.Hour:
                            extendedDateTime.Hour = 0;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Hour;
                            extendedDateTime.UtcOffset = TimeZoneInfo.Local.BaseUtcOffset;
                            break;
                        case ExtendedDateTimePrecision.Minute:
                            extendedDateTime.Minute = 0;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Minute;
                            break;
                        case ExtendedDateTimePrecision.Second:
                            extendedDateTime.Second = 0;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Second;
                            break;
                    }
                }
            }

            return clones;
        }

        public static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            int? second = e.Second;
            int? minute = e.Minute;
            int? hour = e.Hour;
            int? day = e.Day;
            int? month = e.Month;
            int year = e.Year;
            var utcOffset = e.UtcOffset;

            if (t.Seconds != 0)
            {
                if (e.Second == null)
                {
                    second = 0;
                }

                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                second -= t.Seconds;
            }

            while (second < 0)
            {
                second += 60;

                minute--;
            }

            if (t.Minutes != 0)
            {
                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                minute -= t.Minutes;
            }

            while (minute < 0)
            {
                minute += 60;

                hour--;
            }

            if (t.Hours != 0)
            {
                if (hour == null)
                {
                    hour = 0;

                    utcOffset = TimeZoneInfo.Local.BaseUtcOffset;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                hour -= t.Hours;
            }

            var daysToSubtract = 0;

            while (hour < 0)
            {
                hour += 24;

                daysToSubtract++;
            }

            if (t.Days != 0)
            {
                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                daysToSubtract -= t.Days;
            }

            for (int i = 0; i < daysToSubtract; i++)
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

                    day = DaysInMonth(year, month.Value);
                }
                else
                {
                    day--;
                }
            }

            if (month == null)
            {
                return new ExtendedDateTime(year, yearFlags: e.YearFlags);
            }

            if (day == null)
            {
                return new ExtendedDateTime(year, (byte)month, yearFlags: e.YearFlags, monthFlags: e.MonthFlags);
            }

            if (hour == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (minute == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (second == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, (byte)second, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
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

            var normalizedDates = NormalizePrecision(new ExtendedDateTime[] { e2, e1 }, ExtendedDateTimePrecision.Second);

            var e4 = invert ? normalizedDates[1] : normalizedDates[0];
            var e3 = invert ? normalizedDates[0] : normalizedDates[1];

            var days = 0;

            for (int year = e3.Year; year < e4.Year; year++)                             // Add the years between the dates, including the starting year and excluding the ending year.
            {
                days += DaysInYear(year);
            }

            for (int i = e3.Month.Value - 1; i >= 1; i--)                                // Subtract the months before the starting month.
            {
                days -= DaysInMonth(e3.Year, i);
            }

            for (int i = e3.Day.Value; i >= 1; i--)                                      // Subtract the starting day and the days before.
            {
                days--;
            }

            var hours = 24 - (e3.Hour.Value + 1);                                        // Add the hours remaining in the starting day excluding the starting hour.
            var minutes = 60 - (e3.Minute.Value + 1);                                    // Add the minutes remaining in the starting hour excluding the starting minute.
            var seconds = 60 - e3.Second.Value;                                          // Add the seconds remaining in the starting minute.

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

            for (int i = e4.Month.Value - 1; i >= 1; i--)                             // Add the months in the ending year that are before the ending month.
            {
                days += DaysInMonth(e4.Year, i);
            }

            for (int i = e4.Day.Value - 1; i >= 1; i--)                               // Add the days into the ending month.
            {
                days++;
            }

            for (int i = e4.Hour.Value - 1; i >= 0; i--)                              // Add the hours into the ending day.
            {
                hours++;
            }

            for (int i = e4.Minute.Value - 1; i >= 0; i--)                            // Add the minutes into the ending hour.
            {
                minutes++;
            }

            for (int i = e4.Second.Value - 1; i >= 0; i--)                            // Add the seconds into the ending minute.
            {
                seconds++;
            }

            return invert ? new TimeSpan(-days, -hours, -minutes, -seconds) : new TimeSpan(days, hours, minutes, seconds);
        }

        public static ExtendedDateTime SubtractMonths(ExtendedDateTime extendedDateTime, int count)
        {
            if (extendedDateTime == null)
            {
                throw new ArgumentNullException("extendedDateTime");
            }

            var result = (ExtendedDateTime)extendedDateTime.Clone();

            if (result.Month == null)
            {
                result.Month = 1;
            }

            for (int i = 0; i < count; i++)
            {
                if (result.Month == 1)
                {
                    result.Year--;
                    result.Month = 12;
                }
                else
                {
                    result.Month--;
                }
            }

            if (DaysInMonth(result.Year, result.Month.Value) > result.Day)
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            return result;
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

        public static ExtendedDateTime ToPrecision(ExtendedDateTime extendedDateTime, ExtendedDateTimePrecision precision, bool roundUp)
        {
            var originalDate = NormalizePrecision(new ExtendedDateTime[] { extendedDateTime }, precision)[0];         // Ensure the original date is at the specified precision.
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

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month.Value);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month.Value + 1;

                        if (month > 12)
                        {
                            month = 1;
                            year++;
                        }

                        return new ExtendedDateTime(year, (byte)month);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Day:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month.Value, originalDate.Day.Value);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month.Value;
                        var day = originalDate.Day.Value + 1;

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

                        return new ExtendedDateTime(year, (byte)month, (byte)day);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Hour:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month.Value, originalDate.Day.Value, originalDate.Hour.Value, originalDate.UtcOffset.Value);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month.Value;
                        var day = originalDate.Day.Value;
                        var hour = originalDate.Hour.Value + 1;

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

                        return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, originalDate.UtcOffset.Value);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Minute:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month.Value, originalDate.Day.Value, originalDate.Hour.Value, originalDate.Minute.Value, originalDate.UtcOffset.Value);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month.Value;
                        var day = originalDate.Day.Value;
                        var hour = originalDate.Hour.Value;
                        var minute = originalDate.Minute.Value + 1;

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

                        return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, originalDate.UtcOffset.Value);
                    }

                    return roundedDate;

                case ExtendedDateTimePrecision.Second:

                    roundedDate = new ExtendedDateTime(originalDate.Year, originalDate.Month.Value, originalDate.Day.Value, originalDate.Hour.Value, originalDate.Minute.Value, originalDate.Second.Value, originalDate.UtcOffset.Value);

                    if (roundUp && ExtendedDateTime.Comparer.Compare(originalDate, roundedDate) != 0)
                    {
                        var year = originalDate.Year;
                        var month = originalDate.Month.Value;
                        var day = originalDate.Day.Value;
                        var hour = originalDate.Hour.Value;
                        var minute = originalDate.Minute.Value;
                        var second = originalDate.Second.Value + 1;

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

                        return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, (byte)second, originalDate.UtcOffset.Value);
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

            for (int i = e3.Month.Value + 1; i <= 12; i++)                               // Add the months remaining in the starting year, excluding the starting month.
            {
                days -= DaysInMonth(e3.Year, i);
                months++;
            }

            for (int i = e4.Month.Value - 1; i >= 1; i--)                                // Add the months in the ending year that are before the ending month.
            {
                days -= DaysInMonth(e4.Year, i);
                months++;
            }

            return invert ? new double[] { -years, -months, -days, -difference.Hours, -difference.Minutes, -difference.Seconds, DaysInMonth(e4.Year, e4.Month.Value) } : new double[] { years, months, days, difference.Hours, difference.Minutes, difference.Seconds, DaysInMonth(e4.Year, e4.Month.Value) };
        }
    }
}