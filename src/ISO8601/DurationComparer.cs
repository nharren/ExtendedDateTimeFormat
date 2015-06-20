using System.Collections;
using System.Collections.Generic;
using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class DurationComparer : IComparer, IComparer<Duration>
    {
        public int Compare(Duration x, Duration y)
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

            if (x is CalendarDateDuration)
            {
                return Compare((CalendarDateDuration)x, y);
            }

            if (x is OrdinalDateDuration)
            {
                return Compare(((OrdinalDateDuration)x).ToCalendarDateDuration(), y);
            }

            if (x is CalendarDateTimeDuration)
            {
                return Compare((CalendarDateTimeDuration)x, y);
            }

            if (x is OrdinalDateTimeDuration)
            {
                return Compare(((OrdinalDateTimeDuration)x).ToCalendarDateTimeDuration(), y);
            }

            if (x is DesignatedDuration)
            {
                return Compare((DesignatedDuration)x, y);
            }

            if (x is TimeDuration)
            {
                return Compare((TimeDuration)x, y);
            }

            throw new InvalidOperationException("The duration type of the first argument is unrecognized.");
        }

        public int Compare(CalendarDateDuration x, Duration y)
        {
            if (y is CalendarDateDuration)
            {
                return Compare(x, (CalendarDateDuration)y);
            }

            if (y is OrdinalDateDuration)
            {
                return Compare(x, ((OrdinalDateDuration)y).ToCalendarDateDuration());
            }

            if (y is CalendarDateTimeDuration)
            {
                return Compare(x, (CalendarDateTimeDuration)y);
            }

            if (y is OrdinalDateTimeDuration)
            {
                return Compare(x, ((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration());
            }

            if (y is DesignatedDuration)
            {
                return Compare(x, (DesignatedDuration)y);
            }

            if (y is TimeDuration)
            {
                return Compare(x, (TimeDuration)y);
            }

            throw new InvalidOperationException("The duration type of the second argument is unrecognized.");
        }

        public int Compare(CalendarDateTimeDuration x, Duration y)
        {
            if (y is CalendarDateDuration)
            {
                return Compare((CalendarDateDuration)y, x) * -1;
            }

            if (y is OrdinalDateDuration)
            {
                return Compare(((OrdinalDateDuration)y).ToCalendarDateDuration(), x) * -1;
            }

            if (y is CalendarDateTimeDuration)
            {
                return Compare(x, (CalendarDateTimeDuration)y);
            }

            if (y is OrdinalDateTimeDuration)
            {
                return Compare(x, ((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration());
            }

            if (y is DesignatedDuration)
            {
                return Compare(x, (DesignatedDuration)y);
            }

            if (y is TimeDuration)
            {
                return Compare(x, (TimeDuration)y);
            }

            throw new InvalidOperationException("The duration type of the second argument is unrecognized.");
        }

        public int Compare(DesignatedDuration x, Duration y)
        {
            if (y is CalendarDateDuration)
            {
                return Compare((CalendarDateDuration)y, x) * -1;
            }

            if (y is OrdinalDateDuration)
            {
                return Compare(((OrdinalDateDuration)y).ToCalendarDateDuration(), x) * -1;
            }

            if (y is CalendarDateTimeDuration)
            {
                return Compare((CalendarDateTimeDuration)y, x) * -1;
            }

            if (y is OrdinalDateTimeDuration)
            {
                return Compare(((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration(), x) * -1;
            }

            if (y is DesignatedDuration)
            {
                return Compare(x, (DesignatedDuration)y);
            }

            if (y is TimeDuration)
            {
                return Compare(x, (TimeDuration)y);
            }

            throw new InvalidOperationException("The duration type of the second argument is unrecognized.");
        }

        public int Compare(TimeDuration x, Duration y)
        {
            if (y is CalendarDateDuration)
            {
                return Compare((CalendarDateDuration)y, x) * -1;
            }

            if (y is OrdinalDateDuration)
            {
                return Compare(((OrdinalDateDuration)y).ToCalendarDateDuration(), x) * -1;
            }

            if (y is CalendarDateTimeDuration)
            {
                return Compare((CalendarDateTimeDuration)y, x) * -1;
            }

            if (y is OrdinalDateTimeDuration)
            {
                return Compare(((OrdinalDateTimeDuration)y).ToCalendarDateTimeDuration(), x) * -1;
            }

            if (y is DesignatedDuration)
            {
                return Compare((DesignatedDuration)y, x) * -1;
            }

            if (y is TimeDuration)
            {
                return Compare(x, (TimeDuration)y);
            }

            throw new InvalidOperationException("The duration type of the second argument is unrecognized.");
        }

        public int Compare(CalendarDateDuration x, CalendarDateDuration y)
        {
            if (x.Years > y.Years)
            {
                return 1;
            }

            if (x.Years < y.Years)
            {
                return -1;
            }

            if (x.Centuries != null && y.Centuries == null)
            {
                return 1;
            }

            if (x.Centuries == null && y.Centuries != null)
            {
                return -1;
            }

            if (x.Centuries > y.Centuries)
            {
                return 1;
            }

            if (x.Centuries < y.Centuries)
            {
                return -1;
            }

            if (x.Months != null && y.Months == null)
            {
                return 1;
            }

            if (x.Months == null && y.Months != null)
            {
                return -1;
            }

            if (x.Months > y.Months)
            {
                return 1;
            }

            if (x.Months < y.Months)
            {
                return -1;
            }

            if (x.Days != null && y.Days == null)
            {
                return 1;
            }

            if (x.Days == null && y.Days != null)
            {
                return -1;
            }

            if (x.Days > y.Days)
            {
                return 1;
            }

            if (x.Days < y.Days)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateDuration x, CalendarDateTimeDuration y)
        {
            if (x.Centuries != null)
            {
                var xCenturies = long.Parse(x.Centuries + "00");

                if (xCenturies > y.Years)
                {
                    return 1;
                }

                if (xCenturies < y.Years)
                {
                    return -1;
                }
            }

            if (x.Years > y.Years)
            {
                return 1;
            }

            if (x.Years < y.Years)
            {
                return -1;
            }

            if (x.Months == null)
            {
                return -1;
            }

            if (x.Months > y.Months)
            {
                return 1;
            }

            if (x.Months < y.Months)
            {
                return -1;
            }

            if (x.Days == null)
            {
                return -1;
            }

            if (x.Days > y.Days)
            {
                return 1;
            }

            if (x.Days < y.Days)
            {
                return -1;
            }

            if (y.Hours > 0)
            {
                return -1;
            }

            if (y.Minutes != null && y.Minutes > 0)
            {
                return -1;
            }

            if (y.Seconds != null && y.Seconds > 0)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateDuration x, DesignatedDuration y)
        {
            if (x.Years != 0 && y.Years == null)
            {
                return 1;
            }

            if ((x.Months != null && x.Months != 0) && y.Months == null)
            {
                return 1;
            }

            if ((y.Months != null && y.Months != 0) && x.Months == null)
            {
                return -1;
            }

            if ((x.Days != null && x.Days != 0) && y.Days == null)
            {
                return 1;
            }

            if ((y.Days != null && y.Days != 0) && x.Days == null)
            {
                return -1;
            }

            if (y.Hours != null && y.Hours != 0)
            {
                return -1;
            }

            if (y.Minutes != null && y.Minutes != 0)
            {
                return -1;
            }

            if (y.Seconds != null && y.Seconds != 0)
            {
                return -1;
            }

            var xSeconds = (x.Days + (x.Months + x.Years * 365) * 30) * 24 * 60 * 60;
            var ySeconds = y.Seconds + (y.Minutes + (y.Hours + (y.Days + (y.Months + y.Years * 365) * 30) * 24) * 60) * 60;

            if (xSeconds > ySeconds)
            {
                return 1;
            }

            if (xSeconds < ySeconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateDuration x, TimeDuration y)
        {
            if ((x.Centuries != 0 && x.Centuries != 0) || x.Years != 0 || (x.Months != null && x.Months != 0) || (x.Days != null && x.Days != 0))
            {
                return 1;
            }

            if (y.Hours != 0 || (y.Minutes != null && y.Minutes != 0) || (y.Seconds != null && y.Seconds != 0))
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateTimeDuration x, CalendarDateTimeDuration y)
        {
            if (x.Years > y.Years)
            {
                return 1;
            }

            if (x.Years < y.Years)
            {
                return -1;
            }

            if (x.Months > y.Months)
            {
                return 1;
            }

            if (x.Months < y.Months)
            {
                return -1;
            }

            if (x.Days > y.Days)
            {
                return 1;
            }

            if (x.Days < y.Days)
            {
                return -1;
            }

            if (x.Hours > y.Hours)
            {
                return 1;
            }

            if (x.Hours < y.Hours)
            {
                return -1;
            }

            if (x.Minutes != null && y.Minutes == null)
            {
                return 1;
            }

            if (x.Minutes == null && y.Minutes != null)
            {
                return -1;
            }

            if (x.Minutes > y.Minutes)
            {
                return 1;
            }

            if (x.Minutes < y.Minutes)
            {
                return -1;
            }

            if (x.Seconds != null && y.Seconds == null)
            {
                return 1;
            }

            if (x.Seconds == null && y.Seconds != null)
            {
                return -1;
            }

            if (x.Seconds > y.Seconds)
            {
                return 1;
            }

            if (x.Seconds < y.Seconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateTimeDuration x, DesignatedDuration y)
        {
            if (y.Years == null && x.Years != 0)
            {
                return 1;
            }

            if (y.Months == null && x.Months != 0)
            {
                return 1;
            }

            if (y.Days == null && x.Days != 0)
            {
                return 1;
            }

            if (y.Hours == null && x.Hours != 0)
            {
                return 1;
            }

            if (y.Minutes == null && x.Minutes != 0)
            {
                return 1;
            }

            if (y.Seconds == null && x.Seconds != 0)
            {
                return 1;
            }

            var xSeconds = x.Seconds + (x.Minutes + (x.Hours + (x.Days + (x.Months + x.Years * 365) * 30) * 24) * 60) * 60;
            var ySeconds = y.Seconds + (y.Minutes + (y.Hours + (y.Days + (y.Months + y.Years * 365) * 30) * 24) * 60) * 60;

            if (xSeconds > ySeconds)
            {
                return 1;
            }

            if (xSeconds < ySeconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(CalendarDateTimeDuration x, TimeDuration y)
        {
            if (x.Years != 0 || x.Months != 0 || x.Days != 0)
            {
                return 1;
            }

            if (x.Hours > y.Hours)
            {
                return 1;
            }

            if (x.Hours < y.Hours)
            {
                return -1;
            }

            if (x.Minutes != null && y.Minutes == null)
            {
                return 1;
            }

            if (x.Minutes == null && y.Minutes != null)
            {
                return -1;
            }

            if (x.Minutes > y.Minutes)
            {
                return 1;
            }

            if (x.Minutes < y.Minutes)
            {
                return -1;
            }

            if (x.Seconds != null && y.Seconds == null)
            {
                return 1;
            }

            if (x.Seconds == null && y.Seconds != null)
            {
                return -1;
            }

            if (x.Seconds > y.Seconds)
            {
                return 1;
            }

            if (x.Seconds < y.Seconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(DesignatedDuration x, DesignatedDuration y)
        {
            if (x.Years != null && x.Years > 0)
            {
                return 1;
            }

            if (y.Years != null && y.Years > 0)
            {
                return -1;
            }

            if (x.Years > y.Years)
            {
                return 1;
            }

            if (x.Years < y.Years)
            {
                return -1;
            }

            if (x.Months != null && x.Months > 0)
            {
                return 1;
            }

            if (y.Months != null && y.Months > 0)
            {
                return 1;
            }

            if (x.Months > y.Months)
            {
                return 1;
            }

            if (x.Months < y.Months)
            {
                return -1;
            }

            if (x.Weeks != null && x.Weeks > 0)
            {
                return 1;
            }

            if (y.Weeks != null && y.Weeks > 0)
            {
                return 1;
            }

            if (x.Weeks > y.Weeks)
            {
                return 1;
            }

            if (x.Weeks < y.Weeks)
            {
                return -1;
            }

            if (x.Days != null && x.Days > 0)
            {
                return 1;
            }

            if (y.Days != null && y.Days > 0)
            {
                return 1;
            }

            if (x.Days > y.Days)
            {
                return 1;
            }

            if (x.Days < y.Days)
            {
                return -1;
            }

            if (x.Hours != null && x.Hours > 0)
            {
                return 1;
            }

            if (y.Hours != null && y.Hours > 0)
            {
                return 1;
            }

            if (x.Hours > y.Hours)
            {
                return 1;
            }

            if (x.Hours < y.Hours)
            {
                return -1;
            }

            if (x.Minutes != null && y.Minutes == null)
            {
                return 1;
            }

            if (x.Minutes == null && y.Minutes != null)
            {
                return -1;
            }

            if (x.Minutes > y.Minutes)
            {
                return 1;
            }

            if (x.Minutes < y.Minutes)
            {
                return -1;
            }

            if (x.Seconds != null && y.Seconds == null)
            {
                return 1;
            }

            if (x.Seconds == null && y.Seconds != null)
            {
                return -1;
            }

            if (x.Seconds > y.Seconds)
            {
                return 1;
            }

            if (x.Seconds < y.Seconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(DesignatedDuration x, TimeDuration y)
        {
            if (x.Years != null && x.Years > 0)
            {
                return 1;
            }

            if (x.Months != null && x.Months > 0)
            {
                return 1;
            }

            if (x.Weeks != null && x.Weeks > 0)
            {
                return 1;
            }

            if (x.Days != null && x.Days > 0)
            {
                return 1;
            }

            if (x.Hours != null && x.Hours > 0)
            {
                return 1;
            }

            if (x.Hours > y.Hours)
            {
                return 1;
            }

            if (x.Hours < y.Hours)
            {
                return -1;
            }

            if (x.Minutes != null && y.Minutes == null)
            {
                return 1;
            }

            if (x.Minutes == null && y.Minutes != null)
            {
                return -1;
            }

            if (x.Minutes > y.Minutes)
            {
                return 1;
            }

            if (x.Minutes < y.Minutes)
            {
                return -1;
            }

            if (x.Seconds != null && y.Seconds == null)
            {
                return 1;
            }

            if (x.Seconds == null && y.Seconds != null)
            {
                return -1;
            }

            if (x.Seconds > y.Seconds)
            {
                return 1;
            }

            if (x.Seconds < y.Seconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(TimeDuration x, TimeDuration y)
        {
            if (x.Hours > y.Hours)
            {
                return 1;
            }

            if (x.Hours < y.Hours)
            {
                return -1;
            }

            if (x.Minutes != null && y.Minutes == null)
            {
                return 1;
            }

            if (x.Minutes == null && y.Minutes != null)
            {
                return -1;
            }

            if (x.Minutes > y.Minutes)
            {
                return 1;
            }

            if (x.Minutes < y.Minutes)
            {
                return -1;
            }

            if (x.Seconds != null && y.Seconds == null)
            {
                return 1;
            }

            if (x.Seconds == null && y.Seconds != null)
            {
                return -1;
            }

            if (x.Seconds > y.Seconds)
            {
                return 1;
            }

            if (x.Seconds < y.Seconds)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(object x, object y)
        {
            if (!(x is Duration) || !(y is Duration))
            {
                throw new ArgumentException("The objects to compare must be durations.");
            }

            return Compare((Duration)x, (Duration)y);
        }
    }
}