using System.Collections;
using System.Collections.Generic;

namespace System.ISO8601
{
    public class TimeComparer : IComparer, IComparer<Time>
    {
        public int Compare(object x, object y)
        {
            if (!(x is Time) || !(y is Time))
            {
                throw new ArgumentException("The objects of comparison must be Times.");
            }

            return Compare((Time)x, (Time)y);
        }

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

            if (x.UtcOffset.Hours > y.UtcOffset.Hours)
            {
                return 1;
            }

            if (x.UtcOffset.Hours < y.UtcOffset.Hours)
            {
                return -1;
            }

            if (x.UtcOffset.Minutes > y.UtcOffset.Minutes)
            {
                return 1;
            }

            if (x.UtcOffset.Minutes < y.UtcOffset.Minutes)
            {
                return -1;
            }

            return 0;
        }
    }
}