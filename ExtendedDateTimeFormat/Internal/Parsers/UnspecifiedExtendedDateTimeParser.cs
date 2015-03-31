using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    public static class UnspecifiedExtendedDateTimeParser
    {
        public static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString)
        {
            if (unspecifiedExtendedDateTimeString.Length > 10)
            {
                throw new ParseException("An unspecified extended date time must be between 4 and 10 characters long.", unspecifiedExtendedDateTimeString);
            }

            var components = unspecifiedExtendedDateTimeString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (components.Length > 3)
            {
                throw new ParseException("An unspecified extended date time can have at most two components.", unspecifiedExtendedDateTimeString);   
            }

            var unspecifiedExtendedDateTime = new UnspecifiedExtendedDateTime();

            unspecifiedExtendedDateTime.Year = components[0];

            if (components.Length == 1)
            {
                return unspecifiedExtendedDateTime;
            }

            unspecifiedExtendedDateTime.Month = components[1];

            if (components.Length == 2)
            {
                return unspecifiedExtendedDateTime;
            }

            unspecifiedExtendedDateTime.Day = components [2];

            return unspecifiedExtendedDateTime;
        }
    }
}
