namespace System.ExtendedDateTimeFormat.Internal.Converters
{
    public static class CalendarDateConverter
    {
        public static OrdinalDate ToOrdinalDate(CalendarDate calendarDate)
        {
            if (calendarDate.Precision != CalendarDatePrecision.Day)
            {
                throw new ConversionException("The calendar date must be defined to the day to be converted to an ordinal date.");
            }

            return new OrdinalDate(calendarDate.Year, DateCalculator.DayOfYear(calendarDate), calendarDate.AddedYearLength);
        }

        public static WeekDate ToWeekDate(CalendarDate calendarDate, WeekDatePrecision precision)
        {
            if (calendarDate.Precision != CalendarDatePrecision.Day)
            {
                throw new ConversionException("The calendar date must be defined to the day to be converted to an week date.");
            }

            long year = calendarDate.Year;
            int week = DateCalculator.WeekOfYear(calendarDate);
            var isLastWeekOfPreviousYear = week < 1;
            var isFirstWeekOfNextYear = week > DateCalculator.WeeksInYear(year);

            if (isLastWeekOfPreviousYear)
            {
                year--;
                week = DateCalculator.WeeksInYear(year);
            }
            else if (isFirstWeekOfNextYear)
            {
                year++;
                week = 1;
            }

            if (precision == WeekDatePrecision.Week)
            {
                return new WeekDate(year, week, calendarDate.AddedYearLength);
            }

            var day = (int)DateCalculator.DayOfWeek(calendarDate);

            return new WeekDate(year, week, day, calendarDate.AddedYearLength);
        }
    }
}