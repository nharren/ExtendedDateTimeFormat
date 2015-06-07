namespace System.ISO8601.Internal.Parsers
{
    internal static class OrdinalDateDurationParser
    {
        internal static OrdinalDateDuration Parse(string input, int yearLength = 4)
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
                throw new ParseException("The ordinal date duration string must be at least eight characters long.", input);
            }

            int startIndex = 1;
            int endIndex = startIndex + yearLength;

            bool isExpanded = false;

            if (input[1] == '+' || input[1] == '-')
            {
                endIndex++;
                isExpanded = true;
            }

            int years;

            if (!int.TryParse(input.Substring(startIndex, endIndex - startIndex), out years))
            {
                throw new ParseException("The years component must be a number.", input);
            }

            startIndex = endIndex;

            if (input[startIndex] == '-')
            {
                startIndex++;
            }

            endIndex = startIndex + 3;

            int days;

            if (!int.TryParse(input.Substring(startIndex, endIndex - startIndex), out days))
            {
                throw new ParseException("The days component must be a number.", input);
            }

            return new OrdinalDateDuration(years, days) { YearLength = yearLength, IsExpanded = isExpanded };
        }
    }
}