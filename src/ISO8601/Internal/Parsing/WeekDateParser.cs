namespace System.ISO8601.Internal.Parsing
{
    internal static class WeekDateParser
    {
        internal static WeekDate Parse(string input, int yearLength)
        {
            int startIndex = 0;
            var endIndex = yearLength;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                endIndex++;
            }

            var year = long.Parse(input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex + 1;

            if (input[endIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            var week = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new WeekDate(year, week);
            }

            startIndex = endIndex;

            if (input[endIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 1;

            return new WeekDate(year, week, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}