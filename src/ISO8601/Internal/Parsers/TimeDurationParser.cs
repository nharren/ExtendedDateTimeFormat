using System.Globalization;

namespace System.ISO8601.Internal.Parsers
{
    internal static class TimeDurationParser
    {
        internal static TimeDuration Parse(string input)
        {
            int startIndex = 2;
            int endIndex = startIndex + 2;

            if (endIndex < input.Length)
            {
                if (input[endIndex] == ',')
                {
                    endIndex = input.Length;

                    return new TimeDuration(double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("fr-FR")));
                }
                else if (input[endIndex] == '.')
                {
                    endIndex = input.Length;

                    return new TimeDuration(double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")));
                }
            }

            var hours = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new TimeDuration(hours);
            }

            startIndex = endIndex;

            if (input[startIndex] == ':')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            if (endIndex < input.Length)
            {
                if (input[endIndex] == ',')
                {
                    endIndex = input.Length;

                    return new TimeDuration(hours, double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("fr-FR")));
                }
                else if (input[endIndex] == '.')
                {
                    endIndex = input.Length;

                    return new TimeDuration(hours, double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")));
                }
            }

            var minutes = int.Parse(input.Substring(startIndex, endIndex - startIndex));

            if (input.Length == endIndex)
            {
                return new TimeDuration(hours, minutes);
            }

            startIndex = endIndex;

            if (input[startIndex] == ':')
            {
                startIndex++;
            }

            endIndex = startIndex + 2;

            if (endIndex < input.Length)
            {
                if (input[endIndex] == ',')
                {
                    endIndex = input.Length;

                    return new TimeDuration(hours, minutes, double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("fr-FR")));
                }
                else if (input[endIndex] == '.')
                {
                    endIndex = input.Length;

                    return new TimeDuration(hours, minutes, double.Parse(input.Substring(startIndex, endIndex - startIndex), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")));
                }
            }

            return new TimeDuration(hours, minutes, int.Parse(input.Substring(startIndex, endIndex - startIndex)));
        }
    }
}