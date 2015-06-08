namespace System.ISO8601.Internal.Conversion
{
    internal static class CalendarDateTimeConverter
    {
        internal static OrdinalDateTime ToOrdinalDateTime(CalendarDateTime datetime)
        {
            return new OrdinalDateTime(datetime.Date.ToOrdinalDate(), datetime.Time);
        }

        internal static WeekDateTime ToWeekDateTime(CalendarDateTime datetime)
        {
            return new WeekDateTime(datetime.Date.ToWeekDate(), datetime.Time);
        }
    }
}