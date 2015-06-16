namespace System.ISO8601.Internal.Conversion
{
    internal static class OrdinalDateDurationConverter
    {
        internal static CalendarDateDuration ToCalendarDateDuration(OrdinalDateDuration duration)
        {
            int days;

            return new CalendarDateDuration(duration.Years, Math.DivRem(duration.Days, 30, out days), days);
        }
    }
}