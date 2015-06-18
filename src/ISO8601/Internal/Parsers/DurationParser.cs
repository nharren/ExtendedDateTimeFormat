using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Parsers
{
    internal static class DurationParser
    {
        internal static Duration Parse(string input, int yearLength)
        {
            if (input.ContainsAny('Y', 'M', 'D', 'H', 'M', 'S', 'W'))
            {
                return DesignatedDurationParser.Parse(input);
            }

            var adjustedYearLength = input.StartsWith("P+") || input.StartsWith("P-") ? yearLength + 2 : yearLength + 1;

            if (input[adjustedYearLength] == '-')
            {
                adjustedYearLength++;
            }

            return input.IndexOf('T') - adjustedYearLength == 3 ? (Abstract.Duration)OrdinalDateTimeDurationParser.Parse(input, yearLength) : CalendarDateTimeDurationParser.Parse(input, yearLength);
        }
    }
}