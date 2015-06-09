using System.Linq;

namespace System.ISO8601.Internal.Parsing
{
    internal static class DateTimeParser
    {
        internal static Abstract.DateTime Parse(string input, int yearLength)
        {
            if (input.Contains('W'))
            {
                return WeekDateTimeParser.Parse(input, yearLength);
            }

            var adjustedYearLength = input.StartsWith("+") || input.StartsWith("-") ? yearLength + 1 : yearLength;

            if (input[adjustedYearLength] == '-')
            {
                adjustedYearLength++;
            }

            return input.IndexOf('T') - adjustedYearLength == 3 ? (Abstract.DateTime)OrdinalDateTimeParser.Parse(input, yearLength) : CalendarDateTimeParser.Parse(input, yearLength);
        }
    }
}