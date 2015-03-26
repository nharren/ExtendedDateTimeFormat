using System.Collections.Generic;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class ExtendedDateTimeInclusiveSetParser
    {
        public static ExtendedDateTimeInclusiveSet Parse(string extendedDateTimeInclusiveSetString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeInclusiveSetString))
            {
                return null;
            }

            var hasStartBrace = extendedDateTimeInclusiveSetString[0] == '{';
            var hasEndBrace = extendedDateTimeInclusiveSetString[extendedDateTimeInclusiveSetString.Length - 1] == '}';

            if (!hasStartBrace || !hasEndBrace)
            {
                throw new ParseException("An inclusive set string must be surrounded by curly braces.", extendedDateTimeInclusiveSetString);
            }

            var contentsString = extendedDateTimeInclusiveSetString.Substring(1, extendedDateTimeInclusiveSetString.Length - 2);
            var closingChar = (char?)null;
            var setRanges = new Dictionary<int, int>();             // A dictionary of indexes where sets begin and end within the contents string.
            var setStartingIndex = (int?)null;

            for (int i = 0; i < contentsString.Length; i++)         // Locate nested sets.
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
            var inclusiveSet = new ExtendedDateTimeInclusiveSet();

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
                            inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeRangeParser.Parse(preceedingElementString));
                        }
                        else if (preceedingElementString.Contains('u') || preceedingElementString.Contains('x'))
                        {
                            inclusiveSet.Add((IExtendedDateTimeSetType)ShortFormExtendedDateTimeParser.Parse(preceedingElementString));
                        }
                        else
                        {
                            inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeParser.Parse(preceedingElementString));
                        }
                    }

                    remainingChars.Clear();

                    var setString = contentsString.Substring(i, setRanges[i] - i);      // Add nested set.

                    if (setString[0] == '{')
                    {
                        inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeInclusiveSetParser.Parse(setString));
                    }
                    else
                    {
                        inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeExclusiveSetParser.Parse(setString));
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
                    inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeRangeParser.Parse(remainingElementString));
                }
                else if (remainingElementString.Contains('u') || remainingElementString.Contains('x'))
                {
                    inclusiveSet.Add((IExtendedDateTimeSetType)ShortFormExtendedDateTimeParser.Parse(remainingElementString));
                }
                else
                {
                    inclusiveSet.Add((IExtendedDateTimeSetType)ExtendedDateTimeParser.Parse(remainingElementString));
                }
            }

            return inclusiveSet;
        }
    }
}