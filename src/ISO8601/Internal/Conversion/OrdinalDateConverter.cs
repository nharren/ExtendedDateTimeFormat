namespace System.ISO8601.Internal.Conversion
{
    internal static class OrdinalDateConverter
    {
        private static readonly int[] DaysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

        internal static CalendarDate ToCalendarDate(OrdinalDate ordinalDate, CalendarDatePrecision precision)
        {
            if (precision == CalendarDatePrecision.Century)
            {
                return new CalendarDate(DateTimeCalculator.CenturyOfYear(ordinalDate.Year));
            }

            if (precision == CalendarDatePrecision.Year)
            {
                return new CalendarDate(ordinalDate.Year);
            }

            var days = DateTimeCalculator.IsLeapYear(ordinalDate.Year) ? DaysToMonth366 : DaysToMonth365;
            var month = 0;
            var day = 0;

            for (int i = days.Length - 1; i >= 0; i--)
            {
                if (ordinalDate.Day > days[i])
                {
                    month = i + 1;
                    day = ordinalDate.Day - days[i];

                    break;
                }
            }

            if (precision == CalendarDatePrecision.Month)
            {
                return new CalendarDate(ordinalDate.Year, month);
            }

            return new CalendarDate(ordinalDate.Year, month, day);
        }

        internal static WeekDate ToWeekDate(OrdinalDate ordinalDate, WeekDatePrecision precision)
        {
            return ordinalDate.ToCalendarDate(CalendarDatePrecision.Day).ToWeekDate(precision);
        }
    }
}