using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public static class ISO8601Calculator
    {
        private static readonly int[] DayOfWeekCenturyKeys = { 6, 4, 2, 0 };
        private static readonly int[] DayOfWeekMonthKeys365 = { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DayOfWeekMonthKeys366 = { 0, 6, 2, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth365 = { 0, 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

        public static CalendarDateTime Add(CalendarDateTime x, Duration y)
        {
            if (y is CalendarDateTimeDuration)
            {
                return Add(x, (CalendarDateTimeDuration)y);
            }

            if (y is OrdinalDateTimeDuration)
            {
                return Add(x, ((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration());
            }

            if (y is CalendarDateDuration)
            {
                return Add(x, (CalendarDateDuration)y);
            }

            if (y is OrdinalDateDuration)
            {
                return Add(x, ((OrdinalDateDuration)y).ToCalendarDateDuration());
            }

            if (y is TimeDuration)
            {
                return Add(x, (TimeDuration)y);
            }

            if (y is DesignatedDuration)
            {
                return Add(x, (DesignatedDuration)y);
            }

            throw new InvalidOperationException("The Duration was of an unrecognized type.");
        }

        public static CalendarDate Add(CalendarDate x, Duration y)
        {
            if (y is CalendarDateDuration)
            {
                return Add(x, (CalendarDateDuration)y);
            }

            if (y is OrdinalDateDuration)
            {
                return Add(x, ((OrdinalDateDuration)y).ToCalendarDateDuration());
            }

            if (y is DesignatedDuration)
            {
                return Add(x, (DesignatedDuration)y);
            }

            throw new InvalidOperationException("The Duration was of an unrecognized type.");
        }

        public static TimePoint Add(TimePoint x, Duration y)
        {
            if (x is CalendarDateTime)
            {
                return Add((CalendarDateTime)x, y);
            }

            if (x is OrdinalDateTime)
            {
                return Add(((OrdinalDateTime)x).ToCalendarDateTime(), y);
            }

            if (x is WeekDateTime)
            {
                return Add(((WeekDateTime)x).ToCalendarDateTime(), y);
            }

            if (x is CalendarDate)
            {
                return Add((CalendarDate)x, y);
            }

            if (x is OrdinalDate)
            {
                return Add(((OrdinalDate)x).ToCalendarDate(), y);
            }

            if (x is WeekDate)
            {
                return Add(((WeekDate)x).ToCalendarDate(), y);
            }

            if (x is Time)
            {
                if (y is TimeDuration)
                {
                    return Add((Time)x, (TimeDuration)y);
                }

                if (y is DesignatedDuration)
                {
                    return Add((Time)x, (DesignatedDuration)y);
                }
            }

            throw new InvalidOperationException("The TimePoint was of an unrecognized type.");
        }

        public static CalendarDateTime Add(CalendarDateTime x, CalendarDateTimeDuration y)
        {
            double day = x.Day + y.Days;
            double month = x.Month + y.Months;
            double year = x.Year + y.Years;
            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour + y.Hours;
            var precision = x.Precision;

            if (y.Minutes != null)
            {
                minute += y.Minutes.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                if (precision < TimePrecision.Minute)
                {
                    precision = TimePrecision.Minute;
                }
            }

            if (y.Seconds != null)
            {
                second += y.Seconds.Value;

                if (minute != (int)minute)
                {
                    second += (minute - (int)minute) * 60;
                }

                if (precision < TimePrecision.Second)
                {
                    precision = TimePrecision.Second;
                }
            }

            while (!(second < 60))
            {
                second -= 60;
                minute++;
            }

            while ((int)minute > 59 || (precision == TimePrecision.Minute && !(minute < 60d)))
            {
                minute -= 60;
                hour++;
            }

            while ((int)hour > 23 || (precision == TimePrecision.Hour && hour >= 24d))
            {
                hour -= 24;
                day++;
            }

            while ((int)month > 12)
            {
                month -= 12;
                year++;
            }

            int daysInMonth = DaysInMonth((long)year, (int)month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth((long)year, (int)month);
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time(hour));

                case TimePrecision.Minute:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, minute));

                case TimePrecision.Second:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, (int)minute, second));

                default:
                    return null;
            }
        }

        public static CalendarDateTime Add(CalendarDateTime x, CalendarDateDuration y)
        {
            double day = x.Day;
            double month = x.Month;
            double year = x.Year;
            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour;
            var precision = x.Precision;

            year += y.Years;

            if (y.Months != null)
            {
                month += y.Months.Value;
            }

            if (y.Days != null)
            {
                day += y.Days.Value;
            }

            while ((int)month > 12)
            {
                month -= 12;
                year++;
            }

            int daysInMonth = DaysInMonth((long)year, (int)month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth((long)year, (int)month);
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time(hour));

                case TimePrecision.Minute:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, minute));

                case TimePrecision.Second:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, (int)minute, second));

                default:
                    return null;
            }
        }

        public static CalendarDateTime Add(CalendarDateTime x, TimeDuration y)
        {
            double day = x.Day;
            double month = x.Month;
            double year = x.Year;
            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour;
            var precision = x.Precision;

            hour += y.Hours;

            if (y.Minutes != null)
            {
                minute += y.Minutes.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                if (precision < TimePrecision.Minute)
                {
                    precision = TimePrecision.Minute;
                }
            }

            if (y.Seconds != null)
            {
                second += y.Seconds.Value;

                if (minute != (int)minute)
                {
                    second += (minute - (int)minute) * 60;
                }

                if (precision < TimePrecision.Second)
                {
                    precision = TimePrecision.Second;
                }
            }

            while (!(second < 60))
            {
                second -= 60;
                minute++;
            }

            while ((int)minute > 59 || (precision == TimePrecision.Minute && !(minute < 60d)))
            {
                minute -= 60;
                hour++;
            }

            while ((int)hour > 23 || (precision == TimePrecision.Hour && hour >= 24d))
            {
                hour -= 24;
                day++;
            }

            while ((int)month > 12)
            {
                month -= 12;
                year++;
            }

            int daysInMonth = DaysInMonth((long)year, (int)month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth((long)year, (int)month);
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time(hour));

                case TimePrecision.Minute:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, minute));

                case TimePrecision.Second:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, (int)minute, second));

                default:
                    return null;
            }
        }

        public static CalendarDateTime Add(CalendarDateTime x, DesignatedDuration y)
        {
            double day = x.Day;
            double month = x.Month;
            double year = x.Year;
            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour;
            var precision = x.Precision;

            if (y.Years != null)
            {
                year += y.Years.Value;
            }

            if (y.Months != null)
            {
                month += y.Months.Value;

                if (year != (int)year)
                {
                    month += (year - (int)year) * 60;
                }
            }

            if (y.Days != null)
            {
                day += y.Days.Value;

                if (month != (int)month)
                {
                    day += (month - (int)month) * 60;
                }
            }

            if (y.Hours != null)
            {
                hour += y.Hours.Value;

                if (day != (int)day)
                {
                    hour += (day - (int)day) * 24;
                }
            }

            if (y.Minutes != null)
            {
                minute += y.Minutes.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                if (precision < TimePrecision.Minute)
                {
                    precision = TimePrecision.Minute;
                }
            }

            if (y.Seconds != null)
            {
                second += y.Seconds.Value;

                if (minute != (int)minute)
                {
                    second += (minute - (int)minute) * 60;
                }

                if (precision < TimePrecision.Second)
                {
                    precision = TimePrecision.Second;
                }
            }

            while (!(second < 60))
            {
                second -= 60;
                minute++;
            }

            while ((int)minute > 59 || (precision == TimePrecision.Minute && !(minute < 60d)))
            {
                minute -= 60;
                hour++;
            }

            while ((int)hour > 23 || (precision == TimePrecision.Hour && hour >= 24d))
            {
                hour -= 24;
                day++;
            }

            while ((int)month > 12)
            {
                month -= 12;
                year++;
            }

            int daysInMonth = DaysInMonth((long)year, (int)month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth((long)year, (int)month);
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time(hour));

                case TimePrecision.Minute:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, minute));

                case TimePrecision.Second:
                    return new CalendarDateTime(new CalendarDate((long)year, (int)month, (int)day), new Time((int)hour, (int)minute, second));

                default:
                    return null;
            }
        }

        public static CalendarDate Add(CalendarDate x, CalendarDateDuration y)
        {
            var day = x.Day;
            var month = x.Month;
            var year = x.Year;
            var century = x.Century;
            var precision = x.Precision;

            if (y.Centuries != null)
            {
                century += y.Centuries.Value;
            }

            year += y.Years;

            if (y.Months != null)
            {
                month += y.Months.Value;

                if (precision < CalendarDatePrecision.Month)
                {
                    precision = CalendarDatePrecision.Month;
                }
            }

            if (y.Days != null)
            {
                day += y.Days.Value;

                if (precision < CalendarDatePrecision.Day)
                {
                    precision = CalendarDatePrecision.Day;
                }
            }

            while (month > 12)
            {
                month -= 12;
                year++;
            }

            var daysInMonth = DaysInMonth(year, month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth(year, month);
            }

            switch (precision)
            {
                case CalendarDatePrecision.Century:
                    return CalendarDate.FromCentury(century);

                case CalendarDatePrecision.Year:
                    return new CalendarDate(year);

                case CalendarDatePrecision.Month:
                    return new CalendarDate(year, month);

                case CalendarDatePrecision.Day:
                    return new CalendarDate(year, month, day);

                default:
                    return null;
            }
        }

        public static CalendarDate Add(CalendarDate x, DesignatedDuration y)
        {
            if (y.Hours != null)
            {
                throw new InvalidOperationException("The Hours component of a DesignatedDuration cannot be added to a CalendarDate.");
            }

            if (y.Minutes != null)
            {
                throw new InvalidOperationException("The Minutes component of a DesignatedDuration cannot be added to a CalendarDate.");
            }

            if (y.Seconds != null)
            {
                throw new InvalidOperationException("The Seconds component of a DesignatedDuration cannot be added to a CalendarDate.");
            }

            double day = x.Day;
            double month = x.Month;
            double year = x.Year;
            int century = x.Century;
            var precision = x.Precision;

            if (y.Years != null)
            {
                var yYearsString = ((int)y.Years.Value).ToString();
                century += int.Parse(yYearsString.Substring(0, yYearsString.Length - 2));

                year += y.Years.Value;
            }

            if (y.Months != null)
            {
                month += (int)y.Months.Value;

                if (year != (int)year)
                {
                    month += (year - (int)year) * 60;
                }

                if (precision < CalendarDatePrecision.Month)
                {
                    precision = CalendarDatePrecision.Month;
                }
            }
            else if (precision == CalendarDatePrecision.Year && year != (int)year)
            {
                throw new InvalidOperationException("The Year value of a CalendarDate cannot be fractional.");
            }

            if (y.Days != null)
            {
                day += y.Days.Value;

                if (month != (int)month)
                {
                    day += (month - (int)month) * 60;
                }

                if (y.Days != (int)y.Days)
                {
                    throw new InvalidOperationException("The Day value of a CalendarDate cannot be fractional.");
                }

                if (precision < CalendarDatePrecision.Day)
                {
                    precision = CalendarDatePrecision.Day;
                }
            }
            else if (precision == CalendarDatePrecision.Month && month != (int)month)
            {
                throw new InvalidOperationException("The Month value of a CalendarDate cannot be fractional.");
            }

            while ((int)month > 12)
            {
                month -= 12;
                year++;
            }

            int daysInMonth = DaysInMonth((long)year, (int)month);

            while (day > daysInMonth)
            {
                day -= daysInMonth;
                month++;

                daysInMonth = DaysInMonth((long)year, (int)month);
            }

            switch (precision)
            {
                case CalendarDatePrecision.Century:
                    return CalendarDate.FromCentury(century);

                case CalendarDatePrecision.Year:
                    return new CalendarDate((int)year);

                case CalendarDatePrecision.Month:
                    return new CalendarDate((long)year, (int)month);

                case CalendarDatePrecision.Day:
                    return new CalendarDate((long)year, (int)month, (int)day);

                default:
                    return null;
            }
        }

        public static Time Add(Time x, TimeDuration y)
        {
            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour;
            var precision = x.Precision;

            hour += y.Hours;

            if (y.Minutes != null)
            {
                minute += y.Minutes.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                while (minute > 59)
                {
                    minute -= 60;
                    hour++;
                }

                if (precision < TimePrecision.Minute)
                {
                    precision = TimePrecision.Minute;
                }
            }

            if (y.Seconds != null)
            {
                second += y.Seconds.Value;

                if (minute != (int)minute)
                {
                    second += (minute - (int)minute) * 60;
                }

                while (second > 59)
                {
                    second -= 60;
                    minute++;
                }

                if (precision < TimePrecision.Second)
                {
                    precision = TimePrecision.Second;
                }
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    {
                        return new Time(hour);
                    }
                case TimePrecision.Minute:
                    {
                        return new Time((int)hour, minute);
                    }
                default:
                    {
                        return new Time((int)hour, (int)minute, second);
                    }
            }
        }

        public static TimePoint Add(Time x, DesignatedDuration y)
        {
            if (y.Years != null)
            {
                throw new InvalidOperationException("The Years component of the DesignatedDuration cannot be added to a Time.");
            }

            if (y.Months != null)
            {
                throw new InvalidOperationException("The Months component of the DesignatedDuration cannot be added to a Time.");
            }

            if (y.Days != null)
            {
                throw new InvalidOperationException("The Days component of the DesignatedDuration cannot be added to a Time.");
            }

            var second = x.Second;
            var minute = x.Minute;
            var hour = x.Hour;
            var precision = x.Precision;

            if (y.Seconds != null)
            {
                second += y.Seconds.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                if (minute != (int)minute)
                {
                    second += (minute - (int)minute) * 60;
                }

                while (second > 59)
                {
                    second -= 60;
                    minute++;
                }

                if (precision < TimePrecision.Second)
                {
                    precision = TimePrecision.Second;
                }
            }

            if (y.Minutes != null)
            {
                precision = TimePrecision.Minute;
                minute += y.Minutes.Value;

                if (hour != (int)hour)
                {
                    minute += (hour - (int)hour) * 60;
                }

                while (minute > 59)
                {
                    minute -= 60;
                    hour++;
                }

                if (precision < TimePrecision.Minute)
                {
                    precision = TimePrecision.Minute;
                }
            }

            if (y.Hours != null)
            {
                hour += y.Hours.Value;
            }

            switch (precision)
            {
                case TimePrecision.Hour:
                    {
                        return new Time(hour);
                    }
                case TimePrecision.Minute:
                    {
                        return new Time((int)hour, minute);
                    }
                default:
                    {
                        return new Time((int)hour, (int)minute, second);
                    }
            }
        }

        public static int DayOfYear(CalendarDate calendarDate)
        {
            return (IsLeapYear(calendarDate.Year) ? DaysToMonth366[calendarDate.Month] : DaysToMonth365[calendarDate.Month]) + calendarDate.Day;
        }

        public static int DaysInMonth(long year, int month)
        {
            return month == 2 && IsLeapYear(year) ? 29 : DaysInMonthArray[month];
        }

        public static int DaysInYear(long year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static bool IsLeapYear(long year)                                         // http://www.timeanddate.com/date/leapyear.html
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        public static TimePoint Subtract(TimePoint x, Duration y)
        {
            throw new NotImplementedException();
        }

        public static int WeeksInYear(long year)
        {
            return WeekOfYear(new CalendarDate(year, 12, 28));
        }

        internal static int CenturyOfYear(long year)
        {
            var yearString = year.ToString();
            int century;

            if (!int.TryParse(yearString.Substring(0, yearString.Length - 2), out century))
            {
                throw new InvalidOperationException("The century could not be parsed from the year.");
            }

            return century;
        }

        internal static DayOfWeek DayOfWeek(CalendarDate calendarDate)                                                                  // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
        {
            var yearOfCentury = int.Parse(calendarDate.Year.ToString().Substring(calendarDate.Century.ToString().Length));

            return (DayOfWeek)((
                  calendarDate.Day
                + (IsLeapYear(calendarDate.Year) ? DayOfWeekMonthKeys366[calendarDate.Month] : DayOfWeekMonthKeys365[calendarDate.Month])
                + yearOfCentury
                + yearOfCentury / 4
                + DayOfWeekCenturyKeys[calendarDate.Century % 4 < 0 ? 4 + (calendarDate.Century % 4) : calendarDate.Century % 4])
            % 7);
        }

        internal static int DayOfYear(WeekDate weekDate)
        {
            return (weekDate.Week - 1) * 7
                + (weekDate.Day - 1)
                + (int)DayOfWeek(new CalendarDate(weekDate.Year, 1, 1));
        }

        internal static TimeSpan Subtract(CalendarDate d2, CalendarDate d1)
        {
            var years = d2.Year - d1.Year;

            return TimeSpan.FromDays(
                  years * 365
                + years / 4
                - years / 100
                + years / 400
                + (IsLeapYear(d1.Year) && DaysToMonth366[d1.Month] + d1.Day <= 60 ? 1 : 0)
                + (IsLeapYear(d2.Year) && DaysToMonth366[d2.Month] + d2.Day > 60 ? 1 : 0)
                + DaysToMonth365[d2.Month]
                - DaysToMonth365[d1.Month]
                + d2.Day
                - d1.Day
            );
        }

        internal static TimeSpan Subtract(CalendarDateTime d2, CalendarDateTime d1)
        {
            var years = d2.Year - d1.Year;
            var offsetDifference = d2.Time.UtcOffset - d1.Time.UtcOffset;

            return TimeSpan.FromDays(
                  years * 365
                + years / 4
                - years / 100
                + years / 400
                + (IsLeapYear(d1.Year) && DaysToMonth366[d1.Month] + d1.Day <= 60 ? 1 : 0)
                + (IsLeapYear(d2.Year) && DaysToMonth366[d2.Month] + d2.Day > 60 ? 1 : 0)
                + DaysToMonth365[d2.Month]
                - DaysToMonth365[d1.Month]
                + d2.Day
                - d1.Day
            )
                .Add(TimeSpan.FromHours(d2.Hour - d1.Hour + offsetDifference.Hours))
                .Add(TimeSpan.FromMinutes(d2.Minute - d1.Minute + offsetDifference.Minutes))
                .Add(TimeSpan.FromSeconds(d2.Second - d1.Second));
        }

        internal static TimeSpan Subtract(CalendarDateTime d2, CalendarDate d1)
        {
            var years = d2.Year - d1.Year;

            return TimeSpan.FromDays(
                  years * 365
                + years / 4
                - years / 100
                + years / 400
                + (IsLeapYear(d1.Year) && DaysToMonth366[d1.Month] + d1.Day <= 60 ? 1 : 0)
                + (IsLeapYear(d2.Year) && DaysToMonth366[d2.Month] + d2.Day > 60 ? 1 : 0)
                + DaysToMonth365[d2.Month]
                - DaysToMonth365[d1.Month]
                + d2.Day
                - d1.Day
            )
                .Add(TimeSpan.FromHours(d2.Hour))
                .Add(TimeSpan.FromMinutes(d2.Minute))
                .Add(TimeSpan.FromSeconds(d2.Second));
        }

        internal static TimeSpan Subtract(CalendarDateTime d2, Time d1)
        {
            var offsetDifference = d2.Time.UtcOffset - d1.UtcOffset;

            return TimeSpan.FromHours(d2.Hour - d1.Hour + offsetDifference.Hours)
                .Add(TimeSpan.FromMinutes(d2.Minute - d1.Minute + offsetDifference.Minutes))
                .Add(TimeSpan.FromSeconds(d2.Second - d1.Second));
        }

        internal static TimeSpan Subtract(UtcOffset u2, UtcOffset u1)
        {
            return new TimeSpan(u2.Hours, u2.Minutes, 0) - new TimeSpan(u1.Hours, u1.Minutes, 0);
        }

        internal static TimeSpan Subtract(Time t2, Time t1)
        {
            var offsetDifference = t2.UtcOffset - t1.UtcOffset;

            return TimeSpan.FromHours(t2.Hour - t1.Hour + offsetDifference.Hours)
                .Add(TimeSpan.FromMinutes(t2.Minute - t1.Minute + offsetDifference.Minutes))
                .Add(TimeSpan.FromSeconds(t2.Second - t1.Second));
        }

        internal static TimeSpan Subtract(TimePoint x, TimePoint y)
        {
            if (x is CalendarDateTime)
            {
                return Subtract((CalendarDateTime)x, y);
            }
            else if (x is OrdinalDateTime)
            {
                return Subtract(((OrdinalDateTime)x).ToCalendarDateTime(), y);
            }
            else if (x is WeekDateTime)
            {
                return Subtract(((WeekDateTime)x).ToCalendarDateTime(), y);
            }
            else if (x is CalendarDate)
            {
                return Subtract((CalendarDate)x, y);
            }
            else if (x is OrdinalDate)
            {
                return Subtract(((OrdinalDate)x).ToCalendarDate(), y);
            }
            else if (x is WeekDate)
            {
                return Subtract(((WeekDate)x).ToCalendarDate(), y);
            }
            else if (x is Time)
            {
                return Subtract((Time)x, y);
            }

            throw new InvalidOperationException($"A {y.GetType()} cannot be subtracted from a {x.GetType()}");
        }

        internal static TimeSpan Subtract(CalendarDate x, TimePoint y)
        {
            if (y is CalendarDate)
            {
                return x - (CalendarDate)y;
            }

            if (y is OrdinalDate)
            {
                return x - ((OrdinalDate)y).ToCalendarDate();
            }

            if (y is WeekDate)
            {
                return x - ((WeekDate)y).ToCalendarDate();
            }

            throw new InvalidOperationException($"A {y.GetType()} cannot be subtracted from a {x.GetType()}");
        }

        internal static TimeSpan Subtract(CalendarDateTime x, TimePoint y)
        {
            if (y is CalendarDateTime)
            {
                return x - (CalendarDateTime)y;
            }

            if (y is OrdinalDateTime)
            {
                return x - ((OrdinalDateTime)y).ToCalendarDateTime();
            }

            if (y is WeekDateTime)
            {
                return x - ((WeekDateTime)y).ToCalendarDateTime();
            }

            if (y is CalendarDate)
            {
                return x - (CalendarDate)y;
            }

            if (y is OrdinalDate)
            {
                return x - ((OrdinalDate)y).ToCalendarDate();
            }

            if (y is WeekDate)
            {
                return x - ((WeekDate)y).ToCalendarDate();
            }

            if (y is Time)
            {
                return x - (Time)y;
            }

            throw new InvalidOperationException($"A {y.GetType()} cannot be subtracted from a {x.GetType()}");
        }

        internal static TimeSpan Subtract(Time x, TimePoint y)
        {
            if (y is Time)
            {
                return x - (Time)y;
            }

            throw new InvalidOperationException($"A {y.GetType()} cannot be subtracted from a {x.GetType()}");
        }

        internal static int WeekOfYear(CalendarDate calendarDate)                                                                       // http://en.wikipedia.org/wiki/ISO_week_date
        {
            return (DayOfYear(calendarDate) - (int)DayOfWeek(calendarDate) + 10) / 7;
        }
    }
}