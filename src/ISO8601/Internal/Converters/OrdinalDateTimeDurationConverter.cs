namespace System.ISO8601.Internal.Converters
{
    internal static class OrdinalDateTimeDurationConverter
    {
        internal static CalendarDateTimeDuration ToCalendarDateTimeDuration(OrdinalDateTimeDuration duration)
        {
            return new CalendarDateTimeDuration(duration.DateDuration.ToCalendarDateDuration(), duration.TimeDuration);
        }
    }
}