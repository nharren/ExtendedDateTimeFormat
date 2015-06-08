namespace System.ISO8601.Internal.Parsing
{
    internal static class OrdinalDateParser
    {
        internal static OrdinalDate Parse(string input, int yearLength)
        {
            int startIndex = 0;
            int endIndex = yearLength;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                endIndex++;
            }

            var year = long.Parse(input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 3;

            return new OrdinalDate(year, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}