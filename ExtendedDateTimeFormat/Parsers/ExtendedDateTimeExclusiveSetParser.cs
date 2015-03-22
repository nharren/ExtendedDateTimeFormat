using System.Collections.Generic;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Parsers
{
    public static class ExtendedDateTimeExclusiveSetParser
    {
        public static ExtendedDateTimeExclusiveSet Parse(string extendedDateTimeExclusiveSetString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeExclusiveSetString))
            {
                return null;
            }

            var hasStartBrace = extendedDateTimeExclusiveSetString[0] == '[';
            var hasEndBrace = extendedDateTimeExclusiveSetString[extendedDateTimeExclusiveSetString.Length - 1] == ']';

            if (!hasStartBrace || !hasEndBrace)
            {
                throw new ParseException("An exclusive set string must be surrounded by square brackets.", extendedDateTimeExclusiveSetString);
            }

            var contentsString = extendedDateTimeExclusiveSetString.Substring(1, extendedDateTimeExclusiveSetString.Length - 2);
            var closingChar = (char?)null;
            var setRanges = new Dictionary<int, int>();             // A dictionary of indexes where sets begin and end within the contents string.
            var setStartingIndex = (int?)null;

            for (int i = 0; i < contentsString.Length; i++)        // Locate nested sets.
            {
                if (contentsString[i] == '{' && setStartingIndex == null)
                {
                    closingChar = '}';

                    setStartingIndex = i;
                }
                else if (contentsString[i] == '[' && setStartingIndex == null)
                {
                    closingChar = ']';

                    setStartingIndex = i;
                }
                else if (contentsString[i] == closingChar && setStartingIndex != null)
                {
                    setRanges.Add(setStartingIndex.Value, i);

                    setStartingIndex = null;
                }
            }

            var currentSetRangeIndex = 0;
            var remainingChars = new List<char>();
            var exclusiveSet = new ExtendedDateTimeExclusiveSet();

            for (int i = 0; i < contentsString.Length; i++)                                     // Add set contents, including nested sets.
            {
                if (setRanges.Count > currentSetRangeIndex && i == setRanges.Keys.ElementAt(currentSetRangeIndex))
                {
                    var preceedingElementsString = new string(remainingChars.ToArray());        // Add elements preceeding the nested set.

                    var preceedingElementStrings = preceedingElementsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var preceedingElementString in preceedingElementStrings)
                    {
                        if (preceedingElementString.Contains(".."))
                        {
                            exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeRangeParser.Parse(preceedingElementString));
                        }
                        else if (preceedingElementString.Contains('u') || preceedingElementString.Contains('x'))
                        {
                            exclusiveSet.Add((IExtendedDateTimeSetType)ShortFormExtendedDateTimeParser.Parse(preceedingElementString));
                        }
                        else
                        {
                            exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeParser.Parse(preceedingElementString));
                        }
                    }

                    remainingChars.Clear();

                    var setString = contentsString.Substring(i, setRanges[i] - i);      // Add nested set.

                    if (setString[0] == '{')
                    {
                        exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeInclusiveSetParser.Parse(setString));
                    }
                    else
                    {
                        exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeExclusiveSetParser.Parse(setString));
                    }

                    i = setRanges[i];
                    currentSetRangeIndex++;
                }
                else
                {
                    remainingChars.Add(contentsString[i]);
                }
            }

            var remainingElementsString = new string(remainingChars.ToArray());        // Add all elements that remain.

            var remainingElementStrings = remainingElementsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var remainingElementString in remainingElementStrings)
            {
                if (remainingElementString.Contains(".."))
                {
                    exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeRangeParser.Parse(remainingElementString));
                }
                else if (remainingElementString.Contains('u') || remainingElementString.Contains('x'))
                {
                    exclusiveSet.Add((IExtendedDateTimeSetType)ShortFormExtendedDateTimeParser.Parse(remainingElementString));
                }
                else
                {
                    exclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeParser.Parse(remainingElementString));
                }
            }

            return exclusiveSet;
        }
    }
}