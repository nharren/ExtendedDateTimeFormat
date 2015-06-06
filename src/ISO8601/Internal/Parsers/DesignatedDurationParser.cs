using System.Globalization;
using System.Linq;

namespace System.ISO8601.Internal.Parsers
{
    internal static class DesignatedDurationParser
    {
        private const int DurationDesignatorLength = 1;
        private const int MinimumLength = 3;

        internal static DesignatedDuration Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < MinimumLength)
            {
                throw new ParseException("The designated duration must be at least three characters long.", input);
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            var startIndex = DurationDesignatorLength;
            var endIndex = input.IndexOf('Y');
            var componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int years;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out years))
            {
                throw new ParseException("The years component must be a number.", input);
            }

            if (input.Length < endIndex + 1)
            {
                return new DesignatedDuration(years);
            }

            startIndex = endIndex + 1;
            endIndex = input.IndexOf("M");
            componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(years, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(years, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int months;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out months))
            {
                throw new ParseException("The months component must be a number.", input);
            }

            if (input.Length < endIndex + 1)
            {
                return new DesignatedDuration(years, months);
            }

            startIndex = endIndex + 1;
            endIndex = input.IndexOf("D");
            componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(years, months, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(years, months, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int days;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out days))
            {
                throw new ParseException("The days component must be a number.", input);
            }

            if (input.Length < endIndex + 1)
            {
                return new DesignatedDuration(years, months, days);
            }

            startIndex = endIndex + 2;
            endIndex = input.IndexOf("H");
            componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(years, months, days, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(years, months, days, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int hours;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out hours))
            {
                throw new ParseException("The hours component must be a number.", input);
            }

            if (input.Length < endIndex + 1)
            {
                return new DesignatedDuration(years, months, days, hours);
            }

            startIndex = endIndex + 1;
            endIndex = input.IndexOf("M", startIndex);
            componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(years, months, days, hours, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(years, months, days, hours, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int minutes;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out minutes))
            {
                throw new ParseException("The minutes component must be a number.", input);
            }

            if (input.Length < endIndex + 1)
            {
                return new DesignatedDuration(years, months, days, hours, minutes);
            }

            startIndex = endIndex + 1;
            endIndex = input.IndexOf("S", startIndex);
            componentString = input.Substring(startIndex, endIndex);

            if (componentString.Contains('.'))
            {
                return new DesignatedDuration(years, months, days, hours, minutes, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
            }
            else if (componentString.Contains(','))
            {
                return new DesignatedDuration(years, months, days, hours, minutes, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
            }

            int seconds;

            if (!int.TryParse(input.Substring(startIndex, endIndex), out seconds))
            {
                throw new ParseException("The seconds component must be a number.", input);
            }

            return new DesignatedDuration(years, months, days, hours, minutes, seconds);
        }
    }
}