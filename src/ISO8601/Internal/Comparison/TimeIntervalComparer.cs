using System.Collections.Generic;
using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Comparison
{
    public class TimeIntervalComparer : IComparer<TimeInterval>
    {
        public int Compare(TimeInterval x, TimeInterval y)
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

            TimeSpan xTimeSpan;
            TimeSpan yTimeSpan;

            if (x is StartEndTimeInterval)
            {
                xTimeSpan = ((StartEndTimeInterval)x).ToTimeSpan();
            }
            else if (x is StartDurationTimeInterval)
            {
                xTimeSpan = ((StartDurationTimeInterval)x).ToTimeSpan();
            }

            throw new NotImplementedException();
        }
    }
}