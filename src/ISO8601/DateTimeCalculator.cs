using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public static class DateTimeCalculator
    {
        private static readonly int[] DayOfWeekCenturyKeys = { 6, 4, 2, 0 };
        private static readonly int[] DayOfWeekMonthKeys365 = { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DayOfWeekMonthKeys366 = { 0, 6, 2, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth365 = { 0, 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

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

        public static bool IsLeapYear(long year)                // http://www.timeanddate.com/date/leapyear.html
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        public static int WeeksInYear(long year)
        {
            return WeekOfYear(new CalendarDate(year, 12, 28));
        }

        public static TimePoint Add(TimePoint x, Duration y)
        {
            CalendarDateTime calendarDateTime = null;
            CalendarDate calendarDate = null;
            Time time = null;
            CalendarDateTimeDuration calendarDateTimeDuration = null;
            CalendarDateDuration calendarDateDuration = null;
            TimeDuration timeDuration = null;
            DesignatedDuration designatedDuration = null;

            if (x is CalendarDateTime)
            {
                calendarDateTime = (CalendarDateTime)x;
            }
            else if (x is OrdinalDateTime)
            {
                calendarDateTime = ((OrdinalDateTime)x).ToCalendarDateTime();
            }
            else if (x is WeekDateTime)
            {
                calendarDateTime = ((WeekDateTime)x).ToCalendarDateTime();
            }
            else if (x is CalendarDate)
            {
                calendarDate = (CalendarDate)x;
            }
            else if (x is OrdinalDate)
            {
                calendarDate = ((OrdinalDate)x).ToCalendarDate();
            }
            else if (x is WeekDate)
            {
                calendarDate = ((WeekDate)x).ToCalendarDate();
            }
            else if (x is Time)
            {
                time = (Time)x;
            }

            if (y is CalendarDateTimeDuration)
            {
                calendarDateTimeDuration = (CalendarDateTimeDuration)y;
            }
            else if (y is OrdinalDateTimeDuration)
            {
                calendarDateTimeDuration = ((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration();
            }
            else if (y is CalendarDateDuration)
            {
                calendarDateDuration = (CalendarDateDuration)y;
            }
            else if (y is OrdinalDateDuration)
            {
                calendarDateDuration = ((OrdinalDateDuration)y).ToCalendarDateDuration();
            }
            else if (y is TimeDuration)
            {
                timeDuration = (TimeDuration)y;
            }
            else if (y is DesignatedDuration)
            {
                designatedDuration = (DesignatedDuration)y;
            }

            throw new NotImplementedException();
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

        internal static DayOfWeek DayOfWeek(CalendarDate calendarDate)              // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
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

        internal static TimeSpan Subtract(TimePoint d2, TimePoint d1)
        {
            CalendarDateTime calendarDateTime2 = null;
            CalendarDate calendarDate2 = null;
            Time time2 = null;
            CalendarDateTime calendarDateTime1 = null;
            CalendarDate calendarDate1 = null;
            Time time1 = null;

            if (d2 is CalendarDateTime)
            {
                calendarDateTime2 = (CalendarDateTime)d2;
            }
            else if (d2 is OrdinalDateTime)
            {
                calendarDateTime2 = ((OrdinalDateTime)d2).ToCalendarDateTime();
            }
            else if (d2 is WeekDateTime)
            {
                calendarDateTime2 = ((WeekDateTime)d2).ToCalendarDateTime();
            }
            else if (d2 is CalendarDate)
            {
                calendarDate2 = (CalendarDate)d2;
            }
            else if (d2 is OrdinalDate)
            {
                calendarDate2 = ((OrdinalDate)d2).ToCalendarDate();
            }
            else if (d2 is WeekDate)
            {
                calendarDate2 = ((WeekDate)d2).ToCalendarDate();
            }
            else if (d2 is Time)
            {
                time2 = (Time)d2;
            }

            if (d1 is CalendarDateTime)
            {
                calendarDateTime1 = (CalendarDateTime)d1;
            }
            else if (d1 is OrdinalDateTime)
            {
                calendarDateTime1 = ((OrdinalDateTime)d1).ToCalendarDateTime();
            }
            else if (d1 is WeekDateTime)
            {
                calendarDateTime1 = ((WeekDateTime)d1).ToCalendarDateTime();
            }
            else if (d1 is CalendarDate)
            {
                calendarDate1 = (CalendarDate)d1;
            }
            else if (d1 is OrdinalDate)
            {
                calendarDate1 = ((OrdinalDate)d1).ToCalendarDate();
            }
            else if (d1 is WeekDate)
            {
                calendarDate1 = ((WeekDate)d1).ToCalendarDate();
            }
            else if (d1 is Time)
            {
                time1 = (Time)d1;
            }

            if (calendarDateTime2 != null)
            {
                if (calendarDateTime1 != null)
                {
                    return calendarDateTime2 - calendarDateTime1;
                }

                if (calendarDate2 != null)
                {
                    return calendarDateTime2 - calendarDate1;
                }

                if (time2 != null)
                {
                    return calendarDateTime2 - time1;
                }
            }

            if (calendarDate2 != null && calendarDate1 != null)
            {
                return calendarDate2 - calendarDate1;
            }

            if (time2 != null && time1 != null)
            {
                return time2 - time1;
            }

            throw new InvalidOperationException($"The timepoint of type {d1.GetType()} could not be subtracted from the type {d2.GetType()}");
        }

        internal static int WeekOfYear(CalendarDate calendarDate)               // http://en.wikipedia.org/wiki/ISO_week_date
        {
            return (DayOfYear(calendarDate) - (int)DayOfWeek(calendarDate) + 10) / 7;
        }
    }
}