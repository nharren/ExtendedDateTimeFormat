using System.ComponentModel;
using System.EDTF.Internal.Converters;
using System.EDTF.Internal.Parsers;
using System.EDTF.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.EDTF
{
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeConverter))]
    public class ExtendedDateTime : ISingleExtendedDateTimeType, IComparable, IComparable<ExtendedDateTime>, IEquatable<ExtendedDateTime>, ISerializable, IXmlSerializable, ICloneable
    {
        public static readonly ExtendedDateTime Maximum = new ExtendedDateTime(9999, 12, 31, 23, 59, 59, 14);
        public static readonly ExtendedDateTime Minimum = new ExtendedDateTime(-9999, 1, 1, 0, 0, 0, -12);
        public static readonly ExtendedDateTime Open = new ExtendedDateTime { _isOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime { _isUnknown = true };

        private static ExtendedDateTimeComparer _comparer;
        private int _day;
        private DayFlags _dayFlags;
        private int _hour;
        private bool _isLongYear;
        private bool _isOpen;
        private bool _isUnknown;
        private int _minute;
        private int _month;
        private MonthFlags _monthFlags;
        private ExtendedDateTimePrecision _precision;
        private Season _season;
        private SeasonFlags _seasonFlags;
        private string _seasonQualifier;
        private int _second;
        private TimeSpan _utcOffset;
        private int _year;
        private int? _yearExponent;
        private YearFlags _yearFlags;
        private int? _yearPrecision;

        public ExtendedDateTime(int year, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int utcHourOffset = 0, int utcMinuteOffset = 0)
        {
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, $"The argument \"{nameof(year)}\" must be a value from -9999 to 9999");
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, $"The argument \"{nameof(month)}\" must be a value from 1 to 12");
            }

            if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException(nameof(day), day, $"The argument \"{nameof(day)}\" must be a value from 1 to {ExtendedDateTimeCalculator.DaysInMonth(year, month)}");
            }

            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, $"The argument \"{nameof(hour)}\" must be a value from 0 to 23");
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), minute, $"The argument \"{nameof(minute)}\" must be a value from 0 to 59");
            }

            if (second < 0 || second > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second, $"The argument \"{nameof(second)}\" must be a value from 0 to 59");
            }

            _year = year;
            _month = month;
            _day = day;
            _hour = hour;
            _minute = minute;
            _second = second;
            _utcOffset = new TimeSpan(utcHourOffset, utcMinuteOffset, 0);
        }

        internal ExtendedDateTime()
        {
            _month = 1;
            _day = 1;
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
                if (_isOpen)
                {
                    return Now.Day;
                }

                return _day;
            }

            internal set
            {
                _day = value;
            }
        }

        public DayFlags DayFlags
        {
            get
            {
                return _dayFlags;
            }

            set
            {
                _dayFlags = value;
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                if (Precision > ExtendedDateTimePrecision.Day)
                {
                    throw new InvalidOperationException("Day of the Week can only be calculated when the precision is of the day or greater.");
                }

                return ExtendedDateTimeCalculator.DayOfWeek(this);
            }
        }

        public int Hour
        {
            get
            {
                if (_isOpen)
                {
                    return Now.Hour;
                }

                return _hour;
            }

            internal set
            {
                _hour = value;
            }
        }

        public bool IsLongYear
        {
            get
            {
                return _isLongYear;
            }

            set
            {
                _isLongYear = value;
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
                if (_isOpen)
                {
                    return Now.Minute;
                }

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
                if (_isOpen)
                {
                    return Now.Month;
                }

                return _month;
            }

            internal set
            {
                _month = value;
            }
        }

        public MonthFlags MonthFlags
        {
            get
            {
                return _monthFlags;
            }

            set
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

            set
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

        public SeasonFlags SeasonFlags
        {
            get
            {
                return _seasonFlags;
            }

            set
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
                if (_isOpen)
                {
                    return Now.Second;
                }

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
                if (_isOpen)
                {
                    return Now.Year;
                }

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

        public YearFlags YearFlags
        {
            get
            {
                return _yearFlags;
            }

            set
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
            return new ExtendedDateTime { _year = year, _isLongYear = true };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, int? exponent = null, int? precision = null)
        {
            if (significand == 0)
            {
                throw new ArgumentException("significand", "The significand must be nonzero.");
            }

            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent", "An exponent must be positive.");
            }

            if (precision < 1)
            {
                throw new ArgumentOutOfRangeException("precision", "A precision must be positive.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _yearPrecision = precision };
        }

        public static ExtendedDateTime FromSeason(int year, Season season, string seasonQualifier = null)
        {
            if (season == Season.Undefined)
            {
                throw new ArgumentException("A season cannot be input as undefined.");
            }

            return new ExtendedDateTime { _year = year, _season = season, _seasonQualifier = seasonQualifier };
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
                return new ExtendedDateTime();
            }

            return ExtendedDateTimeParser.Parse(extendedDateTimeString);
        }

        public ExtendedDateTime AddMonths(int count, DayExceedsDaysInMonthStrategy dayExceedsDaysInMonthStrategy = DayExceedsDaysInMonthStrategy.RoundDown)
        {
            return ExtendedDateTimeCalculator.AddMonths(this, count, dayExceedsDaysInMonthStrategy);
        }

        public ExtendedDateTime AddYears(int count)
        {
            return ExtendedDateTimeCalculator.AddYears(this, count);
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
                throw new ArgumentException("An extended datetime can only be compared with another extended datetime.");
            }

            return Comparer.Compare(this, (ExtendedDateTime)obj);
        }

        public ExtendedDateTime Earliest()
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(ExtendedDateTime))
            {
                return false;
            }

            return Comparer.Compare(this, (ExtendedDateTime)obj) == 0;
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
            info.AddValue("edtStr", ToString());
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

        public ExtendedDateTime SubtractMonths(int count)
        {
            return ExtendedDateTimeCalculator.SubtractMonths(this, count);
        }

        public ExtendedDateTime SubtractYears(int count)
        {
            return ExtendedDateTimeCalculator.SubtractYears(this, count);
        }

        public ExtendedDateTime ToRoundedPrecision(ExtendedDateTimePrecision p, bool roundUp = false)
        {
            return ExtendedDateTimeCalculator.ToRoundedPrecision(this, p, roundUp);
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(ToString());
        }

        internal static ExtendedDateTime Parse(string extendedDateTimeString, ExtendedDateTime container)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                return new ExtendedDateTime();
            }

            return ExtendedDateTimeParser.Parse(extendedDateTimeString, container);
        }
    }
}