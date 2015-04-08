using System.Collections.Generic;

namespace System.ExtendedDateTimeFormat.Internal.Comparers
{
    public class ExtendedDateTimeComparer : IComparer<ExtendedDateTime>
    {
        public int Compare(ExtendedDateTime x, ExtendedDateTime y)
        {
            return Compare(x, y, false);
        }

        public int Compare(ExtendedDateTime x, ExtendedDateTime y, bool ignorePrecision)
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

            var a = (ExtendedDateTime)null;
            var b = (ExtendedDateTime)null;

            if (ignorePrecision)
            {
                var normalizedPrecisions = ExtendedDateTimeCalculator.NormalizePrecision(new ExtendedDateTime[] { x, y });

                a = normalizedPrecisions[0];
                b = normalizedPrecisions[1];
            }
            else
            {
                a = x;
                b = y;
            }

            long longXYear = a.Year;
            long longYYear = b.Year;

            if (a.YearExponent.HasValue)
            {
                try
                {
                    longXYear *= Convert.ToInt64(Math.Pow(10, a.YearExponent.Value));
                }
                catch (Exception)
                {
                    if (a.Year < 0)
                    {
                        longXYear = long.MinValue;
                    }
                    else
                    {
                        longXYear = long.MaxValue;
                    }
                }
            }

            if (b.YearExponent.HasValue)
            {
                try
                {
                    longYYear *= Convert.ToInt64(Math.Pow(10, b.YearExponent.Value));
                }
                catch (Exception)
                {
                    if (b.Year < 0)
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

            if (a.Season != Season.Undefined || b.Season != Season.Undefined)
            {
                if (b.Season == Season.Undefined)
                {
                    return 1;
                }
                else if (a.Season == Season.Undefined)
                {
                    return -1;
                }
                else if (a.Season > b.Season)
                {
                    return 1;
                }
                else if (a.Season < b.Season)
                {
                    return -1;
                }
            }

            if (a.Month == null && b.Month == null)
            {
                return 0;
            }
            else if (b.Month == null)
            {
                return 1;
            }
            else if (a.Month == null)
            {
                return -1;
            }
            else if (a.Month > b.Month)
            {
                return 1;
            }
            else if (a.Month < b.Month)
            {
                return -1;
            }

            if (a.Day == null && b.Day == null)
            {
                return 0;
            }
            else if (b.Day == null)
            {
                return 1;
            }
            else if (a.Day == null)
            {
                return -1;
            }
            else if (a.Day > b.Day)
            {
                return 1;
            }
            else if (a.Day < b.Day)
            {
                return -1;
            }

            if (a.Hour == null && b.Hour == null)
            {
                return 0;
            }
            else if (b.Hour == null)
            {
                return 1;
            }
            else if (a.Hour == null)
            {
                return -1;
            }
            else if (a.Hour > b.Hour)
            {
                return 1;
            }
            else if (a.Hour < b.Hour)
            {
                return -1;
            }

            if (a.Minute == null && b.Minute == null)
            {
                return 0;
            }
            else if (b.Minute == null)
            {
                return 1;
            }
            else if (a.Minute == null)
            {
                return -1;
            }
            else if (a.Minute > b.Minute)
            {
                return 1;
            }
            else if (a.Minute < b.Minute)
            {
                return -1;
            }

            if (a.Second == null && b.Second == null)
            {
                return 0;
            }
            else if (b.Second == null)
            {
                return 1;
            }
            else if (a.Second == null)
            {
                return -1;
            }
            else if (a.Second > b.Second)
            {
                return 1;
            }
            else if (a.Second < b.Second)
            {
                return -1;
            }

            return 0;
        }
    }
}