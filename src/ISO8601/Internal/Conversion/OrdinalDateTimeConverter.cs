namespace System.ISO8601.Internal.Conversion
{
    internal static class OrdinalDateTimeConverter
    {
        internal static CalendarDateTime ToCalendarDateTime(OrdinalDateTime datetime)
        {
            return new CalendarDateTime(datetime.Date.ToCalendarDate(), datetime.Time);
        }

        internal static WeekDateTime ToWeekDateTime(OrdinalDateTime datetime)
        {
            return new WeekDateTime(datetime.Date.ToWeekDate(), datetime.Time);
        }
    }
}