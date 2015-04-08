using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ExtendedDateTimeFormat
{
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeConverter))]
    public class ExtendedDateTime : ISingleExtendedDateTimeType, ICloneable, IComparable, IComparable<ExtendedDateTime>, IEquatable<ExtendedDateTime>, ISerializable, IXmlSerializable
    {
        public static readonly ExtendedDateTime Maximum = new ExtendedDateTime(9999, 12, 31, 23, 59, 59, TimeSpan.FromHours(14));

        public static readonly ExtendedDateTime Minimum = new ExtendedDateTime(-9999, 1, 1, 0, 0, 0, TimeSpan.FromHours(-12));

        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { IsOpen = true };

        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { IsUnknown = true };

        private static ExtendedDateTimeComparer comparer;

        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, byte second, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0)
            : this(year, month, day, hour, minute, second, utcOffset)
        {
            DayFlags = dayFlags;
            MonthFlags = monthFlags;
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0)
            : this(year, month, day, hour, minute, utcOffset)
        {
            DayFlags = dayFlags;
            MonthFlags = monthFlags;
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, byte month, byte day, byte hour, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0)
            : this(year, month, day, hour, utcOffset)
        {
            DayFlags = dayFlags;
            MonthFlags = monthFlags;
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, byte month, byte day, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0)
            : this(year, month, day)
        {
            DayFlags = dayFlags;
            MonthFlags = monthFlags;
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, byte month, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0)
            : this(year, month)
        {
            MonthFlags = monthFlags;
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, ExtendedDateTimeFlags yearFlags)
            : this(year)
        {
            YearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, byte second, TimeSpan utcOffset)
            : this(year, month, day, hour, minute, utcOffset)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentException("second");
            }

            Second = second;
            Precision = ExtendedDateTimePrecision.Second;
        }

        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, TimeSpan utcOffset)
            : this(year, month, day, hour, utcOffset)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("minute");
            }

            Minute = minute;
            Precision = ExtendedDateTimePrecision.Minute;
        }

        public ExtendedDateTime(int year, byte month, byte day, byte hour, TimeSpan utcOffset)
            : this(year, month, day)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("hour");
            }

            Hour = hour;

            if (utcOffset.Milliseconds != 0 || utcOffset.Seconds != 0 || utcOffset.Days != 0)
            {
                throw new ArgumentOutOfRangeException("utcOffset");
            }

            UtcOffset = utcOffset;
            Precision = ExtendedDateTimePrecision.Hour;
        }

        public ExtendedDateTime(int year, byte month, byte day)
            : this(year, month)
        {
            if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("day");
            }

            Day = day;
            Precision = ExtendedDateTimePrecision.Day;
        }

        public ExtendedDateTime(int year, byte month)
            : this(year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month");
            }

            Month = month;
            Precision = ExtendedDateTimePrecision.Month;
        }

        public ExtendedDateTime(int year)
            : this()
        {
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            Year = year;
            Precision = ExtendedDateTimePrecision.Year;
        }

        internal ExtendedDateTime()                                     // Used for parsing; Internal to prevent an invalid state.
        {
        }

        protected ExtendedDateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            Parse((string)info.GetValue("edtStr", typeof(string)), this);
        }

        public static ExtendedDateTimeComparer Comparer
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

        public byte? Day { get; internal set; }

        public ExtendedDateTimeFlags DayFlags { get; internal set; }

        public byte? Hour { get; internal set; }

        public bool IsOpen { get; internal set; }

        public bool IsUnknown { get; internal set; }

        public byte? Minute { get; internal set; }

        public byte? Month { get; internal set; }

        public ExtendedDateTimeFlags MonthFlags { get; internal set; }

        public ExtendedDateTimePrecision Precision { get; internal set; }

        public Season Season { get; internal set; }

        public ExtendedDateTimeFlags SeasonFlags { get; internal set; }

        public string SeasonQualifier { get; internal set; }

        public byte? Second { get; internal set; }

        public TimeSpan? UtcOffset { get; internal set; }

        public int Year { get; internal set; }

        public int? YearExponent { get; internal set; }

        public ExtendedDateTimeFlags YearFlags { get; internal set; }

        public int? YearPrecision { get; internal set; }

        public static ExtendedDateTime FromLongYear(int year)
        {
            return new ExtendedDateTime { Year = year };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, byte exponent, byte precision)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent", "An exponent must be positive.");
            }

            if (precision < 1)
            {
                throw new ArgumentOutOfRangeException("precision", "A precision must be positive.");
            }

            return new ExtendedDateTime { Year = significand, YearExponent = exponent, YearPrecision = precision, Precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, byte exponent)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent","An exponent must be positive.");
            }

            return new ExtendedDateTime { Year = significand, YearExponent = exponent, Precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromSeason(int year, Season season, string seasonQualifier = null, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags seasonFlags = 0)
        {
            if (season == Season.Undefined)
            {
                throw new ArgumentException("A season cannot be input as undefined.");
            }

            return new ExtendedDateTime { Year = year, YearFlags = yearFlags, Season = season, SeasonQualifier = seasonQualifier, SeasonFlags = seasonFlags, Precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime operator -(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Subtract(e, t);
        }

        public static TimeSpan operator -(ExtendedDateTime e2, ExtendedDateTime e1)
        {
            return ExtendedDateTimeCalculator.Subtract(e2, e1);
        }

        public static bool operator !=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) != 0;
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

        public static bool operator ==(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) == 0;
        }

        public static bool operator >(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) > 0;
        }

        public static bool operator >=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) >= 0;
        }

        public static ExtendedDateTime Parse(string extendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                return null;
            }

            return ExtendedDateTimeParser.Parse(extendedDateTimeString);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is ExtendedDateTime))
            {
                throw new ArgumentException("An extended datetime can only be compared with another extended datetime");
            }

            return Comparer.Compare(this, (ExtendedDateTime)obj);
        }

        public ExtendedDateTime Earliest()
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var extendedDateTime = obj as ExtendedDateTime;

            if (extendedDateTime == null)
            {
                return false;
            }

            return Comparer.Compare(this, extendedDateTime) == 0;
        }

        public bool Equals(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public bool Equals(ExtendedDateTime other, bool ignorePrecision)
        {
            return Comparer.Compare(this, other, ignorePrecision) == 0;
        }

        public override int GetHashCode()
        {
            int hash = Year;        // Year maximum = 32 bits.

            if (Month.HasValue)     // Month maximum = 4 bits.
            {
                hash ^= (Month.Value << 28);
            }

            if (Day.HasValue)       // Day maximum = 5 bits.
            {
                hash ^= (Day.Value << 22);
            }

            if (Hour.HasValue)      // Hour maximum = 6 bits.
            {
                hash ^= (Hour.Value << 14);
            }

            if (Minute.HasValue)    // Minute maximum = 6 bits.
            {
                hash ^= (Minute.Value << 8);
            }

            if (Second.HasValue)    // Hour maximum = 6 bits.
            {
                hash ^= (Second.Value << 6);
            }

            if (UtcOffset != null)
            {
                hash ^= UtcOffset.Value.GetHashCode();
            }

            return hash;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtStr", this.ToString());
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public ExtendedDateTime Latest()
        {
            return this;
        }

        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        internal static ExtendedDateTime Parse(string extendedDateTimeString, ExtendedDateTime container)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                return null;
            }

            return ExtendedDateTimeParser.Parse(extendedDateTimeString, container);
        }
    }
}