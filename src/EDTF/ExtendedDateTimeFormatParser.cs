using System.EDTF.Internal;
using System.EDTF.Internal.Parsing;
using System.Linq;

namespace System.EDTF
{
    public static class ExtendedDateTimeFormatParser
    {
        public static IExtendedDateTimeIndependentType Parse(string EDTFtedString)
        {
            if (string.IsNullOrEmpty(EDTFtedString))
            {
                throw new ParseException("The input string cannot be empty.", EDTFtedString);
            }

            if (EDTFtedString.Contains('/'))
            {
                return ExtendedDateTimeIntervalParser.Parse(EDTFtedString);
            }

            if (EDTFtedString[0] == '{')
            {
                return ExtendedDateTimeCollectionParser.Parse(EDTFtedString);
            }
            else if (EDTFtedString[0] == '[')
            {
                return ExtendedDateTimePossibilityCollectionParser.Parse(EDTFtedString);
            }
            else if (EDTFtedString.ContainsBefore('u', '^'))
            {
                return UnspecifiedExtendedDateTimeParser.Parse(EDTFtedString);
            }
            else if (EDTFtedString.ContainsBefore('x', '^'))
            {
                return ExtendedDateTimeMaskedPrecisionParser.Parse(EDTFtedString);
            }
            else
            {
                return ExtendedDateTimeParser.Parse(EDTFtedString);
            }
        }
    }
}