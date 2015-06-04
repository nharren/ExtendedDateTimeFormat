namespace System.ISO8601.Internal.Parsers
{
    internal static class OrdinalDateParser
    {
        internal static OrdinalDate Parse(string input, int yearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var isExpanded = false;
            var yearLengthWithOperator = yearLength;

            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                yearLengthWithOperator++;
                isExpanded = true;
            }

            if (input.Length < yearLengthWithOperator + 3)
            {
                throw new ParseException(string.Format("The ordinal date string must be at least {0} characters long.", yearLength + 3), input);
            }

            long year;

            if (!long.TryParse(input.Substring(0, yearLengthWithOperator), out year))
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            int day;
            var hyphenLength = input[yearLengthWithOperator] == '-' ? 1 : 0;

            if (!int.TryParse(input.Substring(yearLengthWithOperator + hyphenLength, 3), out day))
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new OrdinalDate(year, day) { YearLength = yearLength, IsExpanded = isExpanded };
        }
    }
}