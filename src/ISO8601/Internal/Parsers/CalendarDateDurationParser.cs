namespace System.ISO8601.Internal.Parsers
{
    internal static class CalendarDateDurationParser
    {
        internal static CalendarDateDuration Parse(string input, int yearLength)
        {
            int startIndex = 1;
            int endIndex = startIndex + yearLength;

            if (input[1] == '+' || input[1] == '-')
            {
                endIndex++;
            }

            if (input.Length < endIndex)
            {
                return CalendarDateDuration.FromCenturies(int.Parse(input.Substring(startIndex, (endIndex - 2) - startIndex)));
            }

            var years = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new CalendarDateDuration(years);
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            int months = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new CalendarDateDuration(years, months);
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            return new CalendarDateDuration(years, months, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}