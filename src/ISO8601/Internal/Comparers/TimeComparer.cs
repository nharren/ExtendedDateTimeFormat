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

            var xHour = x.Precision == TimePrecision.Hour && x.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", x.Hour, x.FractionLength)) : x.Hour;
            var yHour = y.Precision == TimePrecision.Hour && y.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", y.Hour, y.FractionLength)) : y.Hour;

            if (xHour > yHour)
            {
                return 1;
            }
            else if (xHour < yHour)
            {
                return -1;
            }

            var xMinute = x.Precision == TimePrecision.Minute && x.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", x.Minute, x.FractionLength)) : x.Minute;
            var yMinute = y.Precision == TimePrecision.Minute && y.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", y.Minute, y.FractionLength)) : y.Minute;

            if (xMinute > yMinute)
            {
                return 1;
            }
            else if (xMinute < yMinute)
            {
                return -1;
            }

            var xSecond = x.Precision == TimePrecision.Second && x.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", x.Second, x.FractionLength)) : x.Second;
            var ySecond = y.Precision == TimePrecision.Second && y.FractionLength != 0 ? double.Parse(string.Format("{0}.{1}", y.Second, y.FractionLength)) : y.Second;

            if (xSecond > ySecond)
            {
                return 1;
            }
            else if (xSecond < ySecond)
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