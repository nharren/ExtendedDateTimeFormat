namespace System.ISO8601.Internal.Parsers
{
    internal static class WeekDateTimeParser
    {
        internal static WeekDateTime Parse(string input)
        {
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The datetime string is invalid.", input);
            }

            return new WeekDateTime(WeekDateParser.Parse(components[0], 0), Time.Parse(components[1]));
        }
    }
}
