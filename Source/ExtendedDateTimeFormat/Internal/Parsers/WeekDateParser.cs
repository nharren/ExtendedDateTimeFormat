namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class WeekDateParser
    {
        internal static WeekDate Parse(string input, int extraYearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var operatorLength = input.StartsWith("+") || input.StartsWith("-") ? 1 : 0;
            var yearLength = operatorLength + 4 + extraYearLength;
            var weekDesignatorLength = 1;
            var weekLength = weekDesignatorLength + 2;
            var minimumLength = yearLength + weekLength;

            if (string.IsNullOrEmpty(input) || input.Length < minimumLength)
            {
                throw new ParseException(string.Format("The week date string must be at least {0} characters long.", minimumLength), input);
            }

            long year;
            var cannotParseYear = !long.TryParse(input.Substring(0, yearLength), out year);

            if (cannotParseYear)
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            var inExtendedFormat = input[yearLength] == '-';
            var hyphenLength = inExtendedFormat ? 1 : 0;
            int week;
            var cannotParseWeek = !int.TryParse(input.Substring(yearLength + hyphenLength + weekDesignatorLength, 2), out week);

            if (cannotParseWeek)
            {
                throw new ParseException("The week could not be parsed from the input string.", input);
            }

            var dayLength = 1;
            var hasDay = input.Length == yearLength + hyphenLength + weekLength + hyphenLength + dayLength;

            if (!hasDay)
            {
                return new WeekDate(year, week);
            }

            int day;
            var cannotParseDay = !int.TryParse(input.Substring(input.Length, 1), out day);

            if (cannotParseDay)
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new WeekDate(year, week, day);
        }
    }
}