using System.Linq;

namespace System.ISO8601.Internal.Parsing
{
    internal class RecurringTimeIntervalParser
    {
        internal static RecurringTimeInterval Parse(string input, int startYearLength, int endYearLength)
        {
            if (input.Length < 4)
            {
                throw new ParseException("A recurring time interval string must be at least four characters long.", input);
            }

            if (input[0] != 'R')
            {
                throw new ParseException("A recurring time interval must begin with the recurring time interval designator (\"R\").", input);
            }

            string timeIntervalString = null;
            string recurrencesString = null;

            if (input.Count(c => c == '/') == 2 || input.ContainsBefore('/', 'P'))
            {
                timeIntervalString = new string(input.Skip(input.IndexOf('/') + 1).ToArray());
                recurrencesString = new string(input.Substring(1).TakeWhile(c => c != '/').ToArray());
            }
            else
            {
                timeIntervalString = new string(input.Skip(input.IndexOf('P')).ToArray());
                recurrencesString = new string(input.Substring(1).TakeWhile(c => c != 'P').ToArray());
            }

            int? recurrences = null;

            if (!string.IsNullOrEmpty(recurrencesString))
            {
                if (recurrencesString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The number of recurrences must be a number.", input);
                }

                recurrences = int.Parse(recurrencesString);
            }

            return new RecurringTimeInterval(TimeIntervalParser.Parse(timeIntervalString, startYearLength, endYearLength), recurrences);
        }
    }
}