using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Parsing
{
    internal static class TimePointParser
    {
        internal static TimePoint Parse(string input, int yearLength)
        {
            if (input.StartsWith("T"))
            {
                return TimeParser.Parse(input);
            }

            if (input.Contains("T"))
            {
                return DateTimeParser.Parse(input, yearLength);
            }

            return DateParser.Parse(input, yearLength);
        }
    }
}