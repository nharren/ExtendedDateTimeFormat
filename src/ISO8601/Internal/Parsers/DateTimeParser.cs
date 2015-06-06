using System.Linq;

namespace System.ISO8601.Internal.Parsers
{
    internal static class DateTimeParser
    {
        private const int OperatorLength = 1;
        private const int BaseYearLength = 4;

        internal static Abstract.DateTime Parse(string input, int yearLength)
        {
            if (input.Contains('W'))
            {
                return WeekDateTimeParser.Parse(input);
            }

            var adjustedYearLength = input.StartsWith("+") || input.StartsWith("-") ? yearLength + 1 : yearLength;

            if (input[adjustedYearLength] == '-')
            {
                adjustedYearLength++;
            }

            return input.IndexOf('T') - adjustedYearLength == 3 ? (Abstract.DateTime)OrdinalDateTimeParser.Parse(input) : CalendarDateTimeParser.Parse(input, yearLength);
        }
    }
}