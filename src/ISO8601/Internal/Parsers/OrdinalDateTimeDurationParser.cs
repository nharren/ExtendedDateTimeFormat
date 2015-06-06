namespace System.ISO8601.Internal.Parsers
{
    internal static class OrdinalDateTimeDurationParser
    {
        internal static OrdinalDateTimeDuration Parse(string input, int fractionLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The ordinal datetime duration must have a time component.", input);
            }

            return new OrdinalDateTimeDuration(OrdinalDateDurationParser.Parse(components[0]), TimeDuration.Parse(components[1], fractionLength));
        }
    }
}