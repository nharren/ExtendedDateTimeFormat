using System.Collections;
using System.Collections.Generic;

namespace System.ISO8601
{
    public class RecurringTimeIntervalComparer : IComparer, IComparer<RecurringTimeInterval>
    {
        public int Compare(object x, object y)
        {
            if (!(x is RecurringTimeInterval) || !(y is RecurringTimeInterval))
            {
                throw new ArgumentException("The objects to compare must be RecurringTimeIntervals.");
            }

            return Compare((RecurringTimeInterval)x, (RecurringTimeInterval)y);
        }

        public int Compare(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            if (x.Interval > y.Interval)
            {
                return 1;
            }

            if (x.Interval < y.Interval)
            {
                return -1;
            }

            if ((x.Recurrences != null && x.Recurrences > 0) && y.Recurrences == null)
            {
                return 1;
            }

            if ((y.Recurrences != null && y.Recurrences > 0) && x.Recurrences == null)
            {
                return -1;
            }

            if (x.Recurrences > y.Recurrences)
            {
                return 1;
            }

            if (x.Recurrences < y.Recurrences)
            {
                return -1;
            }

            return 0;
        }
    }
}