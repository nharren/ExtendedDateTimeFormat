using System.Collections.Generic;
using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class TimeIntervalComparer : IComparer<TimeInterval>
    {
        public int Compare(TimeInterval x, TimeInterval y)
        {
            return x.ToTimeSpan().CompareTo(y.ToTimeSpan());
        }
    }
}