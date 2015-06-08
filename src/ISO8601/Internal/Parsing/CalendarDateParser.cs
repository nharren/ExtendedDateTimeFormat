namespace System.ISO8601.Internal.Parsing
{
    internal static class CalendarDateParser
    {
        internal static CalendarDate Parse(string input, int yearLength)
        {
            int startIndex = 0;
            int endIndex = yearLength;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                endIndex++;
            }

            var centuryLength = endIndex - 2;

            if (input.Length == centuryLength)
            {
                return CalendarDate.FromCentury(int.Parse(input.Substring(startIndex, centuryLength)));
            }

            var year = long.Parse(input.Substring(startIndex, endIndex));

            if (input.Length == endIndex)
            {
                return new CalendarDate(year);
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            int month = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new CalendarDate(year, month);
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            return new CalendarDate(year, month, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}