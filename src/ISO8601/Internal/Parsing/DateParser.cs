namespace System.ISO8601.Internal.Parsing
{
    internal static class DateParser
    {
        internal static Abstract.Date Parse(string input, int yearLength)
        {
            if (input.Contains("W"))
            {
                return WeekDateParser.Parse(input, yearLength);
            }

            var adjustedYearLength = input.StartsWith("+") || input.StartsWith("-") ? yearLength + 1 : yearLength;

            if (input[adjustedYearLength] == '-')
            {
                adjustedYearLength++;
            }

            return input.Length - adjustedYearLength == 3 ? (Abstract.Date)OrdinalDateParser.Parse(input, yearLength) : CalendarDateParser.Parse(input, yearLength);
        }
    }
}