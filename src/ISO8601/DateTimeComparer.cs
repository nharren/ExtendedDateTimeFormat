using System.Collections;
using System.Collections.Generic;

namespace System.ISO8601
{
    public class DateTimeComparer : IComparer, IComparer<Abstract.DateTime>
    {
        public int Compare(object x, object y)
        {
            if (!(x is Abstract.DateTime) || !(y is Abstract.DateTime))
            {
                throw new ArgumentException("The objects of comparison must be DateTimes.");
            }

            return Compare((Abstract.DateTime)x, (Abstract.DateTime)y);
        }

        public int Compare(Abstract.DateTime x, Abstract.DateTime y)
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

            if (x is CalendarDateTime)
            {
                return Compare((CalendarDateTime)x, y);
            }

            if (x is OrdinalDateTime)
            {
                return Compare(((OrdinalDateTime)x).ToCalendarDateTime(), y);
            }

            if (x is WeekDateTime)
            {
                return Compare(((WeekDateTime)x).ToCalendarDateTime(), y);
            }

            throw new InvalidOperationException("The type of DateTime in the first argument is unrecongnized.");
        }

        public int Compare(CalendarDateTime x, Abstract.DateTime y)
        {
            if (y is CalendarDateTime)
            {
                return Compare(x, (CalendarDateTime)y);
            }

            if (y is OrdinalDateTime)
            {
                return Compare(x, ((OrdinalDateTime)y).ToCalendarDateTime());
            }

            if (y is WeekDateTime)
            {
                return Compare(x, ((WeekDateTime)y).ToCalendarDateTime());
            }

            throw new InvalidOperationException("The type of DateTime in the second argument is unrecongnized.");
        }

        public int Compare(CalendarDateTime x, CalendarDateTime y)
        {
            if (x.Year > y.Year)
            {
                return 1;
            }

            if (x.Year < y.Year)
            {
                return -1;
            }

            if (x.Month > y.Month)
            {
                return 1;
            }

            if (x.Month < y.Month)
            {
                return -1;
            }

            if (x.Day > y.Day)
            {
                return 1;
            }

            if (x.Day < y.Day)
            {
                return -1;
            }

            if (x.Hour > y.Hour)
            {
                return 1;
            }

            if (x.Hour < y.Hour)
            {
                return -1;
            }

            if (x.Minute > y.Minute)
            {
                return 1;
            }

            if (x.Minute < y.Minute)
            {
                return -1;
            }

            if (x.Second > y.Second)
            {
                return 1;
            }

            if (x.Second < y.Second)
            {
                return -1;
            }

            return 0;
        }
    }
}