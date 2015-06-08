namespace System.ISO8601.Internal.Parsing
{
    internal static class CalendarDateTimeDurationParser
    {
        internal static CalendarDateTimeDuration Parse(string input, int yearLength)
        {
            var components = input.Split('T');

            return new CalendarDateTimeDuration(CalendarDateDuration.Parse(components[0], yearLength), TimeDuration.Parse("PT" + components[1]));
        }
    }
}