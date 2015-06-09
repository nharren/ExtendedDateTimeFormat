namespace System.ISO8601.Internal.Parsing
{
    internal static class DurationContextTimeIntervalParser
    {
        internal static DurationContextTimeInterval Parse(string input, int yearLength)
        {
            var parts = input.Split('/');

            return new DurationContextTimeInterval(DurationParser.Parse(input, yearLength));
        }
    }
}