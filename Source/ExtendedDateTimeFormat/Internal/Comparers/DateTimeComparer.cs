using System.Collections.Generic;

namespace System.ExtendedDateTimeFormat.Internal.Comparers
{
    public class DateTimeComparer : IComparer<Types.DateTime>
    {
        public int Compare(Types.DateTime x, Types.DateTime y)
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

            CalendarDateTime calendarDateTimeX = null;

            if (x is CalendarDateTime)
            {
                calendarDateTimeX = (CalendarDateTime)x;
            }
            else if (x is OrdinalDateTime)
            {
                calendarDateTimeX = ((OrdinalDateTime)x).ToCalendarDateTime();
            }
            else if (x is WeekDateTime)
            {
                calendarDateTimeX = ((WeekDateTime)x).ToCalendarDateTime();
            }

            CalendarDateTime calendarDateTimeY = null;

            if (y is CalendarDateTime)
            {
                calendarDateTimeY = (CalendarDateTime)y;
            }
            else if (y is OrdinalDateTime)
            {
                calendarDateTimeY = ((OrdinalDateTime)y).ToCalendarDateTime();
            }
            else if (y is WeekDateTime)
            {
                calendarDateTimeY = ((WeekDateTime)y).ToCalendarDateTime();
            }

            if (calendarDateTimeX.Date > calendarDateTimeY.Date)
            {
                return 1;
            }

            if (calendarDateTimeX.Date < calendarDateTimeY.Date)
            {
                return -1;
            }

            if (calendarDateTimeX.Time > calendarDateTimeY.Time)
            {
                return 1;
            }

            if (calendarDateTimeX.Time < calendarDateTimeY.Time)
            {
                return -1;
            }

            return 0;
        }
    }
}