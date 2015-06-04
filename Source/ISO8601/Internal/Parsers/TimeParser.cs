using System.Collections.Generic;
using System.Linq;

namespace System.ISO8601.Internal.Parsers
{
    internal static class TimeParser
    {
        private enum TimeComponent { Hour = 0, Minute = 1, Second = 2, UtcHour = 3, UtcMinute = 4 }

        internal static Time Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < 2)
            {
                throw new ArgumentException("The time string must be at least two digits long.", nameof(input));
            }

            int hourStartIndex = input[0] == 'T' ? 1 : 0;
            int hour = int.Parse(input.Substring(hourStartIndex, 2));

            if (input.Length == hourStartIndex + 2)
            {
                return new Time(hour);
            }

            var nextChar = input[hourStartIndex + 2];

            if (nextChar == '.' || nextChar == ',')
            {
                var fractionIndex = hourStartIndex + 3;
                var fractionPartBuffer = new List<char>();

                while (fractionIndex < input.Length && char.IsDigit(input[fractionIndex]))
                {
                    fractionPartBuffer.Add(input[fractionIndex]);
                    fractionIndex++;
                }

                if (fractionIndex == input.Length)
                {
                    return new Time(double.Parse(hour + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count };
                }
                else
                {
                    return new Time(double.Parse(hour + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count, UtcOffset = ParseUtcOffset(input, fractionIndex) };
                }

            }
            else if (nextChar == '+' || nextChar == '-' || nextChar == 'Z')
            {
                return new Time(hour) { UtcOffset = ParseUtcOffset(input, hourStartIndex + 2) };
            }

            var hasColons = nextChar == ':';
            int minuteStartIndex = hasColons ? hourStartIndex + 3 : hourStartIndex + 2;
            int minute = int.Parse(input.Substring(minuteStartIndex, 2));

            if (input.Length == minuteStartIndex + 2)
            {
                return new Time(hour, minute);
            }

            nextChar = input[minuteStartIndex + 2];

            if (nextChar == '.' || nextChar == ',')
            {
                var fractionIndex = minuteStartIndex + 3;
                var fractionPartBuffer = new List<char>();

                while (fractionIndex < input.Length && char.IsDigit(input[fractionIndex]))
                {
                    fractionPartBuffer.Add(input[fractionIndex]);
                    fractionIndex++;
                }

                if (fractionIndex == input.Length)
                {
                    return new Time(hour, double.Parse(minute + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count };
                }
                else
                {
                    return new Time(hour, double.Parse(minute + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count, UtcOffset = ParseUtcOffset(input, fractionIndex) };
                }

            }
            else if (nextChar == '+' || nextChar == '-' || nextChar == 'Z')
            {
                return new Time(hour, minute) { UtcOffset = ParseUtcOffset(input, minuteStartIndex + 2) };
            }

            int secondStartIndex = hasColons ? minuteStartIndex + 3 : minuteStartIndex + 2;
            int second = int.Parse(input.Substring(secondStartIndex, 2));

            if (input.Length == secondStartIndex + 2)
            {
                return new Time(hour, minute, second);
            }

            nextChar = input[secondStartIndex + 2];

            if (nextChar == '.' || nextChar == ',')
            {
                var fractionIndex = secondStartIndex + 3;
                var fractionPartBuffer = new List<char>();

                while (fractionIndex < input.Length && char.IsDigit(input[fractionIndex]))
                {
                    fractionPartBuffer.Add(input[fractionIndex]);
                    fractionIndex++;
                }

                if (fractionIndex == input.Length)
                {
                    return new Time(hour, minute, double.Parse(second + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count };
                }
                else
                {
                    return new Time(hour, minute, double.Parse(second + "." + new string(fractionPartBuffer.ToArray()))) { FractionLength = fractionPartBuffer.Count, UtcOffset = ParseUtcOffset(input, fractionIndex) };
                }

            }
            else
            {
                return new Time(hour, minute, second) { UtcOffset = ParseUtcOffset(input, secondStartIndex + 2) };
            }
        }

        private static UtcOffset ParseUtcOffset(string input, int utcOffsetStartIndex)
        {
            if (input[utcOffsetStartIndex] == 'Z')
            {
                return new UtcOffset(0, 0);
            }

            int hours = int.Parse(input.Substring(utcOffsetStartIndex, 3));

            if (input.Length == utcOffsetStartIndex + 3)
            {
                return new UtcOffset(hours);
            }

            var hasColons = input[utcOffsetStartIndex + 3] == ':';
            int minutes = int.Parse(input.Substring(hasColons ? utcOffsetStartIndex + 4 : utcOffsetStartIndex + 3, 2));

            return new UtcOffset(hours, minutes);
        }
    }
}