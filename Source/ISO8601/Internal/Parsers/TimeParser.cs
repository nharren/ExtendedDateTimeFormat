using System.Collections.Generic;
using System.Linq;

namespace System.ISO8601.Internal.Parsers
{
    internal static class TimeParser
    {
        private enum TimeComponent { Hour = 0, Minute = 1, Second = 2, UtcHour = 3, UtcMinute = 4 }

        internal static Time Parse(string input)
        {
            var componentBuffer = new List<char>();
            var currentComponent = TimeComponent.Hour;

            double hour = 0d;
            double? minute = null;
            double? second = null;
            int? utcHours = null;
            int? utcMinutes = null;
            var invertUtc = false;
            var componentLength = 2;

            for (int i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (character == ',')
                {
                    character = '.';
                }

                var noDecimalFraction = true;

                if (char.IsDigit(character) || character == '.')
                {
                    componentBuffer.Add(character);
                }
                else if (character == '.')
                {
                    componentBuffer.Add(character);
                    noDecimalFraction = false;
                }
                else if (character == 'Z')
                {
                    utcHours = 0;
                    utcMinutes = 0;
                }
                else if (character == '+')
                {
                    currentComponent = TimeComponent.UtcHour;
                }
                else if (character == '-')
                {
                    currentComponent = TimeComponent.UtcHour;
                    invertUtc = true;
                }
                else if (character != ':' || character != 'T')
                {
                    throw new ParseException("The character \'" + character + "\' could not be recognized.", new string(componentBuffer.ToArray()));
                }

                var isLastCharacter = ReferenceEquals(character, input.Last());
                var endOfComponent = (componentBuffer.Count == componentLength && noDecimalFraction) || isLastCharacter;

                if (endOfComponent)
                {
                    var componentString = new string(componentBuffer.ToArray());

                    switch (currentComponent)
                    {
                        case TimeComponent.Hour:
                            hour = double.Parse(componentString);
                            currentComponent++;
                            break;

                        case TimeComponent.Minute:
                            minute = double.Parse(componentString);
                            currentComponent++;
                            break;

                        case TimeComponent.Second:
                            second = double.Parse(componentString);
                            currentComponent++;
                            break;

                        case TimeComponent.UtcHour:
                            utcHours = int.Parse(componentString);
                            currentComponent++;
                            break;

                        case TimeComponent.UtcMinute:
                            utcMinutes = int.Parse(componentString);
                            break;
                    }

                    componentBuffer.Clear();
                }
            }

            var utcOffset = TimeZoneInfo.Local.BaseUtcOffset;

            if (utcHours.HasValue)
            {
                if (utcMinutes.HasValue)
                {
                    utcOffset = new TimeSpan(invertUtc ? -utcHours.Value : utcHours.Value, invertUtc ? -utcMinutes.Value : utcMinutes.Value, 0);
                }
                else
                {
                    utcOffset = TimeSpan.FromHours(invertUtc ? -utcHours.Value : utcHours.Value);
                }
            }

            if (minute == null)
            {
                return new Time(hour, utcOffset);
            }

            if (second == null)
            {
                return new Time((int)hour, minute.Value, utcOffset);
            }

            return new Time((int)hour, (int)minute.Value, second.Value, utcOffset);
        }
    }
}