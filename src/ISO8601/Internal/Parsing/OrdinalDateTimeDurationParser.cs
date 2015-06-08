namespace System.ISO8601.Internal.Parsing
{
    internal static class OrdinalDateTimeDurationParser
    {
        internal static OrdinalDateTimeDuration Parse(string input, int yearLength)
        {
            var components = input.Split('T');

            return new OrdinalDateTimeDuration(OrdinalDateDuration.Parse(components[0], yearLength), TimeDuration.Parse("PT" + components[1]));
        }
    }
}