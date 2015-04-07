using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeFormatParser
    {
        public static IExtendedDateTimeIndependentType Parse(string extendedDateTimeFormattedString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeFormattedString))
            {
                return null;
            }

            if (extendedDateTimeFormattedString.Contains('/'))
            {
                return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeFormattedString);
            }

            if (extendedDateTimeFormattedString[0] == '{')
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString[0] == '[')
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimePossibilityCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.ContainsBefore('u', '^'))
            {
                return (IExtendedDateTimeIndependentType)UnspecifiedExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.ContainsBefore('x', '^'))
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeMaskedPrecisionParser.Parse(extendedDateTimeFormattedString);
            }
            else
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
        }
    }
}