namespace System.ISO8601.Internal.Conversion
{
    internal static class CalendarDateConverter
    {
        internal static OrdinalDate ToOrdinalDate(CalendarDate calendarDate)
        {
            if (calendarDate.Precision != CalendarDatePrecision.Day)
            {
                throw new ConversionException("The calendar date must be defined to the day to be converted to an ordinal date.");
            }

            return new OrdinalDate(calendarDate.Year, DateTimeCalculator.DayOfYear(calendarDate));
        }

        internal static WeekDate ToWeekDate(CalendarDate calendarDate, WeekDatePrecision precision)
        {
            if (calendarDate.Precision != CalendarDatePrecision.Day)
            {
                throw new ConversionException("The calendar date must be defined to the day to be converted to an week date.");
            }

            long year = calendarDate.Year;
            int week = DateTimeCalculator.WeekOfYear(calendarDate);
            var isLastWeekOfPreviousYear = week < 1;
            var isFirstWeekOfNextYear = week > DateTimeCalculator.WeeksInYear(year);

            if (isLastWeekOfPreviousYear)
            {
                year--;
                week = DateTimeCalculator.WeeksInYear(year);
            }
            else if (isFirstWeekOfNextYear)
            {
                year++;
                week = 1;
            }

            if (precision == WeekDatePrecision.Week)
            {
                return new WeekDate(year, week);
            }

            var day = (int)DateTimeCalculator.DayOfWeek(calendarDate);

            return new WeekDate(year, week, day);
        }
    }
}