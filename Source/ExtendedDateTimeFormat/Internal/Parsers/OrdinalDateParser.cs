namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class OrdinalDateParser
    {
        internal static OrdinalDate Parse(string input, int addedYearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var operatorLength = input.StartsWith("+") || input.StartsWith("-") ? 1 : 0;
            var yearLength = operatorLength + 4 + addedYearLength;
            var dayLength = 3;
            var minimumLength = yearLength + dayLength;

            if (input.Length < minimumLength)
            {
                throw new ParseException(string.Format("The ordinal date string must be at least {0} characters long.", minimumLength), input);
            }

            long year;
            var cannotParseYear = !long.TryParse(input.Substring(0, yearLength), out year);

            if (cannotParseYear)
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            int day;
            var inExtendedFormat = input[yearLength] == '-';
            var hyphenLength = inExtendedFormat ? 1 : 0;

            var cannotParseDay = !int.TryParse(input.Substring(yearLength + hyphenLength, dayLength), out day);

            if (cannotParseDay)
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new OrdinalDate(year, day) { AddedYearLength = addedYearLength };
        }
    }
}