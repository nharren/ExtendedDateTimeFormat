namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class CalendarDateDurationParser
    {
        internal static CalendarDateDuration Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            if (input.Length < 5)
            {
                throw new ParseException("The years component must be four characters long.", input);
            }

            int years;

            if (!int.TryParse(input.Substring(1, 4), out years))
            {
                throw new ParseException("The years component must be a number.", input);
            }

            if (input.Length < 7)
            {
                return new CalendarDateDuration(years);
            }

            var seperatorLength = input[5] == '-' ? 1 : 0;

            int months;

            if (!int.TryParse(input.Substring(5 + seperatorLength, 2), out months))
            {
                throw new ParseException("The months component must be a number.", input);
            }

            if (input.Length < 9)
            {
                return new CalendarDateDuration(years, months);
            }

            int days;

            if (!int.TryParse(input.Substring(7 + seperatorLength * 2, 2), out days))
            {
                throw new ParseException("The days component must be a number.", input);
            }

            return new CalendarDateDuration(years, months, days);
        }
    }
}