namespace System.ISO8601.Internal.Parsing
{
    internal static class StartDurationTimeIntervalParser
    {
        internal static StartDurationTimeInterval Parse(string input, int startYearLength, int endYearLength)
        {
            var parts = input.Split('/');

            return new StartDurationTimeInterval(TimePointParser.Parse(parts[0], startYearLength), DurationParser.Parse(parts[1], endYearLength));
        }
    }
}