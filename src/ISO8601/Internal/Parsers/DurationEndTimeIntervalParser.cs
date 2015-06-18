namespace System.ISO8601.Internal.Parsers
{
    internal static class DurationEndTimeIntervalParser
    {
        internal static DurationEndTimeInterval Parse(string input, int startYearLength, int endYearLength)
        {
            var parts = input.Split('/');

            return new DurationEndTimeInterval(DurationParser.Parse(parts[0], startYearLength), TimePointParser.Parse(parts[1], endYearLength));
        }
    }
}