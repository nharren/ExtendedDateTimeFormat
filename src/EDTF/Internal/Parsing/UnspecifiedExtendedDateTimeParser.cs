namespace System.EDTF.Internal.Parsing
{
    internal static class UnspecifiedExtendedDateTimeParser
    {
        internal static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString, UnspecifiedExtendedDateTime unspecifiedExtendedDateTime = null)
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

            if (unspecifiedExtendedDateTime == null)
            {
                unspecifiedExtendedDateTime = new UnspecifiedExtendedDateTime();
            }

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

            unspecifiedExtendedDateTime.Day = components[2];

            return unspecifiedExtendedDateTime;
        }
    }
}