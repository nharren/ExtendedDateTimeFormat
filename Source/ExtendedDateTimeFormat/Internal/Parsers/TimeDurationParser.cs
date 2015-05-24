using System.Globalization;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class TimeDurationParser
    {
        private const char DurationDesignator = 'P';
        private const char TimeDesignator = 'T';
        private const char SeparatorDesignator = ':';
        private const char CommaDecimalSeparator = ',';
        private const char DotDecimalSeparator = '.';
        private const int DecimalSeperatorLength = 1;
        private const int DurationDesignatorLength = 1;
        private const int HoursLength = 2;
        private const int MinutesLength = 2;
        private const int SecondsLength = 2;
        private const int MinimumLength = 3;

        internal static TimeDuration Parse(string input, int fractionLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < MinimumLength)
            {
                throw new ParseException("The time duration must be at least three characters long.", input);
            }

            if (input[0] != DurationDesignator)
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            int timeDesignatorLength = input[1] == TimeDesignator ? 1 : 0;
            int cumulativeLength = MinimumLength + timeDesignatorLength;
            int hoursIndex = DurationDesignatorLength + timeDesignatorLength;

            if (input.Length >= cumulativeLength + DecimalSeperatorLength + fractionLength && (input[cumulativeLength] == CommaDecimalSeparator || input[cumulativeLength] == DotDecimalSeparator))
            {
                return new TimeDuration(double.Parse(input.Substring(hoursIndex, HoursLength + DecimalSeperatorLength + fractionLength), input[cumulativeLength] == CommaDecimalSeparator ? CultureInfo.GetCultureInfo("fr-FR") : CultureInfo.GetCultureInfo("en-US")));
            }

            int hours;

            if (!int.TryParse(input.Substring(hoursIndex, HoursLength), out hours))
            {
                throw new ParseException("The hours component must be a number.", input);
            }

            int separatorLength = input[cumulativeLength] == SeparatorDesignator ? 1 : 0;

            cumulativeLength += separatorLength + MinutesLength;

            if (input.Length < cumulativeLength)
            {
                return new TimeDuration(hours);
            }

            int minutesIndex = hoursIndex + HoursLength + separatorLength;

            if (input.Length >= cumulativeLength + DecimalSeperatorLength + fractionLength && (input[cumulativeLength] == CommaDecimalSeparator || input[cumulativeLength] == DotDecimalSeparator))
            {
                return new TimeDuration(hours, double.Parse(input.Substring(minutesIndex, MinutesLength + DecimalSeperatorLength + fractionLength), input[cumulativeLength] == CommaDecimalSeparator ? CultureInfo.GetCultureInfo("fr-FR") : CultureInfo.GetCultureInfo("en-US")));
            }

            int minutes;

            if (!int.TryParse(input.Substring(minutesIndex, MinutesLength), out minutes))
            {
                throw new ParseException("The minutes component must be a number.", input);
            }

            cumulativeLength += separatorLength + SecondsLength;

            if (input.Length < cumulativeLength)
            {
                return new TimeDuration(hours, minutes);
            }

            int secondsIndex = minutesIndex + MinutesLength + separatorLength;

            if (input.Length >= cumulativeLength + DecimalSeperatorLength + fractionLength && (input[cumulativeLength] == CommaDecimalSeparator || input[cumulativeLength] == DotDecimalSeparator))
            {
                return new TimeDuration(hours, minutes, double.Parse(input.Substring(secondsIndex, SecondsLength + DecimalSeperatorLength + fractionLength), input[cumulativeLength] == CommaDecimalSeparator ? CultureInfo.GetCultureInfo("fr-FR") : CultureInfo.GetCultureInfo("en-US")));
            }

            int seconds;

            if (!int.TryParse(input.Substring(secondsIndex, SecondsLength), out seconds))
            {
                throw new ParseException("The seconds component must be a number.", input);
            }

            return new TimeDuration(hours, minutes, seconds);
        }
    }
}