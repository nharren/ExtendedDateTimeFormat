namespace System.ISO8601.Internal.Parsing
{
    internal static class OrdinalDateDurationParser
    {
        internal static OrdinalDateDuration Parse(string input, int yearLength)
        {
            int startIndex = 1;
            int endIndex = startIndex + yearLength;

            if (input[1] == '+' || input[1] == '-')
            {
                endIndex++;
            }

            var years = long.Parse(input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 3;

            return new OrdinalDateDuration(years, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}