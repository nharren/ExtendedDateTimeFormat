﻿using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeCalculator
    {
        private static readonly int[] DaysInMonthArray = { 29, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

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

            while (daysToAdd > 0)
            {
                var daysToNextMonth = DaysInMonth(year, month) - day;

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
            return month == 2 && IsLeapYear(year) ? DaysInMonthArray[0] : DaysInMonthArray[month];
        }

        public static int DaysInYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static bool IsLeapYear(int year)                                                                                                      // http://www.timeanddate.com/date/leapyear.html
        {
            return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
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

        public static TimeSpan Subtract(ExtendedDateTime minuend, ExtendedDateTime subtrahend)
        {
            return GetSpan(subtrahend, minuend).TimeSpan;
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
            return GetSpan(e1, e2).TotalMonths;
        }

        public static double TotalYears(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return GetSpan(e1, e2).TotalYears;
        }

        public static ExtendedDateTime[] ToUniformPrecision(ExtendedDateTime[] extendedDateTimes, ExtendedDateTimePrecision? precision = null)
        {
            var maxPrecision = precision ?? extendedDateTimes.Max(e => e.Precision);

            var clones = extendedDateTimes.Select(e => (ExtendedDateTime)e.Clone()).ToArray();

            foreach (var extendedDateTime in clones)
            {
                extendedDateTime.Precision = maxPrecision;
            }

            return clones;
        }
        private static ExtendedTimeSpan GetSpan(ExtendedDateTime e1, ExtendedDateTime e2)
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

            var years = 0;
            var months = 0;
            var totalMonths = 0d;
            var days = 0;
            var exclusiveDays = 0;
            var offsetDifference = e4.UtcOffset - e3.UtcOffset;
            var hours = e4.Hour - e3.Hour + offsetDifference.Hours;
            var minutes = e4.Minute - e3.Minute + offsetDifference.Minutes;
            var seconds = e4.Second - e3.Second;

            var includeStartDay = e3.Hour == 0 && e3.Minute == 0 && e3.Second == 0;
            var includeStartMonth = e3.Day == 1 && includeStartDay;
            var includeStartYear = e3.Month == 1 && includeStartMonth;

            var yearsEqual = e3.Year == e4.Year;
            var yearsMonthsEqual = yearsEqual && e3.Month == e4.Month;

            if (yearsEqual)
            {
                for (int i = e3.Month + (includeStartMonth ? 0 : 1); i < e4.Month; i++)                                                                // Add the difference between two months in the same year.
                {
                    months++;
                    totalMonths++;
                    days += DaysInMonth(e3.Year, i);
                }
            }
            else
            {
                for (int year = e3.Year + (includeStartYear ? 0 : 1); year < e4.Year; year++)                                                          // Add the years between.
                {
                    years++;
                    days += DaysInYear(year);
                }

                if (!includeStartYear)
                {
                    for (int i = e3.Month + (includeStartMonth ? 0 : 1); i <= 12; i++)                                                                 // Add the months remaining in the starting year.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e3.Year, i);
                    }

                    for (int i = e4.Month - 1; i >= 1; i--)                                                                                            // Add the months into the ending year excluding the ending month.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e4.Year, i);
                    }
                }
            }

            if (yearsMonthsEqual)
            {
                totalMonths += (e4.Day - e3.Day) / DaysInMonth(e3.Year, e3.Month);

                for (int i = e3.Day + (includeStartDay ? 0 : 1); i < e4.Day; i++)                                                                      // Add the difference between two days in the same month of the same year.
                {
                    exclusiveDays++;
                    days++;
                }
            }
            else if (!includeStartMonth)
            {
                var daysInMonth = DaysInMonth(e3.Year, e3.Month);

                totalMonths += ((daysInMonth - (e3.Day - 1)) / (double)daysInMonth) + ((e4.Day - 1) / (double)DaysInMonth(e4.Year, e4.Month));         // The -1 is because the start day is excluded (e.g. If the start day is Dec. 2, then only one day has passed in the month, so there are 30 remaining days.)

                for (int i = e3.Day + (includeStartDay ? 0 : 1); i <= daysInMonth; i++)                                                                // Add the days remaining in the starting month.
                {
                    exclusiveDays++;
                    days++;
                }

                for (int i = e4.Day - 1; i >= 1; i--)                                                                                                  // Add the days into the ending month excluding the ending day.
                {
                    exclusiveDays++;
                    days++;
                }
            }

            return invert ? new ExtendedTimeSpan(-years, -totalMonths / 12, -months, -totalMonths, -exclusiveDays, new TimeSpan(-days, -hours, -minutes, -seconds)) : new ExtendedTimeSpan(years, totalMonths / 12, months, totalMonths, exclusiveDays, new TimeSpan(days, hours, minutes, seconds));
        }

        private class ExtendedTimeSpan
        {
            private readonly int _exclusiveDays;
            private readonly int _months;
            private readonly TimeSpan _timeSpan;
            private readonly double _totalMonths;
            private readonly double _totalYears;
            private readonly int _years;

            public ExtendedTimeSpan(int years, double totalYears, int months, double totalMonths, int exclusiveDays, TimeSpan timeSpan)
            {
                _years = years;
                _totalYears = totalYears;
                _months = months;
                _totalMonths = totalMonths;
                _exclusiveDays = exclusiveDays;
                _timeSpan = timeSpan;
            }

            public int ExclusiveDays
            {
                get
                {
                    return _exclusiveDays;
                }
            }

            public int Months
            {
                get
                {
                    return _months;
                }
            }

            public TimeSpan TimeSpan
            {
                get
                {
                    return _timeSpan;
                }
            }

            public double TotalMonths
            {
                get
                {
                    return _totalMonths;
                }
            }

            public double TotalYears
            {
                get
                {
                    return _totalYears;
                }
            }

            public int Years
            {
                get
                {
                    return _years;
                }
            }
        }
    }
}