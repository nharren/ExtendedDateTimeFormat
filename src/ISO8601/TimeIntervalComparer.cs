using System.Collections;
using System.Collections.Generic;
using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class TimeIntervalComparer : IComparer, IComparer<TimeInterval>
    {
        public int Compare(object x, object y)
        {
            if (!(x is TimeInterval) || !(y is TimeInterval))
            {
                throw new ArgumentException("The objects of comparison must be TimeIntervals.");
            }

            return Compare((TimeInterval)x, (TimeInterval)y);
        }

        public int Compare(TimeInterval x, TimeInterval y)
        {
            return x.ToTimeSpan().CompareTo(y.ToTimeSpan());
        }
    }
}