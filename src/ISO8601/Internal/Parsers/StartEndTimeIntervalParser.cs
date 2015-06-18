namespace System.ISO8601.Internal.Parsers
{
    internal static class StartEndTimeIntervalParser
    {
        internal static StartEndTimeInterval Parse(string input, int startYearLength, int endYearLength)
        {
            var parts = input.Split('/');

            return new StartEndTimeInterval(TimePointParser.Parse(parts[0], startYearLength), TimePointParser.Parse(parts[1], endYearLength));
        }
    }
}