namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    public static class WeekDateParser
    {
        internal static WeekDate Parse(string input, int numberOfAddedYearDigits)
        {
            if (numberOfAddedYearDigits > 0)
            {
                numberOfAddedYearDigits++;                                                  // Account for ± prefix in expanded representations.
            }

            var minimumLength = 7 + numberOfAddedYearDigits;

            if (string.IsNullOrEmpty(input) || input.Length < minimumLength)
            {
                throw new ParseException(string.Format("The weekdate string must be at least {0} characters long.", minimumLength), input);
            }

            int year;
            int week;
            int day = 0;

            if (!int.TryParse(input.Substring(0, 4 + numberOfAddedYearDigits), out year))
            {
                throw new ParseException("The year could not be determined from the input string.", input);
            }

            if (input[5 + numberOfAddedYearDigits] == 'W')                                  // Input is in extended format.
            {
                if (!int.TryParse(input.Substring(6 + numberOfAddedYearDigits, 2), out week))
                {
                    throw new ParseException("The week could not be determined from the input string.", input);
                }

                if (input.Length == 10 + numberOfAddedYearDigits)
                {
                    if (!int.TryParse(input.Substring(9 + numberOfAddedYearDigits), out day))
                    {
                        throw new ParseException("The day could not be determined from the input string.", input);
                    }
                }
                else if (input.Length != 8 + numberOfAddedYearDigits)
                {
                    throw new ParseException("The weekdate string is of an unexpected length.", input);
                }
            }
            else                                                                                 // Input is in basic format.
            {
                if (!int.TryParse(input.Substring(5 + numberOfAddedYearDigits, 2), out week))
                {
                    throw new ParseException("The week could not be determined from the input string.", input);
                }

                if (input.Length == 8 + numberOfAddedYearDigits)
                {
                    if (!int.TryParse(input.Substring(7 + numberOfAddedYearDigits), out day))
                    {
                        throw new ParseException("The day could not be determined from the input string.", input);
                    }
                }
                else if (input.Length != 6 + numberOfAddedYearDigits)
                {
                    throw new ParseException("The weekdate string is of an unexpected length.", input);
                }
            }

            return day == 0 ? new WeekDate(year, week) : new WeekDate(year, week, day);
        }
    }
}