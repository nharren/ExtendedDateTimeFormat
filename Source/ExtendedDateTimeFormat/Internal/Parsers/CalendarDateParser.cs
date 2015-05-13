namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class CalendarDateParser
    {
        internal static CalendarDate Parse(string input, int addedYearLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var operatorLength = input.StartsWith("+") || input.StartsWith("-") ? 1 : 0;
            var centuryLength = operatorLength + 2 + addedYearLength;
            var minimumLength = centuryLength;

            if (string.IsNullOrEmpty(input) || input.Length < minimumLength)
            {
                throw new ParseException(string.Format("The calendar date string must be at least {0} characters long.", minimumLength), input);
            }

            if (input.Length == centuryLength)
            {
                int century;
                var cannotParseCentury = !int.TryParse(input.Substring(0, centuryLength), out century);

                if (cannotParseCentury)
                {
                    throw new ParseException("The century could not be parsed from the input string", input);
                }

                return new CalendarDate(century, addedYearLength);
            }

            var yearLength = centuryLength + 2;
            long year;
            var cannotParseYear = !long.TryParse(input.Substring(0, yearLength), out year);

            if (cannotParseYear)
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            var noMonth = input.Length == yearLength;

            if (noMonth)
            {
                return new CalendarDate(year, addedYearLength);
            }

            var inExtendedFormat = input[yearLength] == '-';
            var hyphenLength = inExtendedFormat ? 1 : 0;
            var monthLength = 2;
            int month;
            var cannotParseMonth = !int.TryParse(input.Substring(yearLength + hyphenLength + monthLength, 2), out month);

            if (cannotParseMonth)
            {
                throw new ParseException("The month could not be parsed from the input string.", input);
            }

            var noDay = input.Length == yearLength + hyphenLength + monthLength;

            if (noDay)
            {
                return new CalendarDate(year, month, addedYearLength);
            }

            int day;
            var cannotParseDay = !int.TryParse(input.Substring(input.Length - 1, 2), out day);

            if (cannotParseDay)
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new CalendarDate(year, month, day, addedYearLength);
        }
    }
}