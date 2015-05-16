namespace System.ExtendedDateTimeFormat.Internal.Converters
{
    internal static class WeekDateTimeConverter
    {
        internal static CalendarDateTime ToCalendarDateTime(WeekDateTime datetime)
        {
            return new CalendarDateTime(datetime.Date.ToCalendarDate(), datetime.Time);
        }

        internal static OrdinalDateTime ToOrdinalDateTime(WeekDateTime datetime)
        {
            return new OrdinalDateTime(datetime.Date.ToOrdinalDate(), datetime.Time);
        }
    }
}