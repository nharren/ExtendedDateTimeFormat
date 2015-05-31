using System.Linq;

namespace System.ISO8601.Internal.Parsers
{
    internal static class DateTimeParser
    {
        private const int OperatorLength = 1;
        private const int BaseYearLength = 4;

        internal static ISO8601.Abstract.DateTime Parse(string input, int addedYearLength)
        {
            if (input.Contains('W'))
            {
                return WeekDateTimeParser.Parse(input);
            }

            var yearLength = input[0] == '+' || input[0] == '-' ? BaseYearLength + OperatorLength + addedYearLength : BaseYearLength;
            var nonYearStart = input[yearLength] == '-' ? yearLength + 1 : yearLength;

            return input.IndexOf('T') - nonYearStart == 3 ? (ISO8601.Abstract.DateTime)OrdinalDateTimeParser.Parse(input) : CalendarDateTimeParser.Parse(input);
        }
    }
}