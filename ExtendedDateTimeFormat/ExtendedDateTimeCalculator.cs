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
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 == 0)
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }            
                                                                
            return false;
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

        public static ExtendedDateTime[] NormalizePrecision(ExtendedDateTime[] extendedDateTimes, ExtendedDateTimePrecision? precision = null)
        {
            var maxPrecision = precision ?? extendedDateTimes.Max(e => e.Precision);

            var clones = extendedDateTimes.Select(e => (ExtendedDateTime)e.Clone()).ToArray();

            foreach (var extendedDateTime in clones)
            {
                while (extendedDateTime.Precision != maxPrecision)
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

        public static TimeSpan Subtract(ExtendedDateTime e2, ExtendedDateTime e1)          // Use http://www.calculator.net/date-calculator.html to verify. (Beware of calculators which use the julian calender, as this will produce different results.)
        {
            var invert = e1 > e2;

            var normalizedDates = NormalizePrecision(new ExtendedDateTime[] { e2, e1 }, ExtendedDateTimePrecision.Second);

            var e4 = invert ? normalizedDates[1] : normalizedDates[0];
            var e3 = invert ? normalizedDates[0] : normalizedDates[1];

            var days = 0;
            var hours = 0;
            var minutes = 0;
            var seconds = 0;

            for (int year = e3.Year; year < e4.Year; year++)                          // Add the years between the dates, including the starting year and excluding the ending year.
            {
                days += DaysInYear(year);
            }

            for (int i = e3.Month.Value - 1; i >= 1; i--)                             // Subtract the months in the starting year that are before the starting month.
            {
                days -= DaysInMonth(e3.Year, i);
            }

            for (int i = e3.Day.Value - 1; i >= 1; i--)                               // Subtract the days in the starting month that are before the starting day.
            {
                days--;
            }

            for (int i = e3.Hour.Value - 1; i >= 0; i--)                              // Subtract the hours in the starting day that are before the starting hour.
            {
                hours--;
            }

            for (int i = e3.Minute.Value - 1; i >= 0; i--)                            // Subtract the minutes in the starting hour that are before the starting minute.
            {
                minutes--;
            }

            for (int i = e3.Second.Value - 1; i >= 0; i--)                            // Subtract the seconds in the starting minute that are before the starting second.
            {
                seconds--;
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

            var yearsBetween = e2.Year - e1.Year;               // We already accounted for the remainder of the start year, so we subtract one from the difference so that this year isnt counted.

            var monthsRemainingInStartYear = 0;

            if (e1.Month != null)
            {
                yearsBetween--;

                monthsRemainingInStartYear = 12 - e1.Month.Value;
            }

            var daysRemainingInStartMonth = 0;

            if (e1.Day != null)
            {
                monthsRemainingInStartYear--;                            // When we "finish" the days in the month, we are effectively removing a month.

                daysRemainingInStartMonth = DaysInMonth(e1.Year, e1.Month.Value) - e1.Day.Value;
            }

            var hoursRemainingInStartDay = 0;

            if (e1.Hour != null)
            {
                daysRemainingInStartMonth--;

                hoursRemainingInStartDay = 24 - e1.Hour.Value;
            }

            var minutesRemainingInStartHour = 0;

            if (e1.Minute != null)
            {
                hoursRemainingInStartDay--;

                minutesRemainingInStartHour = 60 - e1.Minute.Value;
            }

            var secondsRemainingInStartMinute = 0;

            if (e1.Second != null)
            {
                minutesRemainingInStartHour--;

                secondsRemainingInStartMinute = 60 - e1.Second.Value;
            }

            var secondsIntoEndMinute = 0;

            if (e2.Second != null)
            {
                secondsIntoEndMinute = e2.Second.Value;
            }

            var minutesIntoEndHour = 0;

            if (e2.Minute != null)
            {
                minutesIntoEndHour = e2.Minute.Value;
            }

            var hoursIntoEndDay = 0;

            if (e2.Hour != null)
            {
                hoursIntoEndDay = e2.Hour.Value;
            }

            var daysIntoEndMonth = 0;

            if (e2.Day != null)
            {
                daysIntoEndMonth = e2.Day.Value;
            }

            var monthsIntoEndYear = 0;

            if (e2.Month != null)
            {
                monthsIntoEndYear = e2.Month.Value;
            }

            double seconds = secondsRemainingInStartMinute + secondsIntoEndMinute;
            double minutes = minutesRemainingInStartHour + minutesIntoEndHour;
            double hours = hoursRemainingInStartDay + hoursIntoEndDay;
            double days = daysRemainingInStartMonth + daysIntoEndMonth;
            double months = monthsRemainingInStartYear + monthsIntoEndYear;
            double years = yearsBetween;

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

            var monthIncrement = e2.Month.Value;
            var yearIncrement = e2.Year;

            var daysInMonth = DaysInMonth(yearIncrement, monthIncrement);

            while (days > daysInMonth)
            {
                days -= daysInMonth;

                if (monthIncrement + 1 > 12)
                {
                    months -= 12;
                    years++;
                    yearIncrement++;
                    monthIncrement = 1;
                }
                else
                {
                    months++;
                    monthIncrement++;
                }

                daysInMonth = DaysInMonth(yearIncrement, monthIncrement);
            }

            return new double[] { years, months, days, hours, minutes, seconds, daysInMonth };
        }
    }
}