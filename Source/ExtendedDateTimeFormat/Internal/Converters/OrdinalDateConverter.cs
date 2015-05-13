namespace System.ExtendedDateTimeFormat.Internal.Converters
{
    internal static class OrdinalDateConverter
    {
        private static readonly int[] DaysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };

        internal static CalendarDate ToCalendarDate(OrdinalDate ordinalDate, CalendarDatePrecision precision)
        {
            if (precision == CalendarDatePrecision.Century)
            {
                return new CalendarDate(DateCalculator.CenturyOfYear(ordinalDate.Year), ordinalDate.AddedYearLength);
            }

            if (precision == CalendarDatePrecision.Year)
            {
                return new CalendarDate(ordinalDate.Year, ordinalDate.AddedYearLength);
            }

            var isLeapYear = DateCalculator.IsLeapYear(ordinalDate.Year);
            var month = 0;
            var day = 0;

            if (isLeapYear)
            {
                for (int i = DaysToMonth366.Length - 1; i >= 0; i--)
                {
                    if (ordinalDate.Day > DaysToMonth366[i])
                    {
                        month = i + 1;
                        day = ordinalDate.Day - DaysToMonth366[i];

                        break;
                    }
                }
            }
            else
            {
                for (int i = DaysToMonth365.Length - 1; i >= 0; i--)
                {
                    if (ordinalDate.Day > DaysToMonth365[i])
                    {
                        month = i + 1;
                        day = ordinalDate.Day - DaysToMonth365[i];

                        break;
                    }
                }
            }

            if (precision == CalendarDatePrecision.Month)
            {
                return new CalendarDate(ordinalDate.Year, month, ordinalDate.AddedYearLength);
            }

            return new CalendarDate(ordinalDate.Year, month, day, ordinalDate.AddedYearLength);
        }

        internal static WeekDate ToWeekDate(OrdinalDate ordinalDate, WeekDatePrecision precision)
        {
            return ordinalDate.ToCalendarDate(CalendarDatePrecision.Day).ToWeekDate(precision);
        }
    }
}