namespace System.ExtendedDateTimeFormat
{
    public static class DateCalculator
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
            var yearString = calendarDate.Year.ToString();
            var yearOfCentury = int.Parse(yearString.Substring(2, 2));

            return (DayOfWeek)((
                  calendarDate.Day
                + (IsLeapYear(calendarDate.Year) ? DayOfWeekMonthKeys366[calendarDate.Month] : DayOfWeekMonthKeys365[calendarDate.Month])
                + yearOfCentury
                + yearOfCentury / 4
                + DayOfWeekCenturyKeys[int.Parse(yearString.Substring(0, 2)) % 4])
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

        internal static int WeekOfYear(CalendarDate calendarDate)               // http://en.wikipedia.org/wiki/ISO_week_date
        {
            return (DayOfYear(calendarDate) - (int)DayOfWeek(calendarDate) + 10) / 7;
        }
    }
}