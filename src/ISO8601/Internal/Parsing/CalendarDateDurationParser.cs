namespace System.ISO8601.Internal.Parsing
{
    internal static class CalendarDateDurationParser
    {
        internal static CalendarDateDuration Parse(string input, int yearLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            if (input.Length < 3)
            {
                throw new ParseException("A date duration string must be at least three characters long.", input);
            }

            int startIndex = 1;
            int endIndex = startIndex + yearLength;

            bool isExpanded = false;

            if (input[1] == '+' || input[1] == '-')
            {
                endIndex++;
                isExpanded = true;
            }

            if (input.Length < endIndex)
            {
                int centuries;

                if (!int.TryParse(input.Substring(startIndex, (endIndex - 2) - startIndex), out centuries))
                {
                    throw new ParseException("The centuries component must be a number.", input);
                }

                var centuriesDuration = CalendarDateDuration.FromCenturies(centuries);
                centuriesDuration.IsExpanded = isExpanded;
                centuriesDuration.YearLength = yearLength;

                return centuriesDuration;
            }

            int years;

            if (!int.TryParse(input.Substring(startIndex, endIndex - startIndex), out years))
            {
                throw new ParseException("The years component must be a number.", input);
            }

            if (input.Length == endIndex)
            {
                return new CalendarDateDuration(years) { YearLength = yearLength, IsExpanded = isExpanded };
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            int months;

            if (!int.TryParse(input.Substring(startIndex, endIndex - startIndex), out months))
            {
                throw new ParseException("The months component must be a number.", input);
            }

            if (input.Length == endIndex)
            {
                return new CalendarDateDuration(years, months) { YearLength = yearLength, IsExpanded = isExpanded };
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            int days;

            if (!int.TryParse(input.Substring(startIndex, endIndex - startIndex), out days))
            {
                throw new ParseException("The days component must be a number.", input);
            }

            return new CalendarDateDuration(years, months, days) { YearLength = yearLength, IsExpanded = isExpanded };
        }
    }
}