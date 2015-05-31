namespace System.ISO8601.Internal.Parsers
{
    internal static class StartEndTimeIntervalParser
    {
        internal static StartEndTimeInterval Parse(string input)
        {
            var parts = input.Split('/');

            if (parts.Length != 2)
            {
                throw new ParseException("A start-end time interval must have one solidus ('/')", input);
            }

            return new StartEndTimeInterval(CalendarDateTimeParser.Parse(parts[0]), CalendarDateTimeParser.Parse(parts[1]));
        }
    }
}