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

        public static ExtendedDateTimeCollection ParseCollection(string extendedDateTimeCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                return null;
            }

            return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString);
        }

        public static ExtendedDateTime ParseExtendedDateTime(string extendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                return null;
            }

            return ExtendedDateTimeParser.Parse(extendedDateTimeString);
        }

        public static ExtendedDateTimeInterval ParseInterval(string extendedDateTimeIntervalString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                return null;
            }

            return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeIntervalString);
        }

        public static UnspecifiedExtendedDateTime ParseUnspecifiedExtendedDateTime(string unspecifiedExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(unspecifiedExtendedDateTimeString))
            {
                return null;
            }

            return (UnspecifiedExtendedDateTime)UnspecifiedExtendedDateTimeParser.Parse(unspecifiedExtendedDateTimeString);
        }

        public static ExtendedDateTimePossibilityCollection ParsePossibilityCollection(string extendedDateTimePossibilityCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimePossibilityCollectionString))
            {
                return null;
            }

            return ExtendedDateTimePossibilityCollectionParser.Parse(extendedDateTimePossibilityCollectionString);
        }
    }
}