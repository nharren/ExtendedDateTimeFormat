using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeFormatParser
    {
        public static IExtendedDateTimeIndependentType Parse(string extendedDateTimeFormattedString)
        {
            if (string.IsNullOrEmpty(extendedDateTimeFormattedString))
            {
                throw new ParseException("The input string cannot be empty.", extendedDateTimeFormattedString);
            }

            if (extendedDateTimeFormattedString.Contains('/'))
            {
                return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeFormattedString);
            }

            if (extendedDateTimeFormattedString[0] == '{')
            {
                return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString[0] == '[')
            {
                return ExtendedDateTimePossibilityCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.ContainsBefore('u', '^'))
            {
                return UnspecifiedExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.ContainsBefore('x', '^'))
            {
                return ExtendedDateTimeMaskedPrecisionParser.Parse(extendedDateTimeFormattedString);
            }
            else
            {
                return ExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
        }
    }
}