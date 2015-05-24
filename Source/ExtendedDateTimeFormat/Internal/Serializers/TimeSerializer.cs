namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class TimeSerializer
    {
        internal static string Serialize(Time time, bool withTimeDesignator, bool withColons, bool withUtcOffset)
        {
            var timeDesignator = withTimeDesignator ? "T" : string.Empty;
            var colon = withColons ? ":" : string.Empty;
            var utcOffset = string.Empty;

            if (withUtcOffset)
            {
                if (time.UtcOffset == TimeSpan.Zero)
                {
                    utcOffset = "Z";
                }
                else
                {
                    var sign = time.UtcOffset.Hours > 0 ? "+" : string.Empty;
                    var utcOffsetHours = time.UtcOffset.Hours.ToString("D2");

                    if (time.UtcOffset.Minutes == 0)
                    {
                        utcOffset = string.Format("{0}{1:D2}", sign, utcOffsetHours);
                    }
                    else
                    {
                        var utcOffsetMinutes = Math.Abs(time.UtcOffset.Minutes).ToString("D2");
                        utcOffset = string.Format("{0}{1:D2}:{2:D2}", sign, utcOffsetHours, utcOffsetMinutes);
                    }
                }
            }

            var hour = time.Precision == TimePrecision.Hour && time.DecimalFraction != 0 ? string.Format("{0:D2}.{1}", time.Hour, time.DecimalFraction) : time.Hour.ToString("D2");

            if (time.Precision == TimePrecision.Hour)
            {
                return string.Format("{0}{1}{2}", timeDesignator, hour, utcOffset);
            }

            var minute = time.Precision == TimePrecision.Minute && time.DecimalFraction != 0 ? string.Format("{0:D2}.{1}", time.Minute, time.DecimalFraction) : time.Minute.ToString("D2");

            if (time.Precision == TimePrecision.Minute)
            {
                return string.Format("{0}{1}{2}{3}{4}", timeDesignator, hour, colon, minute, utcOffset);
            }

            var second = time.Precision == TimePrecision.Second && time.DecimalFraction != 0 ? string.Format("{0:D2}.{1}", time.Second, time.DecimalFraction) : time.Second.ToString("D2");

            return string.Format("{0}{1}{2}{3}{4}{5}{6}", timeDesignator, hour, colon, minute, colon, second, utcOffset);
        }
    }
}