namespace System.ISO8601.Internal.Parsing
{
    internal static class WeekDateTimeParser
    {
        internal static WeekDateTime Parse(string input, int yearLength)
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

            return new WeekDateTime(WeekDateParser.Parse(input.Substring(0, dateLength), yearLength), Time.Parse(input.Substring(dateLength)));
        }
    }
}
