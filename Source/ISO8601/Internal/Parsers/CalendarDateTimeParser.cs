namespace System.ISO8601.Internal.Parsers
{
    internal static class CalendarDateTimeParser
    {
        internal static CalendarDateTime Parse(string input, int yearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            int dateLength = 8;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                dateLength += yearLength - 3;
            }

            if (input.Substring(1).Contains("-"))
            {
                dateLength += 2;
            }

            return new CalendarDateTime(CalendarDateParser.Parse(input.Substring(0, dateLength), yearLength), Time.Parse(input.Substring(dateLength)));
        }
    }
}