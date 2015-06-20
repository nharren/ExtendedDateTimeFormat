using System.Collections;
using System.Collections.Generic;
using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class DateComparer : IComparer, IComparer<Date>
    {
        public int Compare(object x, object y)
        {
            if (!(x is Date) || !(y is Date))
            {
                throw new ArgumentException("The objects to compare must be Dates.");
            }

            return Compare((Date)x, (Date)y);
        }

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

            if (x is CalendarDate)
            {
                return Compare((CalendarDate)x, y);
            }

            if (x is OrdinalDate)
            {
                return Compare(((OrdinalDate)x).ToCalendarDate(), y);
            }

            if (x is WeekDate)
            {
                return Compare(((WeekDate)x).ToCalendarDate(), y);
            }

            throw new InvalidOperationException("The type of Date in the first argument is unrecongnized.");
        }

        public int Compare(CalendarDate x, Date y)
        {
            if (y is CalendarDate)
            {
                return Compare(x, (CalendarDate)y);
            }

            if (y is OrdinalDate)
            {
                return Compare(x, ((OrdinalDate)y).ToCalendarDate());
            }

            if (y is WeekDate)
            {
                return Compare(x, ((WeekDate)y).ToCalendarDate());
            }

            throw new InvalidOperationException("The type of Date in the second argument is unrecongnized.");
        }

        public int Compare(CalendarDate x, CalendarDate y)
        {
            if (x.Century > y.Century)
            {
                return 1;
            }

            if (x.Century < y.Century)
            {
                return -1;
            }

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

            return 0;
        }
    }
}