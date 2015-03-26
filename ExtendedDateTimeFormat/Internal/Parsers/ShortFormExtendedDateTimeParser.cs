using System.Collections.Generic;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class ShortFormExtendedDateTimeParser
    {
        public static ShortFormExtendedDateTime Parse(string shortFormExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(shortFormExtendedDateTimeString))
            {
                return null;
            }

            var currentScope = 0;
            var inheritedScopeFlagDictionary = new Dictionary<int, ExtendedDateTimeFlags>();
            var shortFormExtendedDateTime = new ShortFormExtendedDateTime();
            var componentBuffer = new List<char>();
            var isDatePart = true;
            var dateComponentIndex = 0;
            var timeComponentIndex = 0;
            var timeZonePart = false;
            var componentFlags = (ExtendedDateTimeFlags)0;
            var hasSeasonComponent = false;
            var scopeFlagDictionary = new Dictionary<int, ExtendedDateTimeFlags>();
            var isSeasonQualifier = false;

            inheritedScopeFlagDictionary[currentScope] = 0;

            for (int i = 0; i < shortFormExtendedDateTimeString.Length; i++)
            {
                var extendedDateTimeCharacter = shortFormExtendedDateTimeString[i];

                if (isDatePart)                                                                  // Parsing date portion of extended date time.
                {
                    if (extendedDateTimeCharacter == '(')                                        // Scope increment.
                    {
                        if (componentBuffer.Count > 0)
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                            componentBuffer.Clear();
                        }

                        currentScope++;

                        scopeFlagDictionary[currentScope] = GetScopeFlags(currentScope, shortFormExtendedDateTimeString);

                        inheritedScopeFlagDictionary[currentScope] = scopeFlagDictionary[currentScope] | inheritedScopeFlagDictionary[currentScope - 1];          // An inner scope inherits flags of the outer scope.
                    }
                    else if (extendedDateTimeCharacter == ')')                                   // Scope decrement.
                    {
                        if (componentBuffer.Count > 0)
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                            componentBuffer.Clear();
                        }

                        while (scopeFlagDictionary[currentScope] != 0)                           // Skip past scope flags.
                        {
                            scopeFlagDictionary[currentScope] = scopeFlagDictionary[currentScope] & (scopeFlagDictionary[currentScope] - 1);

                            i++;
                        }

                        if (i + 1 < shortFormExtendedDateTimeString.Length && shortFormExtendedDateTimeString[i + 1] == '-')        // We have already committed the buffer.
                        {
                            i++;
                        }

                        currentScope--;

                        if (currentScope < 0)
                        {
                            throw new ParseException("There were more scopes closed then were opened.", new string(componentBuffer.ToArray()));
                        }
                    }
                    else if (char.IsDigit(extendedDateTimeCharacter) || extendedDateTimeCharacter == 'u' || extendedDateTimeCharacter == 'x')                            // Add digit to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == 'y')                                             // Add long year indicator to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == 'e')                                             // Add exponent indicator to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == 'p')                                             // Add precision indicator to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == '^')                                             // Add season qualifier indicator to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);

                        isSeasonQualifier = true;
                    }
                    else if (extendedDateTimeCharacter == '-')
                    {
                        if (i == 0 || (i > 0 && shortFormExtendedDateTimeString[i - 1] == 'y'))           // Hyphen is a negative sign.
                        {
                            componentBuffer.Add(extendedDateTimeCharacter);
                        }
                        else                                                                              // Hyphen is component separator.
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                            componentBuffer.Clear();

                            componentFlags = 0;
                        }
                    }
                    else if (extendedDateTimeCharacter == 'T')
                    {
                        CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                        componentBuffer.Clear();

                        isDatePart = false;
                    }
                    else if (extendedDateTimeCharacter == '~')
                    {
                        componentFlags |= GetFlag(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == '?')
                    {
                        componentFlags |= GetFlag(extendedDateTimeCharacter);
                    }
                    else if (isSeasonQualifier)
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else
                    {
                        throw new ParseException("The character \'" + extendedDateTimeCharacter + "\' in the extended date time string could not be recognized.", new string(componentBuffer.ToArray()));
                    }
                }
                else                                                                                                                           // Parsing time portion of extended date time.
                {
                    if (char.IsDigit(extendedDateTimeCharacter) || (extendedDateTimeCharacter == ':' && timeZonePart))                         // Add digit to component buffer.
                    {
                        componentBuffer.Add(extendedDateTimeCharacter);
                    }
                    else if (extendedDateTimeCharacter == ':' && !timeZonePart)
                    {
                        CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                        componentBuffer.Clear();
                    }
                    else if (extendedDateTimeCharacter == 'Z' || extendedDateTimeCharacter == '+' || extendedDateTimeCharacter == '-')         // Time zone component
                    {
                        CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);

                        componentBuffer.Clear();

                        componentBuffer.Add(extendedDateTimeCharacter);

                        timeZonePart = true;
                    }
                    else
                    {
                        throw new ParseException("The character \'" + extendedDateTimeCharacter + "\' in the extended date time string could not be recognized.", new string(componentBuffer.ToArray()));
                    }
                }
            }

            if (componentBuffer.Count > 0)
            {
                if (isDatePart)
                {
                    CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);
                }
                else
                {
                    CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), shortFormExtendedDateTime);
                }
            }

            if (currentScope > 0)
            {
                throw new ParseException("There were more scopes opened then were closed.", new string(componentBuffer.ToArray()));
            }

            return shortFormExtendedDateTime;
        }

        private static void CommitTimeComponent(ref int timeComponentIndex, bool timeZonePart, string componentString, ShortFormExtendedDateTime shortFormExtendedDateTime)
        {
            if (timeComponentIndex == 0 && !timeZonePart)                                                               // We expect hours to appear first.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("An hour component must contain only digits.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("An hour component must contain exactly two digits.", componentString);
                }

                shortFormExtendedDateTime.Hour = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeComponentIndex == 1 && !timeZonePart)                                                         // We expect minutes to appear second.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("A minute component must contain only digits.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("A minute component must contain exactly two digits.", componentString);
                }

                shortFormExtendedDateTime.Minute = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeComponentIndex == 2 && !timeZonePart)                                                        // We expect seconds to appear third.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("A second (the time quantity) component must contain only digits.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("A second (the time quantity) component must contain exactly two digits.", componentString);
                }

                shortFormExtendedDateTime.Second = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeZonePart)
            {
                shortFormExtendedDateTime.TimeZone = new TimeZone();

                if (componentString.StartsWith("Z"))
                {
                    shortFormExtendedDateTime.TimeZone.HourOffset = 0;
                }
                else if (componentString.StartsWith("+") || componentString.StartsWith("-"))         // It must be a non-UTC time zone offset.
                {
                    var timeZoneOffsetComponentStrings = componentString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (timeZoneOffsetComponentStrings.Length == 0)
                    {
                        throw new ParseException("A time zone offset must contain at least two digits to indicate the hour offset.", componentString);
                    }

                    var hourOffsetString = timeZoneOffsetComponentStrings[0];                        // Time zone hours offset.

                    if (hourOffsetString.Any(c => !char.IsDigit(c) && c != '+' && c != '-'))
                    {
                        throw new ParseException("A time zone hour offset must contain only digits.", hourOffsetString);
                    }

                    if (hourOffsetString.StartsWith("+") || hourOffsetString.StartsWith("-"))
                    {
                        if (hourOffsetString.Length != 3)
                        {
                            throw new ParseException("A time zone hour offset must contain exactly two digits.", hourOffsetString);
                        }
                    }
                    else if (hourOffsetString.Length != 2)
                    {
                        throw new ParseException("A time zone hour offset must contain exactly two digits.", hourOffsetString);
                    }

                    shortFormExtendedDateTime.TimeZone.HourOffset = int.Parse(hourOffsetString);

                    if (timeZoneOffsetComponentStrings.Length == 2)                                  // Optional time zone minutes offset.
                    {
                        var minuteOffsetString = timeZoneOffsetComponentStrings[1];

                        if (minuteOffsetString.Any(c => !char.IsDigit(c)))
                        {
                            throw new ParseException("A time zone minute offset must contain only digits.", minuteOffsetString);
                        }
                        else if (minuteOffsetString.Length != 2)
                        {
                            throw new ParseException("A time zone minute offset must contain exactly two digits.", minuteOffsetString);
                        }

                        shortFormExtendedDateTime.TimeZone.MinuteOffset = int.Parse(minuteOffsetString);
                    }

                    if (timeZoneOffsetComponentStrings.Length > 2)
                    {
                        throw new ParseException("A time zone offset can contain at most two components: One for hours and one for minutes.", componentString);
                    }
                }
            }
            else
            {
                throw new ParseException("There can be at most three time components exluding a time zone component.", componentString);
            }
        }

        private static void CommitDateComponent(ref int dateComponentIndex, ref bool hasSeasonComponent, ExtendedDateTimeFlags flags, string componentString, ShortFormExtendedDateTime shortFormExtendedDateTime)
        {
            if (dateComponentIndex == 0)                                                          // We expect a year to appear first.
            {
                if (componentString.Length < 4)
                {
                    throw new ParseException("A year must be at least four characters long.", new string(componentString.ToArray()));
                }

                var isLongFormYear = false;
                var isExponent = false;
                var isPrecision = false;
                var characterList = new List<char>();

                for (int i = 0; i < componentString.Length; i++)
                {
                    if (i == 0 && componentString[i] == 'y')                                      // Component is long-form year.
                    {
                        isLongFormYear = true;
                    }
                    else if (char.IsDigit(componentString[i]) || componentString[i] == '-' || componentString[i] == 'u' || componentString[i] == 'x')       // Character is year digit, negative sign, precision mask, or unspecified.
                    {
                        characterList.Add(componentString[i]);
                    }
                    else if (isLongFormYear && componentString[i] == 'e')                         // Component indicates exponent.
                    {
                        shortFormExtendedDateTime.Year = new string(characterList.ToArray());

                        characterList.Clear();

                        isExponent = true;
                    }
                    else if (componentString[i] == 'p' && isExponent)                            // Component indicates precision.
                    {
                        shortFormExtendedDateTime.YearExponent = int.Parse(new string(characterList.ToArray()));

                        characterList.Clear();

                        isPrecision = true;
                        isExponent = false;
                    }
                    else
                    {
                        throw new ParseException("A year string is invalid.", new string(componentString.ToArray()));
                    }
                }

                if (characterList.Count > 0)
                {
                    if (isExponent)
                    {
                        shortFormExtendedDateTime.YearExponent = int.Parse(new string(characterList.ToArray()));
                    }
                    else if (isPrecision)
                    {
                        shortFormExtendedDateTime.YearPrecision = int.Parse(new string(characterList.ToArray()));
                    }
                    else
                    {
                        shortFormExtendedDateTime.Year = new string(characterList.ToArray());
                    }
                }

                shortFormExtendedDateTime.YearFlags = flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 1)                                                 // We expect either a month or a season to appear second.
            {
                if (componentString[0] == '2')                                                // The component is a season
                {
                    if (componentString.Contains('^'))                                        // check for season qualifier.
                    {
                        var seasonComponentStrings = componentString.Split('^');

                        shortFormExtendedDateTime.SeasonQualifier = seasonComponentStrings[1];

                        componentString = seasonComponentStrings[0];
                    }

                    if (componentString.Length != 2)
                    {
                        throw new ParseException("A season string excluding a qualifier must be two digits long.", componentString);
                    }
                    else if (componentString.Any(c => !char.IsDigit(c)))
                    {
                        throw new ParseException("A season string excluding a qualifier can contain digits only.", componentString);
                    }

                    var seasonInteger = int.Parse(componentString);

                    if (seasonInteger < 21 || seasonInteger > 24)
                    {
                        throw new ParseException("A season string must be a number between 21 and 24.", componentString);
                    }

                    shortFormExtendedDateTime.Season = (Season)seasonInteger;

                    shortFormExtendedDateTime.SeasonFlags = flags;

                    hasSeasonComponent = true;
                }
                else                                                                       // The component is a month
                {
                    if (componentString.Length != 2)
                    {
                        throw new ParseException("A month string excluding a qualifier must be two characters long.", componentString);
                    }

                    shortFormExtendedDateTime.Month = componentString;

                    shortFormExtendedDateTime.MonthFlags = flags;
                }

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 2)                                                   // We expect a day.
            {
                if (hasSeasonComponent)
                {
                    throw new ParseException("A date string with a season cannot also have a day.", componentString);
                }

                if (componentString.Length != 2)
                {
                    throw new ParseException("A day string excluding a qualifier must be two characters long.", componentString);
                }

                shortFormExtendedDateTime.Day = componentString;

                shortFormExtendedDateTime.DayFlags = flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex > 2)
            {
                throw new ParseException("A date string can have at most three components.", componentString);
            }
        }

        private static ExtendedDateTimeFlags GetScopeFlags(int scope, string extendedDateTimeString)
        {
            var currentScope = 0;

            for (int i = 0; i < extendedDateTimeString.Length; i++)
            {
                if (extendedDateTimeString[i] == '(')
                {
                    currentScope++;
                }
                else if (extendedDateTimeString[i] == ')')
                {
                    if (currentScope == scope)                                                   // We have reached the end of the target scope.
                    {
                        if (i + 1 < extendedDateTimeString.Length)
                        {
                            var flags = GetFlag(extendedDateTimeString[i + 1]);                  // Search for first flag.

                            if (flags != 0)
                            {
                                if (i + 2 < extendedDateTimeString.Length)
                                {
                                    flags |= GetFlag(extendedDateTimeString[i + 2]);            // Search for second flag.
                                }
                            }

                            return flags;
                        }
                    }

                    currentScope--;
                }
            }

            return 0;
        }

        private static ExtendedDateTimeFlags GetFlag(char character)
        {
            if (character == '?')
            {
                return ExtendedDateTimeFlags.Uncertain;
            }
            else if (character == '~')
            {
                return ExtendedDateTimeFlags.Approximate;
            }

            return 0;
        }
    }
}