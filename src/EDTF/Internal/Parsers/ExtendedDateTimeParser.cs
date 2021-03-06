﻿using System.Collections.Generic;
using System.Linq;

namespace System.EDTF.Internal.Parsers
{
    internal static class ExtendedDateTimeParser
    {
        internal static ExtendedDateTime Parse(string extendedDateTimeString, ExtendedDateTime extendedDateTime = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                throw new ArgumentNullException("extendedDateTimeString");
            }

            if (extendedDateTime == null)
            {
                extendedDateTime = new ExtendedDateTime();
            }

            InsertArtificialScopes(ref extendedDateTimeString);                                                        // e.g. 1995-11?-12~ => {{1995-11}?-12}~

            var componentBuffer = new List<char>();

            var isDatePart = true;
            var isTimeZonePart = false;
            var isSeasonQualifierPart = false;

            var currentDateComponent = 0;
            var currentTimeComponent = 0;

            var hasSeasonComponent = false;

            if (string.IsNullOrEmpty(extendedDateTimeString))
            {
                throw new ParseException("The input string cannot be empty.", extendedDateTimeString);
            }

            for (int i = 0; i < extendedDateTimeString.Length; i++)
            {
                var character = extendedDateTimeString[i];

                if (isDatePart)                                                                                        // Parsing date portion of extended date time.
                {
                    if (character == '(' || character == '{')                                                          // Scope increment for natural and artificial scopes.
                    {
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, GetScopeFlags(i - 1, extendedDateTimeString), componentBuffer, ref extendedDateTime);
                    }
                    else if (character == ')' || character == '}')                                                     // Scope decrement for natural and artificial scopes.
                    {
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, GetScopeFlags(i - 1, extendedDateTimeString), componentBuffer, ref extendedDateTime);

                        if (i + 1 < extendedDateTimeString.Length && GetFlag(extendedDateTimeString[i + 1]) != 0)
                        {
                            i++;                                                                                       // Skip past first flag if exists.

                            if (i + 1 < extendedDateTimeString.Length && GetFlag(extendedDateTimeString[i + 1]) != 0)
                            {
                                i++;                                                                                   // Skip past second flag if exists.
                            }
                        }
                    }
                    else if (char.IsDigit(character) || character == 'y' || character == 'e' || character == 'p')
                    {
                        componentBuffer.Add(character);
                    }
                    else if (character == '^')                                                                         // Add season qualifier indicator to component buffer.
                    {
                        componentBuffer.Add(character);

                        isSeasonQualifierPart = true;
                    }
                    else if (character == '-')
                    {
                        if (i == 0 || (i > 0 && extendedDateTimeString[i - 1] == 'y'))                                 // Hyphen is a negative sign.
                        {
                            componentBuffer.Add(character);
                        }
                        else                                                                                           // Hyphen is a component separator.
                        {
                            CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, GetScopeFlags(i - 1, extendedDateTimeString), componentBuffer, ref extendedDateTime);
                        }
                    }
                    else if (character == 'T')
                    {
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, GetScopeFlags(i - 1, extendedDateTimeString), componentBuffer, ref extendedDateTime);

