namespace System.ISO8601.Internal.Parsers
{
    internal static class CalendarDateTimeDurationParser
    {
        internal static CalendarDateTimeDuration Parse(string input, int yearLength, int fractionLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The calendar datetime duration must have a time component.", input);
            }

            return new CalendarDateTimeDuration(CalendarDateDurationParser.Parse(components[0], yearLength), TimeDuration.Parse(components[1]));
        }
    }
}