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
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { _isOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { _isUnknown = true };

        private static ExtendedDateTimeComparer _comparer;
        private int _day = 1;
        private ExtendedDateTimeFlags _dayFlags;
        private int _hour;
        private bool _isOpen;
        private bool _isUnknown;
        private int _minute;
        private int _month = 1;
        private ExtendedDateTimeFlags _monthFlags;
        private ExtendedDateTimePrecision _precision;
        private Season _season;
        private ExtendedDateTimeFlags _seasonFlags;
        private string _seasonQualifier;
        private int _second;
        private TimeSpan _utcOffset = TimeZoneInfo.Local.BaseUtcOffset;
        private int _year;
        private int? _yearExponent;
        private ExtendedDateTimeFlags _yearFlags;
        private int? _yearPrecision;

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, hour, minute, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, hour, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0) : this(year, month)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, ExtendedDateTimeFlags yearFlags) : this(year)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset) : this(year, month, day, hour, minute, utcOffset)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentException("second");
            }

            _second = second;
            _precision = ExtendedDateTimePrecision.Second;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset) : this(year, month, day, hour, utcOffset)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("minute");
            }

            _minute = minute;
            _precision = ExtendedDateTimePrecision.Minute;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset) : this(year, month, day)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("hour");
            }

            _hour = hour;

            if (utcOffset.Milliseconds != 0 || utcOffset.Seconds != 0 || utcOffset.Days != 0)
            {
                throw new ArgumentOutOfRangeException("utcOffset");
            }

            _utcOffset = utcOffset;
            _precision = ExtendedDateTimePrecision.Hour;
        }

        public ExtendedDateTime(int year, int month, int day) : this(year, month)
        {
            if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("day");
            }

            _day = day;
            _precision = ExtendedDateTimePrecision.Day;
        }

        public ExtendedDateTime(int year, int month) : this(year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month");
            }

            _month = month;
            _precision = ExtendedDateTimePrecision.Month;
        }

        public ExtendedDateTime(int year) : this()
        {
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            _year = year;
            _precision = ExtendedDateTimePrecision.Year;
        }

        internal ExtendedDateTime()                                     // Used for parsing; Internal to prevent an invalid state.
        {
        }

        protected ExtendedDateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Parse((string)info.GetValue("edtStr", typeof(string)), this);
        }

        public static ExtendedDateTimeComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new ExtendedDateTimeComparer();
                }

                return _comparer;
            }
        }

        public static ExtendedDateTime Now
        {
            get
            {
                return DateTimeOffset.Now.ToExtendedDateTime();
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }

            internal set
            {
                _day = value;
            }
        }

        public ExtendedDateTimeFlags DayFlags
        {
            get
            {
                return _dayFlags;
            }

            internal set
            {
                _dayFlags = value;
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }

            internal set
            {
                _hour = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }

            internal set
            {
                _isOpen = value;
            }
        }

        public bool IsUnknown
        {
            get
            {
                return _isUnknown;
            }

            internal set
            {
                _isUnknown = value;
            }
        }

        public int Minute
        {
            get
            {
                return _minute;
            }

            internal set
            {
                _minute = value;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }

            internal set
            {
                _month = value;
            }
        }

        public ExtendedDateTimeFlags MonthFlags
        {
            get
            {
                return _monthFlags;
            }

            internal set
            {
                _monthFlags = value;
            }
        }

        public ExtendedDateTimePrecision Precision
        {
            get
            {
                return _precision;
            }

            internal set
            {
                _precision = value;
            }
        }

        public Season Season
        {
            get
            {
                return _season;
            }

            internal set
            {
                _season = value;
            }
        }

        public ExtendedDateTimeFlags SeasonFlags
        {
            get
            {
                return _seasonFlags;
            }

            internal set
            {
                _seasonFlags = value;
            }
        }

        public string SeasonQualifier
        {
            get
            {
                return _seasonQualifier;
            }

            internal set
            {
                _seasonQualifier = value;
            }
        }

        public int Second
        {
            get
            {
                return _second;
            }

            internal set
            {
                _second = value;
            }
        }

        public TimeSpan UtcOffset
        {
            get
            {
                return _utcOffset;
            }

            internal set
            {
                _utcOffset = value;
            }
        }

        public int Year
        {
            get
            {
                return _year;
            }

            internal set
            {
                _year = value;
            }
        }

        public int? YearExponent
        {
            get
            {
                return _yearExponent;
            }

            internal set
            {
                _yearExponent = value;
            }
        }

        public ExtendedDateTimeFlags YearFlags
        {
            get
            {
                return _yearFlags;
            }

            internal set
            {
                _yearFlags = value;
            }
        }

        public int? YearPrecision
        {
            get
            {
                return _yearPrecision;
            }

            internal set
            {
                _yearPrecision = value;
            }
        }

        public static ExtendedDateTime FromLongYear(int year)
        {
            return new ExtendedDateTime { _year = year };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, int exponent, int precision)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent", "An exponent must be positive.");
            }

            if (precision < 1)
            {
                throw new ArgumentOutOfRangeException("precision", "A precision must be positive.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _yearPrecision = precision, _precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, int exponent)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent", "An exponent must be positive.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromSeason(int year, Season season, string seasonQualifier = null, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags seasonFlags = 0)
        {
            if (season == Season.Undefined)
            {
                throw new ArgumentException("A season cannot be input as undefined.");
            }

            return new ExtendedDateTime { _year = year, _yearFlags = yearFlags, _season = season, _seasonQualifier = seasonQualifier, _seasonFlags = seasonFlags, _precision = ExtendedDateTimePrecision.Year };
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

        public override int GetHashCode()
        {
            return Year ^ (Month << 28) ^ (Day << 22) ^ (Hour << 14) ^ (Minute << 8) ^ (Second << 6) ^ UtcOffset.GetHashCode();
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

        public ExtendedDateTime ToPrecision(ExtendedDateTimePrecision precision, bool roundUp)
        {
            return ExtendedDateTimeCalculator.ToPrecision(this, precision, roundUp);
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