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

        public ExtendedDateTime(int year, int month, int day, int hours, int minutes, int seconds, TimeSpan utcOffset) : this(year, month, day, hours, minutes, utcOffset)
        {
            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentException("seconds");
            }
        }

        public ExtendedDateTime(int year, int month, int day, int hours, int minutes, TimeSpan utcOffset) : this(year, month, day, hours, utcOffset)
        {
            if (minutes < 0 || minutes > 59)
            {
                throw new ArgumentException("minutes");
            }
        }

        public ExtendedDateTime(int year, int month, int day, int hours, TimeSpan utcOffset) : this(year, month, day)
        {
            if (hours < 0 || hours > 59)
            {
                throw new ArgumentException("hours");
            }

            if (utcOffset.Ticks != 0 || utcOffset.Milliseconds != 0 || utcOffset.Seconds != 0 || utcOffset.Days != 0)
            {
                throw new ArgumentOutOfRangeException("utcOffset");
            }

            UtcOffset = utcOffset;
        }

        public ExtendedDateTime(int year, int month, int day) : this(year, month)
        {
            if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("day");
            }

            Day = day;
        }

        public ExtendedDateTime(int year, int month) : this(year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month");
            }

            Month = month;
        }

        public ExtendedDateTime(int year)
        {
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            Year = year;
        }

        public ExtendedDateTime()
        {

        }

        public static IComparer<ExtendedDateTime> Comparer
        {
            get
            {
                if (comparer == null)
                {
                    comparer = new ExtendedDateTimeComparer();
                }

                return comparer;
            }
        }

        public static ExtendedDateTime Now
        {
            get
            {
                return DateTimeOffset.Now.ToExtendedDateTime();
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

        public TimeSpan? UtcOffset { get; set; }

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
            return (ExtendedDateTime)MemberwiseClone();;
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