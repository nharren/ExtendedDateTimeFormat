using System.Collections.Generic;

namespace System.ISO8601.Internal.Comparers
{
    public class TimeComparer : IComparer<Time>
    {
        public int Compare(Time x, Time y)
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

            if (x.Hour > y.Hour)
            {
                return 1;
            }
            else if (x.Hour < y.Hour)
            {
                return -1;
            }

            if (x.Minute > y.Minute)
            {
                return 1;
            }
            else if (x.Minute < y.Minute)
            {
                return -1;
            }

            if (x.Second > y.Second)
            {
                return 1;
            }
            else if (x.Second < y.Second)
            {
                return -1;
            }

            if (x.UtcOffset.Hours > y.UtcOffset.Hours)
            {
                return 1;
            }
            else if (x.UtcOffset.Hours < y.UtcOffset.Hours)
            {
                return -1;
            }

            if (x.UtcOffset.Minutes > y.UtcOffset.Minutes)
            {
                return 1;
            }
            else if (x.UtcOffset.Minutes < y.UtcOffset.Minutes)
            {
                return -1;
            }

            return 0;
        }
    }
}