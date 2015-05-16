namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class OrdinalDateTimeParser
    {
        internal static OrdinalDateTime Parse(string input)
        {
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The datetime string is invalid.", input);
            }

            return new OrdinalDateTime(OrdinalDateParser.Parse(components[0], 0), Time.Parse(components[1]));
        }
    }
}