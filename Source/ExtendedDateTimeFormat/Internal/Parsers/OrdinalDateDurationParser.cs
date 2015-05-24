namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class OrdinalDateDurationParser
    {
        internal static OrdinalDateDuration Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            if (input.Length < 8)
            {
                throw new ParseException("The input must be at least eight characters long.", input);
            }

            int years;

            if (!int.TryParse(input.Substring(1, 4), out years))
            {
                throw new ParseException("The years component must be a number.", input);
            }

            var seperatorLength = input[5] == '-' ? 1 : 0;

            int days;

            if (!int.TryParse(input.Substring(5 + seperatorLength, 3), out days))
            {
                throw new ParseException("The days component must be a number.", input);
            }

            return new OrdinalDateDuration(years, days);
        }
    }
}