using System.Globalization;
using System.Linq;

namespace System.ISO8601.Internal.Parsing
{
    internal static class DesignatedDurationParser
    {
        internal static DesignatedDuration Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < 3)
            {
                throw new ParseException("The designated duration must be at least three characters long.", input);
            }

            if (input[0] != 'P')
            {
                throw new ParseException("A duration designator was expected.", input);
            }

            int startIndex;
            int endIndex = 0;
            string componentString = null;
            int temp;
            int? years = null;
            int? months = null;
            int? days = null;
            int? hours = null;
            int? minutes = null;

            if (input.Contains('W'))
            {
                startIndex = endIndex + 1;
                endIndex = input.IndexOf('W');
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return DesignatedDuration.FromWeeks(double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return DesignatedDuration.FromWeeks(double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The weeks component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return DesignatedDuration.FromWeeks(temp);
                }

                years = temp;
            }

            if (input.Contains('Y'))
            {
                startIndex = endIndex + 1;
                endIndex = input.IndexOf('Y');
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The years component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return new DesignatedDuration(temp);
                }

                years = temp;
            }

            if (input.ContainsBefore('M', 'T'))
            {
                startIndex = endIndex + 1;
                endIndex = input.IndexOf('M');
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(years, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(years, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The months component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return new DesignatedDuration(years, temp);
                }

                months = temp;
            }

            if (input.Contains('D'))
            {
                startIndex = endIndex + 1;
                endIndex = input.IndexOf('D');
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(years, months, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(years, months, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The days component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return new DesignatedDuration(years, months, temp);
                }

                days = temp;
            }

            if (input.Contains('H'))
            {
                startIndex = endIndex + 1;

                if (input[startIndex] == 'T')
                {
                    startIndex++;
                }

                endIndex = input.IndexOf('H');
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(years, months, days, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(years, months, days, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The hours component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return new DesignatedDuration(years, months, days, temp);
                }

                hours = temp;
            }

            if (input.ContainsAfter('M', 'T'))
            {
                startIndex = endIndex + 1;

                if (input[startIndex] == 'T')
                {
                    startIndex++;
                }

                endIndex = input.IndexOf('M', input.IndexOf('T'));
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(years, months, days, hours, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(years, months, days, hours, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The minutes component must be a number.", input);
                }

                if (input.Length == endIndex + 1)
                {
                    return new DesignatedDuration(years, months, days, hours, temp);
                }

                minutes = temp;
            }

            if (input.Contains('S'))
            {
                startIndex = endIndex + 1;

                if (input[startIndex] == 'T')
                {
                    startIndex++;
                }

                endIndex = input.IndexOf('S', startIndex);
                componentString = input.Substring(startIndex, endIndex - startIndex);

                if (componentString.Contains('.'))
                {
                    return new DesignatedDuration(years, months, days, hours, minutes, double.Parse(componentString, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (componentString.Contains(','))
                {
                    return new DesignatedDuration(years, months, days, hours, minutes, double.Parse(componentString, CultureInfo.GetCultureInfo("fr-FR")));
                }

                if (!int.TryParse(componentString, out temp))
                {
                    throw new ParseException("The seconds component must be a number.", input);
                }

                return new DesignatedDuration(years, months, days, hours, minutes, temp);
            }

            throw new ParseException("A designated duration string must contain a least one time component.", input);
        }
    }
}