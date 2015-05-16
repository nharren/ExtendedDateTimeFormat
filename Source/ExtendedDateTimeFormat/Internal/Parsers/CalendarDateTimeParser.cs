namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class CalendarDateTimeParser
    {
        internal static CalendarDateTime Parse(string input)
        {
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The datetime string is invalid.", input);
            }

            return new CalendarDateTime(CalendarDateParser.Parse(components[0], 0), Time.Parse(components[1]));
        }
    }
}