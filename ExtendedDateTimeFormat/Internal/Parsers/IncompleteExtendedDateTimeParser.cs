using System.Collections.Generic;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class IncompleteExtendedDateTimeParser
    {
        public static IncompleteExtendedDateTime Parse(string incompleteExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(incompleteExtendedDateTimeString))
            {
                return null;
            }

            var currentScope = 0;
            var inheritedScopeFlagDictionary = new Dictionary<int, ExtendedDateTimeFlags>();
            var incompleteExtendedDateTime = new IncompleteExtendedDateTime();
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

            for (int i = 0; i < incompleteExtendedDateTimeString.Length; i++)
            {
                var extendedDateTimeCharacter = incompleteExtendedDateTimeString[i];

                if (isDatePart)                                                                  // Parsing date portion of extended date time.
                {
                    if (extendedDateTimeCharacter == '(')                                        // Scope increment.
                    {
                        if (componentBuffer.Count > 0)
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

                            componentBuffer.Clear();
                        }

                        currentScope++;

                        scopeFlagDictionary[currentScope] = GetScopeFlags(currentScope, incompleteExtendedDateTimeString);

                        inheritedScopeFlagDictionary[currentScope] = scopeFlagDictionary[currentScope] | inheritedScopeFlagDictionary[currentScope - 1];          // An inner scope inherits flags of the outer scope.
                    }
                    else if (extendedDateTimeCharacter == ')')                                   // Scope decrement.
                    {
                        if (componentBuffer.Count > 0)
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

                            componentBuffer.Clear();
                        }

                        while (scopeFlagDictionary[currentScope] != 0)                           // Skip past scope flags.
                        {
                            scopeFlagDictionary[currentScope] = scopeFlagDictionary[currentScope] & (scopeFlagDictionary[currentScope] - 1);

                            i++;
                        }

                        if (i + 1 < incompleteExtendedDateTimeString.Length && incompleteExtendedDateTimeString[i + 1] == '-')        // We have already committed the buffer.
                        {
                            i++;
                        }

                        currentScope--;

                        if (currentScope < 0)
                        {
                            throw new ParseException("There are more close than open parentheses.", new string(componentBuffer.ToArray()));
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
                        if (i == 0 || (i > 0 && incompleteExtendedDateTimeString[i - 1] == 'y'))           // Hyphen is a negative sign.
                        {
                            componentBuffer.Add(extendedDateTimeCharacter);
                        }
                        else                                                                              // Hyphen is component separator.
                        {
                            CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

                            componentBuffer.Clear();

                            componentFlags = 0;
                        }
                    }
                    else if (extendedDateTimeCharacter == 'T')
                    {
                        CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

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
                        throw new ParseException("The character \'" + extendedDateTimeCharacter + "\' could not be recognized.", new string(componentBuffer.ToArray()));
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
                        CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

                        componentBuffer.Clear();
                    }
                    else if (extendedDateTimeCharacter == 'Z' || extendedDateTimeCharacter == '+' || extendedDateTimeCharacter == '-')         // Time zone component
                    {
                        CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);

                        componentBuffer.Clear();

                        componentBuffer.Add(extendedDateTimeCharacter);

                        timeZonePart = true;
                    }
                    else
                    {
                        throw new ParseException("The character \'" + extendedDateTimeCharacter + "\' could not be recognized.", new string(componentBuffer.ToArray()));
                    }
                }
            }

            if (componentBuffer.Count > 0)
            {
                if (isDatePart)
                {
                    CommitDateComponent(ref dateComponentIndex, ref hasSeasonComponent, inheritedScopeFlagDictionary[currentScope] | componentFlags, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);
                }
                else
                {
                    CommitTimeComponent(ref timeComponentIndex, timeZonePart, new string(componentBuffer.ToArray()), incompleteExtendedDateTime);
                }
            }

            if (currentScope > 0)
            {
                throw new ParseException("There are more open than close parentheses.", new string(componentBuffer.ToArray()));
            }

            return incompleteExtendedDateTime;
        }

        private static void CommitTimeComponent(ref int timeComponentIndex, bool timeZonePart, string componentString, IncompleteExtendedDateTime incompleteExtendedDateTime)
        {
            if (timeComponentIndex == 0 && !timeZonePart)                                                               // We expect hours to appear first.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The hour must be a number.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("The hour must have two digits.", componentString);
                }

                incompleteExtendedDateTime.Hour = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeComponentIndex == 1 && !timeZonePart)                                                         // We expect minutes to appear second.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The minute must be a number.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("The minute have two digits.", componentString);
                }

                incompleteExtendedDateTime.Minute = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeComponentIndex == 2 && !timeZonePart)                                                        // We expect seconds to appear third.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The second must be a number.", componentString);
                }
                else if (componentString.Length != 2)
                {
                    throw new ParseException("The second must have two digits.", componentString);
                }

                incompleteExtendedDateTime.Second = int.Parse(componentString);

                timeComponentIndex++;
            }
            else if (timeZonePart)
            {
                incompleteExtendedDateTime.TimeZone = new TimeZone();

                if (componentString.StartsWith("Z"))
                {
                    incompleteExtendedDateTime.TimeZone.HourOffset = 0;
                }
                else if (componentString.StartsWith("+") || componentString.StartsWith("-"))         // It must be a non-UTC time zone offset.
                {
                    var timeZoneOffsetComponentStrings = componentString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (timeZoneOffsetComponentStrings.Length == 0)
                    {
                        throw new ParseException("The time zone offset must have at least two digits.", componentString);
                    }

                    var hourOffsetString = timeZoneOffsetComponentStrings[0];                        // Time zone hours offset.

                    if (hourOffsetString.Any(c => !char.IsDigit(c) && c != '+' && c != '-'))
                    {
                        throw new ParseException("The time zone hour offset must be a number.", hourOffsetString);
                    }

                    if (hourOffsetString.StartsWith("+") || hourOffsetString.StartsWith("-"))
                    {
                        if (hourOffsetString.Length != 3)
                        {
                            throw new ParseException("The time zone hour offset must have two digits.", hourOffsetString);
                        }
                    }
                    else if (hourOffsetString.Length != 2)
                    {
                        throw new ParseException("The time zone hour offset must have two digits.", hourOffsetString);
                    }

                    incompleteExtendedDateTime.TimeZone.HourOffset = int.Parse(hourOffsetString);

                    if (timeZoneOffsetComponentStrings.Length == 2)                                  // Optional time zone minutes offset.
                    {
                        var minuteOffsetString = timeZoneOffsetComponentStrings[1];

                        if (minuteOffsetString.Any(c => !char.IsDigit(c)))
                        {
                            throw new ParseException("The time zone minute offset must be a number.", minuteOffsetString);
                        }
                        else if (minuteOffsetString.Length != 2)
                        {
                            throw new ParseException("The time zone minute offset must have two digits.", minuteOffsetString);
                        }

                        incompleteExtendedDateTime.TimeZone.MinuteOffset = int.Parse(minuteOffsetString);
                    }

                    if (!incompleteExtendedDateTime.TimeZone.IsValidOffset())
                    {
                        throw new ParseException("The time zone has an unknown UTC offset.", componentString);
                    }

                    if (timeZoneOffsetComponentStrings.Length > 2)
                    {
                        throw new ParseException("The time zone can have at most two components: hours and minutes.", componentString);
                    }
                }
            }
            else
            {
                throw new ParseException("The time can have at most three components excluding the time zone.", componentString);
            }
        }

        private static void CommitDateComponent(ref int dateComponentIndex, ref bool hasSeasonComponent, ExtendedDateTimeFlags flags, string componentString, IncompleteExtendedDateTime incompleteExtendedDateTime)
        {
            if (dateComponentIndex == 0)                                                          // We expect a year to appear first.
            {
                if (componentString.Length < 4)
                {
                    throw new ParseException("The year must have at least four characters.", new string(componentString.ToArray()));
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
                        incompleteExtendedDateTime.Year = new string(characterList.ToArray());

                        characterList.Clear();

                        isExponent = true;
                    }
                    else if (componentString[i] == 'p' && isExponent)                            // Component indicates precision.
                    {
                        incompleteExtendedDateTime.YearExponent = int.Parse(new string(characterList.ToArray()));

                        characterList.Clear();

                        isPrecision = true;
                        isExponent = false;
                    }
                    else
                    {
                        throw new ParseException("The year is invalid.", new string(componentString.ToArray()));
                    }
                }

                if (characterList.Count > 0)
                {
                    if (isExponent)
                    {
                        incompleteExtendedDateTime.YearExponent = int.Parse(new string(characterList.ToArray()));
                    }
                    else if (isPrecision)
                    {
                        incompleteExtendedDateTime.YearPrecision = int.Parse(new string(characterList.ToArray()));
                    }
                    else
                    {
                        incompleteExtendedDateTime.Year = new string(characterList.ToArray());
                    }
                }

                incompleteExtendedDateTime.YearFlags = flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 1)                                                 // We expect either a month or a season to appear second.
            {
                if (componentString[0] == '2')                                                // The component is a season
                {
                    if (componentString.Contains('^'))                                        // check for season qualifier.
                    {
                        var seasonComponentStrings = componentString.Split('^');

                        incompleteExtendedDateTime.SeasonQualifier = seasonComponentStrings[1];

                        componentString = seasonComponentStrings[0];
                    }

                    if (componentString.Length != 2)
                    {
                        throw new ParseException("The season must have two digits (excluding the qualifier).", componentString);
                    }
                    else if (componentString.Any(c => !char.IsDigit(c)))
                    {
                        throw new ParseException("The season must be a number (excluding the qualifier).", componentString);
                    }

                    var seasonInteger = int.Parse(componentString);

                    if (seasonInteger < 21 || seasonInteger > 24)
                    {
                        throw new ParseException("The season must be between 21 and 24.", componentString);
                    }

                    incompleteExtendedDateTime.Season = (Season)seasonInteger;

                    incompleteExtendedDateTime.SeasonFlags = flags;

                    hasSeasonComponent = true;
                }
                else                                                                       // The component is a month
                {
                    if (componentString.Length != 2)
                    {
                        throw new ParseException("The month must have two characters.", componentString);
                    }

                    incompleteExtendedDateTime.Month = componentString;

                    incompleteExtendedDateTime.MonthFlags = flags;
                }

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 2)                                                   // We expect a day.
            {
                if (hasSeasonComponent)
                {
                    throw new ParseException("A season and a day cannot coexist.", componentString);
                }

                if (componentString.Length != 2)
                {
                    throw new ParseException("The day must have two characters.", componentString);
                }

                incompleteExtendedDateTime.Day = componentString;

                incompleteExtendedDateTime.DayFlags = flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex > 2)
            {
                throw new ParseException("The date can have at most three components.", componentString);
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