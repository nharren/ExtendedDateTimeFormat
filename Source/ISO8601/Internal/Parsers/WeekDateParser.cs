namespace System.ISO8601.Internal.Parsers
{
    internal static class WeekDateParser
    {
        internal static WeekDate Parse(string input, int yearLength)
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
                throw new ParseException(string.Format("The week date string must be at least {0} characters long.", yearLength + 3), input);
            }

            long year;

            if (!long.TryParse(input.Substring(0, yearLengthWithOperator), out year))
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            var inExtendedFormat = input[yearLengthWithOperator] == '-';
            var hyphenLength = inExtendedFormat ? 1 : 0;
            int week;

            if (!int.TryParse(input.Substring(yearLengthWithOperator + hyphenLength + 1, 2), out week))
            {
                throw new ParseException("The week could not be parsed from the input string.", input);
            }

            if (input.Length == yearLengthWithOperator + hyphenLength + 3)
            {
                return new WeekDate(year, week) { YearLength = yearLength, IsExpanded = isExpanded };
            }

            int day;

            if (!int.TryParse(input.Substring(input.Length - 1, 1), out day))
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new WeekDate(year, week, day) { YearLength = yearLength, IsExpanded = isExpanded };
        }
    }
}