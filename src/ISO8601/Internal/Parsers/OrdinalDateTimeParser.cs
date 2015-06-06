namespace System.ISO8601.Internal.Parsers
{
    internal static class OrdinalDateTimeParser
    {
        internal static OrdinalDateTime Parse(string input, int yearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            int dateLength = 7;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                dateLength += yearLength - 3;
            }

            if (input.Substring(1).Contains("-"))
            {
                dateLength++;
            }

            return new OrdinalDateTime(OrdinalDateParser.Parse(input.Substring(0, dateLength), yearLength), Time.Parse(input.Substring(dateLength)));
        }
    }
}