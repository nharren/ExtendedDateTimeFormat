namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    public static class WeekDateSerializer
    {
        public static string ToString(WeekDate weekDate, bool hyphenate)
        {
            if (hyphenate)
            {
                if (weekDate.NumberOfAddedYearDigits > 0)
                {
                    if (weekDate.Precision == WeekDatePrecision.Week)
                    {
                        return string.Format("{0}{1}-W{2}", weekDate.Year < 0 ? string.Empty : "+", weekDate.Year, weekDate.Week);
                    }
                    else
                    {
                        return string.Format("{0}{1}-W{2}-{3}", weekDate.Year < 0 ? string.Empty : "+", weekDate.Year, weekDate.Week, weekDate.Day);
                    }
                }
                else
                {
                    if (weekDate.Precision == WeekDatePrecision.Week)
                    {
                        return string.Format("{0}-W{1}", weekDate.Year, weekDate.Week);
                    }
                    else
                    {
                        return string.Format("{0}-W{1}-{2}", weekDate.Year, weekDate.Week, weekDate.Day);
                    }
                }
            }
            else
            {
                if (weekDate.NumberOfAddedYearDigits > 0)
                {
                    if (weekDate.Precision == WeekDatePrecision.Week)
                    {
                        return string.Format("{0}{1}W{2}", weekDate.Year < 0 ? string.Empty : "+", weekDate.Year, weekDate.Week);
                    }
                    else
                    {
                        return string.Format("{0}{1}W{2}{3}", weekDate.Year < 0 ? string.Empty : "+", weekDate.Year, weekDate.Week, weekDate.Day);
                    }
                }
                else
                {
                    if (weekDate.Precision == WeekDatePrecision.Week)
                    {
                        return string.Format("{0}W{1}", weekDate.Year, weekDate.Week);
                    }
                    else
                    {
                        return string.Format("{0}W{1}{2}", weekDate.Year, weekDate.Week, weekDate.Day);
                    }
                }
            }
        }
    }
}