                        isDatePart = false;
                    }
                    else if (isSeasonQualifierPart)
                    {
                        if (char.IsWhiteSpace(character))
                        {
                            throw new ParseException("Season qualifiers cannot contain whitespace.", new string(componentBuffer.ToArray()));
                        }

                        componentBuffer.Add(character);
                    }
                    else
                    {
                        throw new ParseException("The character \'" + character + "\' could not be recognized.", new string(componentBuffer.ToArray()));
                    }
                }
                else                                                                                                           // Parsing time portion of extended date time.
                {
                    if (char.IsDigit(character) || (character == ':' && isTimeZonePart))                                       // Add digit to component buffer.
                    {
                        componentBuffer.Add(character);
                    }
                    else if (character == ':' && !isTimeZonePart)
                    {
                        CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime);
                    }
                    else if (character == 'Z' || character == '+' || character == '-')                                         // Time zone component
                    {
                        CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime);

                        componentBuffer.Add(character);

                        isTimeZonePart = true;
                    }
                    else
                    {
                        throw new ParseException("The character \'" + character + "\' could not be recognized.", new string(componentBuffer.ToArray()));
                    }
                }
            }

            if (isDatePart)
            {
                CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, GetScopeFlags(extendedDateTimeString.Length - 1, extendedDateTimeString), componentBuffer, ref extendedDateTime);
            }
            else
            {
                CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime);
            }

            return extendedDateTime;
        }

        private static void CommitDateComponent(ref int dateComponentIndex, ref bool hasSeasonComponent, int flags, List<char> componentBuffer, ref ExtendedDateTime extendedDateTime)
        {
            if (componentBuffer.Count == 0)
            {
                return;
            }

            var componentString = new string(componentBuffer.ToArray());

            if (dateComponentIndex == 0)                                                           // We expect a year to appear first.
            {
                var isLongFormYear = false;
                var isExponent = false;
                var isPrecision = false;
                var digits = new List<char>();

                for (int i = 0; i < componentString.Length; i++)
                {
                    if (i == 0 && componentString[i] == 'y')                                      // Component is long-form year.
                    {
                        isLongFormYear = true;
                    }
                    else if (char.IsDigit(componentString[i]) || componentString[i] == '-')       // Character is year digit or negative sign.
                    {
                        digits.Add(componentString[i]);
                    }
                    else if (isLongFormYear && componentString[i] == 'e')                         // Component indicates exponent.
                    {
                        extendedDateTime.Year = int.Parse(new string(digits.ToArray()));

                        digits.Clear();

                        isExponent = true;
                    }
                    else if (componentString[i] == 'p' && isExponent)                            // Component indicates precision.
                    {
                        extendedDateTime.YearExponent = int.Parse(new string(digits.ToArray()));

                        digits.Clear();

                        isPrecision = true;
                        isExponent = false;
                    }
                    else
                    {
                        throw new ParseException("The year is invalid.", componentString);
                    }
                }

                if (isExponent)
                {
                    extendedDateTime.YearExponent = int.Parse(new string(digits.ToArray()));
                }
                else if (isPrecision)
                {
                    extendedDateTime.YearPrecision = int.Parse(new string(digits.ToArray()));
                }
                else
                {
                    if (digits.Count == 0 || (digits.Count == 1 && digits[0] == '-'))
                    {
                        throw new ParseException("The year must have more than zero digits.", componentString);
                    }

                    var year = int.Parse(new string(digits.ToArray()));

                    if (isLongFormYear)
                    {
                        if (isExponent && year == 0)
                        {
                            throw new ParseException("The significand of a long year cannot be zero.", componentString);
                        }
                        else if (digits.Count < 4 || (digits[0] == '-' && digits.Count < 5))
                        {
                            throw new ParseException("The long year must have at least four digits.", componentString);
                        }
                    }
                    else if (digits.Count < 4 || (digits[0] == '-' && digits.Count < 5) || year < -9999 || year > 9999)
                    {
                        throw new ParseException("The year must have four digits.", componentString);
                    }

                    extendedDateTime.Year = year;
                }

                extendedDateTime.YearFlags = (YearFlags)flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 1)                                                 // We expect either a month or a season to appear second.
            {
                if (componentString[0] == '2')                                                // The component is a season.
                {
                    if (componentString.Contains('^'))                                        // Check for season qualifier.
                    {
                        var seasonComponentStrings = componentString.Split('^');

                        extendedDateTime.SeasonQualifier = seasonComponentStrings[1];

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

                    extendedDateTime.Season = (Season)seasonInteger;

                    extendedDateTime.SeasonFlags = (SeasonFlags)flags;

                    hasSeasonComponent = true;
                }
                else                                                                       // The component is a month
                {
                    if (componentString.Length != 2)
                    {
                        throw new ParseException("The month must have two digits.", componentString);
                    }

                    if (componentString.Any(c => !char.IsDigit(c)))
                    {
                        throw new ParseException("The month must be a number.", componentString);
                    }

                    var monthInteger = int.Parse(componentString);

                    if (monthInteger < 1 || monthInteger > 12)
                    {
                        throw new ParseException("The month must be between 1 and 12.", componentString);
                    }

                    extendedDateTime.Month = monthInteger;

                    extendedDateTime.Precision++;

                    extendedDateTime.MonthFlags = (MonthFlags)flags;
                }

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 2)                                                   // We expect a day.
            {
                if (hasSeasonComponent)
                {
                    throw new ParseException("A season and day cannot coexist.", componentString);
                }

                if (componentString.Length != 2)
                {
                    throw new ParseException("The day must have two digits.", componentString);
                }

                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The day must be a number.", componentString);
                }

                var dayInteger = int.Parse(componentString);

                if (dayInteger < 1 || dayInteger > 31)
                {
                    throw new ParseException("The day must be between 1 and 31.", componentString);
                }

                var daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(extendedDateTime.Year, extendedDateTime.Month);

                if (dayInteger > daysInMonth)
                {
                    throw new ParseException("The month " + extendedDateTime.Month + " in the year " + extendedDateTime.Year + " has only " + daysInMonth + " days.", componentString);
                }

                extendedDateTime.Day = dayInteger;

                extendedDateTime.Precision++;

                extendedDateTime.DayFlags = (DayFlags)flags;

                dateComponentIndex++;
            }
            else if (dateComponentIndex > 2)
            {
                throw new ParseException("The date can have at most three components.", componentString);
            }

            componentBuffer.Clear();
        }

        private static void CommitTimeComponent(ref int timeComponentIndex, bool timeZonePart, List<char> componentBuffer, ref ExtendedDateTime extendedDateTime)
        {
            if (componentBuffer.Count == 0)
            {
                return;
            }

            var componentString = new string(componentBuffer.ToArray());

            if (timeComponentIndex == 0 && !timeZonePart)                                                               // We expect hours to appear first.
            {
                if (componentString.Any(c => !char.IsDigit(c)))
                {
                    throw new ParseException("The hour must be a number.", componentString);
                }
                else if (componentString.Length != 2 && !(componentString.Length == 3 && componentString.StartsWith("-")))
                {
                    throw new ParseException("The hour must be two digits long.", componentString);
                }

                extendedDateTime.Hour = int.Parse(componentString);

                extendedDateTime.Precision++;

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
                    throw new ParseException("The minute must be two digits long.", componentString);
                }

                extendedDateTime.Minute = int.Parse(componentString);

                extendedDateTime.Precision++;

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
                    throw new ParseException("The second must be two digits long.", componentString);
                }

                extendedDateTime.Second = int.Parse(componentString);

                extendedDateTime.Precision++;

                timeComponentIndex++;
            }
            else if (timeZonePart)
            {
                if (componentString.StartsWith("Z"))
                {
                    extendedDateTime.UtcOffset = TimeSpan.Zero;
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
                            throw new ParseException("The time zone hour offset must have exactly two digits.", hourOffsetString);
                        }
                    }
                    else if (hourOffsetString.Length != 2)
                    {
                        throw new ParseException("The time zone hour offset must have exactly two digits.", hourOffsetString);
                    }

                    extendedDateTime.UtcOffset = TimeSpan.FromHours(double.Parse(hourOffsetString));

                    if (timeZoneOffsetComponentStrings.Length == 2)                                  // Optional time zone minutes offset.
                    {
                        var minuteOffsetString = timeZoneOffsetComponentStrings[1];

                        if (minuteOffsetString.Any(c => !char.IsDigit(c)))
                        {
                            throw new ParseException("The time zone minute offset must be a number.", minuteOffsetString);
                        }
                        else if (minuteOffsetString.Length != 2)
                        {
                            throw new ParseException("The time zone minute offset must have exactly two digits.", minuteOffsetString);
                        }

                        extendedDateTime.UtcOffset += TimeSpan.FromMinutes(double.Parse(minuteOffsetString));
                    }

                    if (timeZoneOffsetComponentStrings.Length > 2)
                    {
                        throw new ParseException("The time zone offset can have at most two components: hours and minutes.", componentString);
                    }
                }
            }
            else
            {
                throw new ParseException("The time can have at most three components excluding the time zone.", componentString);
            }

            componentBuffer.Clear();
        }

        private static int GetFlag(char character)
        {
            if (character == '?')
            {
                return 1;
            }
            else if (character == '~')
            {
                return 2;
            }

            return 0;
        }

        private static int GetScopeFlags(int characterIndex, string extendedDateTimeString)
        {
            if (characterIndex < 0)
            {
                return 0;
            }

            var scopeFlags = 0;

            if (extendedDateTimeString[characterIndex] == '?' || extendedDateTimeString[characterIndex] == '~')              // Set starting point to close parenthesis or before.
            {
                characterIndex--;

                if (extendedDateTimeString[characterIndex - 1] == '?' || extendedDateTimeString[characterIndex - 1] == '~')
                {
                    characterIndex--;
                }
            }

            var currentScope = 0;
            var isArtificialScope = false;
            var isFirst = false;

            for (int i = characterIndex; i < extendedDateTimeString.Length; i++)
            {
                if (extendedDateTimeString[i] == '(')
                {
                    if (currentScope == -1)
                    {
                        return scopeFlags;
                    }

                    currentScope++;
                }
                else if (extendedDateTimeString[i] == ')')
                {
                    if (currentScope <= 0)
                    {
                        if (!isFirst)
                        {
                            isFirst = true;
                        }

                        if (i + 1 < extendedDateTimeString.Length)
                        {
                            scopeFlags |= GetFlag(extendedDateTimeString[i + 1]);                    // Search for first flag.

                            if (scopeFlags != 0)
                            {
                                i++;

                                if (i + 1 < extendedDateTimeString.Length)
                                {
                                    scopeFlags |= GetFlag(extendedDateTimeString[i + 1]);            // Search for second flag.

                                    if (scopeFlags == 3)                                             // 3 means both flags are present.
                                    {
                                        i++;
                                    }
                                }
                            }
                        }
                    }

                    currentScope--;
                }
                else if (extendedDateTimeString[i] == '}')
                {
                    if (!isFirst)
                    {
                        isFirst = true;
                        isArtificialScope = true;
                    }

                    if (isArtificialScope)
                    {
                        if (currentScope <= 0)
                        {
                            if (!isFirst)
                            {
                                isFirst = true;
                            }

                            if (i + 1 < extendedDateTimeString.Length)
                            {
                                scopeFlags |= GetFlag(extendedDateTimeString[i + 1]);                    // Search for first flag.

                                if (scopeFlags != 0)
                                {
                                    i++;

                                    if (i + 1 < extendedDateTimeString.Length)
                                    {
                                        scopeFlags |= GetFlag(extendedDateTimeString[i + 1]);            // Search for second flag.

                                        if (scopeFlags == 3)                                             // 3 means both flags are present.
                                        {
                                            i++;
                                        }
                                    }
                                }
                            }
                        }

                        currentScope--;
                    }
                }
                else if (extendedDateTimeString[i] == '{')
                {
                    if (currentScope == -1)
                    {
                        return scopeFlags;
                    }

                    currentScope++;
                }
            }

            return scopeFlags;
        }

        private static void InsertArtificialScopes(ref string extendedDateTimeString)
        {
            var characterList = extendedDateTimeString.ToList();

            var currentScope = 0;

            for (int i = 0; i < characterList.Count; i++)
            {
                if (characterList[i] == '(')
                {
                    currentScope++;
                }
                else if (characterList[i] == ')')
                {
                    if (i + 1 < characterList.Count && GetFlag(characterList[i + 1]) != 0)
                    {
                        i++;

                        if (i + 1 < characterList.Count && GetFlag(characterList[i + 1]) != 0)
                        {
                            i++;
                        }
                    }

                    currentScope--;
                }
                else if (characterList[i] == '?' || characterList[i] == '~')
                {
                    var foundScopeEnd = false;

                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (characterList[j] == '?' || characterList[j] == '~' && (characterList[j - 1] == ')' || characterList[j - 2] == ')'))
                        {
                            if (characterList[j - 1] == ')' || ((characterList[j - 1] == '~' || characterList[j - 1] == '?') && characterList[j - 2] == ')'))   // Found flag belongs to a normal scope.
                            {
                                characterList.Insert(j + 2, '{');             // Insert after hyphen. The curly brace is used instead to identify artificial scopes because normal scopes do not inherit the flags of artificial scopes.

                                foundScopeEnd = true;
                            }
                        }
                    }

                    if (!foundScopeEnd)
                    {
                        characterList.Insert(0, '{');
                    }

                    characterList.Insert(i + 1, '}');

                    if (i + 3 < characterList.Count && GetFlag(characterList[i + 3]) != 0)
                    {
                        i += 3;
                    }
                    else
                    {
                        i += 2;
                    }
                }
            }

            extendedDateTimeString = new string(characterList.ToArray());
        }
    }
}