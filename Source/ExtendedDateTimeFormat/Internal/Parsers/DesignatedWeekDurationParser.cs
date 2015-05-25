using System.ExtendedDateTimeFormat.Internal.Durations;
using System.Globalization;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class DesignatedWeekDurationParser
    {
        private const int DurationDesignatorLength = 1;
        private const int MinimumLength = 3;

        internal static DesignatedWeekDuration Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < MinimumLength)
            {
                throw new ParseException("The designated week duration must be at least three characters long.", input);
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            var startIndex = DurationDesignatorLength;
            var endIndex = input.IndexOf('W');
            var componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedWeekDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedWeekDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int weeks;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out weeks))
            {
                throw new ParseException("The weeks component must be a number.", input);
            }

            return new DesignatedWeekDuration(weeks);
        }
    }
}