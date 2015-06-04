namespace System.ISO8601.Internal.Parsers
{
    internal static class CalendarDateParser
    {
        internal static CalendarDate Parse(string input, int yearLength)
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

            var centuryLength = yearLengthWithOperator - 2;

            if (input.Length < centuryLength)
            {
                throw new ParseException(string.Format("The calendar date string must be at least {0} characters long.", centuryLength), input);
            }

            if (input.Length == centuryLength)
            {
                int century;

                if (!int.TryParse(input.Substring(0, centuryLength), out century))
                {
                    throw new ParseException("The century could not be parsed from the input string", input);
                }

                var calendarDate = CalendarDate.FromCentury(century);
                calendarDate.YearLength = yearLength;
                calendarDate.IsExpanded = isExpanded;

                return calendarDate;
            }

            long year;

            if (!long.TryParse(input.Substring(0, yearLengthWithOperator), out year))
            {
                throw new ParseException("The year could not be parsed from the input string.", input);
            }

            if (input.Length == yearLengthWithOperator)
            {
                return new CalendarDate(year) { YearLength = yearLength, IsExpanded = isExpanded };
            }

            var hyphenLength = input[yearLengthWithOperator] == '-' ? 1 : 0;
            var yearMonthLength = yearLengthWithOperator + hyphenLength + 2;
            int month;

            if (!int.TryParse(input.Substring(yearLengthWithOperator + hyphenLength, 2), out month))
            {
                throw new ParseException("The month could not be parsed from the input string.", input);
            }

            if (input.Length == yearMonthLength)
            {
                return new CalendarDate(year, month) { YearLength = yearLength, IsExpanded = isExpanded };
            }

            int day;

            if (!int.TryParse(input.Substring(yearMonthLength + hyphenLength, 2), out day))
            {
                throw new ParseException("The day could not be parsed from the input string.", input);
            }

            return new CalendarDate(year, month, day) { YearLength = yearLength, IsExpanded = isExpanded };
        }
    }
}