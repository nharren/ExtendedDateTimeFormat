using System.Collections.Generic;

namespace System.ExtendedDateTimeFormat.Internal.Comparers
{
    internal class ExtendedDateTimeComparer : IComparer<ExtendedDateTime>
    {
        public int Compare(ExtendedDateTime x, ExtendedDateTime y)
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

            long longXYear = x.Year;
            long longYYear = y.Year;

            if (x.YearExponent.HasValue)
            {
                try
                {
                    longXYear *= Convert.ToInt64(Math.Pow(10, x.YearExponent.Value));
                }
                catch (Exception)
                {
                    if (x.Year < 0)
                    {
                        longXYear = long.MinValue;
                    }
                    else
                    {
                        longXYear = long.MaxValue;
                    }
                }
            }

            if (y.YearExponent.HasValue)
            {
                try
                {
                    longYYear *= Convert.ToInt64(Math.Pow(10, y.YearExponent.Value));
                }
                catch (Exception)
                {
                    if (y.Year < 0)
                    {
                        longYYear = long.MinValue;
                    }
                    else
                    {
                        longYYear = long.MaxValue;
                    }
                }
            }

            if (longXYear > longYYear)
            {
                return 1;
            }
            else if (longXYear < longYYear)
            {
                return -1;
            }

            if (x.Season != Season.Undefined || y.Season != Season.Undefined)
            {
                if (y.Season == Season.Undefined)
                {
                    return 1;
                }
                else if (x.Season == Season.Undefined)
                {
                    return -1;
                }
                else if (x.Season > y.Season)
                {
                    return 1;
                }
                else if (x.Season < y.Season)
                {
                    return -1;
                }
            }

            if (x.Month == null && y.Month == null)
            {
                return 0;
            }
            else if (y.Month == null)
            {
                return 1;
            }
            else if (x.Month == null)
            {
                return -1;
            }
            else if (x.Month > y.Month)
            {
                return 1;
            }
            else if (x.Month < y.Month)
            {
                return -1;
            }

            if (x.Day == null && y.Day == null)
            {
                return 0;
            }
            else if (y.Day == null)
            {
                return 1;
            }
            else if (x.Day == null)
            {
                return -1;
            }
            else if (x.Day > y.Day)
            {
                return 1;
            }
            else if (x.Day < y.Day)
            {
                return -1;
            }

            if (x.Hour == null && y.Hour == null)
            {
                return 0;
            }
            else if (y.Hour == null)
            {
                return 1;
            }
            else if (x.Hour == null)
            {
                return -1;
            }
            else if (x.Hour > y.Hour)
            {
                return 1;
            }
            else if (x.Hour < y.Hour)
            {
                return -1;
            }

            if (x.Minute == null && y.Minute == null)
            {
                return 0;
            }
            else if (y.Minute == null)
            {
                return 1;
            }
            else if (x.Minute == null)
            {
                return -1;
            }
            else if (x.Minute > y.Minute)
            {
                return 1;
            }
            else if (x.Minute < y.Minute)
            {
                return -1;
            }

            if (x.Second == null && y.Second == null)
            {
                return 0;
            }
            else if (y.Second == null)
            {
                return 1;
            }
            else if (x.Second == null)
            {
                return -1;
            }
            else if (x.Second > y.Second)
            {
                return 1;
            }
            else if (x.Second < y.Second)
            {
                return -1;
            }

            return 0;
        }
    }
}