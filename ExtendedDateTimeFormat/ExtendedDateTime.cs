using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(ExtendedDateTimeConverter))]
    public class ExtendedDateTime : ISingleExtendedDateTimeType, ICloneable, IComparable, IComparable<ExtendedDateTime>, IEquatable<ExtendedDateTime>
    {
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { IsOpen = true };

        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { IsUnknown = true };

        private static IComparer<ExtendedDateTime> comparer;

        public static IComparer<ExtendedDateTime> Comparer
        {
            get
            {
                return comparer ?? (comparer = new ExtendedDateTimeComparer());
            }
        }

        public static ExtendedDateTime Now
        {
            get
            {
                return DateTime.Now.ToExtendedDateTime(new TimeZone(TimeZoneInfo.Local.BaseUtcOffset.Hours, TimeZoneInfo.Local.BaseUtcOffset.Minutes));
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

        public static bool operator <(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) < 0;
        }

        public static bool operator <=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) <= 0;
        }

        public static bool operator >(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) > 0;
        }

        public static bool operator >=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) >= 0;
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

        public int CompareTo(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            return Comparer.Compare(this, (ExtendedDateTime)obj);
        }

        public bool Equals(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }

        public ExtendedDateTime Earliest()
        {
            return this;
        }

        public ExtendedDateTime Latest()
        {
            return this;
        }
    }
}