using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class DateTimeParser
    {
        internal static CalendarDateTime Parse(string input)
        {
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The datetime string is invalid.", input);
            }

            var dateIsCalendar = (components[0].Contains('-') && components[0].Length == 10) || (!components[0].Contains('-') && components[0].Length == 8);

            if (dateIsCalendar)
            {
                return new CalendarDateTime(CalendarDateParser.Parse(components[0], 0), Time.Parse(components[1]));
            }

            //var dateIsWeek = components[0].Contains('W');

            //if (dateIsWeek)
            //{
            //    date = WeekDateParser.Parse(components[0], 0).ToCalendarDate(CalendarDatePrecision.Day);
            //}

            //var dateIsOrdinal = !dateIsCalendar && !dateIsWeek;

            //if (dateIsWeek)
            //{
            //    date = OrdinalDateParser.Parse(components[0], 0).ToCalendarDate(CalendarDatePrecision.Day);
            //}

            //return new CalendarDateTime(date, time);

            return null;
        }
    }
}