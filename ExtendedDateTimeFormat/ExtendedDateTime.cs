using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTime : ISingleExtendedDateTimeType, ICloneable
    {
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { IsOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { IsUnknown = true };

        public static ExtendedDateTime Now
        {
            get
            {
                return new ExtendedDateTime
                {
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Hour = DateTime.Now.Hour,
                    Minute = DateTime.Now.Minute,
                    Second = DateTime.Now.Second,
                    TimeZone = new TimeZone
                    {
                        HourOffset = TimeZoneInfo.Local.BaseUtcOffset.Hours,
                        MinuteOffset = TimeZoneInfo.Local.BaseUtcOffset.Minutes
                    }
                };
            }
        }

        public int? Day { get; set; }

        public ExtendedDateTimeFlags DayFlags { get; set; }

        public int? Hour { get; set; }

        public bool IsOpen { get; internal set; }

        public bool IsUnknown { get; internal set; }

        public int? Minute { get; set; }

        public int? Month { get; set; }

        public ExtendedDateTimeFlags MonthFlags { get; set; }

        public Season Season { get; set; }

        public ExtendedDateTimeFlags SeasonFlags { get; set; }

        public string SeasonQualifier { get; set; }

        public int? Second { get; set; }

        public TimeZone TimeZone { get; set; }

        public int? Year { get; set; }

        public int? YearExponent { get; set; }

        public ExtendedDateTimeFlags YearFlags { get; set; }

        public int? YearPrecision { get; set; }

        public static ExtendedDateTime operator -(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Subtract(e, t);
        }

        public static TimeSpan operator -(ExtendedDateTime e2, ExtendedDateTime e1)
        {
            return ExtendedDateTimeCalculator.Subtract(e2, e1);
        }

        public static ExtendedDateTime operator +(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Add(e, t);
        }

        public object Clone()
        {
            var clone = (ExtendedDateTime)MemberwiseClone();

            if (TimeZone != null)
            {
                clone.TimeZone = new TimeZone { HourOffset = TimeZone.HourOffset, MinuteOffset = TimeZone.MinuteOffset };
            }

            return clone;
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize<ExtendedDateTime>(this);
        }
    }
}