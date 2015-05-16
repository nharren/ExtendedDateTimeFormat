using System.Collections.Generic;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class ExtendedDateTimeCollectionParser
    {
        internal static ExtendedDateTimeCollection Parse(string extendedDateTimeCollectionString, ExtendedDateTimeCollection collection = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                return null;
            }

            var hasStartBrace = extendedDateTimeCollectionString[0] == '{';
            var hasEndBrace = extendedDateTimeCollectionString[extendedDateTimeCollectionString.Length - 1] == '}';

            if (!hasStartBrace || !hasEndBrace)
            {
                throw new ParseException("A collection string must be surrounded by curly braces.", extendedDateTimeCollectionString);
            }

            var contentsString = extendedDateTimeCollectionString.Substring(1, extendedDateTimeCollectionString.Length - 2);
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

            if (collection == null)
            {
                collection = new ExtendedDateTimeCollection();
            }

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
                            collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeRangeParser.Parse(preceedingElementString));
                        }
                        else
                        {
                            collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeParser.Parse(preceedingElementString));
                        }
                    }

                    remainingChars.Clear();

                    var setString = contentsString.Substring(i, setRanges[i] - i);      // Add nested set.

                    if (setString[0] == '{')
                    {
                        collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeCollectionParser.Parse(setString));
                    }
                    else
                    {
                        collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimePossibilityCollectionParser.Parse(setString));
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
                    collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeRangeParser.Parse(remainingElementString));
                }
                else
                {
                    collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeParser.Parse(remainingElementString));
                }
            }

            return collection;
        }
    }
}