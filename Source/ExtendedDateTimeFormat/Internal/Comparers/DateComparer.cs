using System.Collections.Generic;
using System.ExtendedDateTimeFormat.Internal.Types;

namespace System.ExtendedDateTimeFormat.Internal.Comparers
{
    public class DateComparer : IComparer<Date>
    {
        public int Compare(Date x, Date y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            {
                return 0;
            }

            if (ReferenceEquals(y, null))
            {
                return 1;
            }

            if (ReferenceEquals(x, null))
            {
                return -1;
            }

            CalendarDate calendarDateX = null;

            if (x is CalendarDate)
            {
                calendarDateX = (CalendarDate)x;
            }
            else if (x is OrdinalDate)
            {
                calendarDateX = ((OrdinalDate)x).ToCalendarDate(CalendarDatePrecision.Day);
            }
            else if (x is WeekDate)
            {
                calendarDateX = ((WeekDate)x).ToCalendarDate(CalendarDatePrecision.Day);
            }

            CalendarDate calendarDateY = null;

            if (y is CalendarDate)
            {
                calendarDateY = (CalendarDate)y;
            }
            else if (y is OrdinalDate)
            {
                calendarDateY = ((OrdinalDate)y).ToCalendarDate(CalendarDatePrecision.Day);
            }
            else if (y is WeekDate)
            {
                calendarDateY = ((WeekDate)y).ToCalendarDate(CalendarDatePrecision.Day);
            }

            if (calendarDateX.Year > calendarDateY.Year)
            {
                return 1;
            }

            if (calendarDateX.Year < calendarDateY.Year)
            {
                return -1;
            }

            if (calendarDateX.Month > calendarDateY.Month)
            {
                return 1;
            }

            if (calendarDateX.Month < calendarDateY.Month)
            {
                return -1;
            }

            if (calendarDateX.Day > calendarDateY.Day)
            {
                return 1;
            }
            else if (calendarDateX.Day < calendarDateY.Day)
            {
                return -1;
            }

            return 0;
        }
    }
}