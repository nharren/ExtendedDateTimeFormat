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
        public static readonly ExtendedDateTime Maximum = new ExtendedDateTime(9999, 12, 31, 23, 59, 59, TimeSpan.FromHours(14));
        public static readonly ExtendedDateTime Minimum = new ExtendedDateTime(-9999, 1, 1, 0, 0, 0, TimeSpan.FromHours(-12));
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { _isOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { _isUnknown = true };

        private static ExtendedDateTimeComparer _comparer;
        private int _day;
        private DayFlags _dayFlags;
        private int _hour;
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

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, second)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour, minute, second)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, DayFlags dayFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, DayFlags dayFlags) : this(year, month, day, hour, minute, second)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, MonthFlags monthFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, MonthFlags monthFlags) : this(year, month, day, hour, minute, second)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, TimeSpan utcOffset, YearFlags yearFlags) : this(year, month, day, hour, minute, second, utcOffset)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second, YearFlags yearFlags) : this(year, month, day, hour, minute, second)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, minute)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour, minute)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour, minute)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, DayFlags dayFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, DayFlags dayFlags) : this(year, month, day, hour, minute)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, MonthFlags monthFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, MonthFlags monthFlags) : this(year, month, day, hour, minute)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, TimeSpan utcOffset, YearFlags yearFlags) : this(year, month, day, hour, minute, utcOffset)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, YearFlags yearFlags) : this(year, month, day, hour, minute)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour, utcOffset)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day, hour)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour, utcOffset)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day, hour)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour, utcOffset)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day, hour)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, DayFlags dayFlags) : this(year, month, day, hour, utcOffset)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, DayFlags dayFlags) : this(year, month, day, hour)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, MonthFlags monthFlags) : this(year, month, day, hour, utcOffset)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, MonthFlags monthFlags) : this(year, month, day, hour)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset, YearFlags yearFlags) : this(year, month, day, hour, utcOffset)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, YearFlags yearFlags) : this(year, month, day, hour)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, YearFlags yearFlags, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, MonthFlags monthFlags, DayFlags dayFlags) : this(year, month, day)
        {
            _dayFlags = dayFlags;
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, YearFlags yearFlags, DayFlags dayFlags) : this(year, month, day)
        {
            _dayFlags = dayFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month, day)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, int day, DayFlags dayFlags) : this(year, month, day)
        {
            _dayFlags = dayFlags;
        }

        public ExtendedDateTime(int year, int month, int day, MonthFlags monthFlags) : this(year, month, day)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, int day, YearFlags yearFlags) : this(year, month, day)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, YearFlags yearFlags, MonthFlags monthFlags) : this(year, month)
        {
            _monthFlags = monthFlags;
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, int month, MonthFlags monthFlags) : this(year, month)
        {
            _monthFlags = monthFlags;
        }

        public ExtendedDateTime(int year, int month, YearFlags yearFlags) : this(year, month)
        {
            _yearFlags = yearFlags;
        }

        public ExtendedDateTime(int year, YearFlags yearFlags) : this(year)
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

        public ExtendedDateTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute)
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

        public ExtendedDateTime(int year, int month, int day, int hour, int minute) : this(year, month, day, hour)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("minute");
            }

            _minute = minute;
            _precision = ExtendedDateTimePrecision.Minute;
        }

        public ExtendedDateTime(int year, int month, int day, int hour, TimeSpan utcOffset) : this(year, month, day, hour)
        {
            if (utcOffset.Milliseconds != 0 || utcOffset.Seconds != 0 || utcOffset.Days != 0)
            {
                throw new ArgumentOutOfRangeException("utcOffset");
            }

            _utcOffset = utcOffset;
        }

        public ExtendedDateTime(int year, int month, int day, int hour) : this(year, month, day)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("hour");
            }

            _hour = hour;
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

        internal ExtendedDateTime()
        {
            _year = 0;
            _month = 1;
            _day = 1;
            _hour = 0;
            _minute = 0;
            _second = 0;
            _season = Season.Undefined;
            _yearFlags = 0;
            _monthFlags = 0;
            _dayFlags = 0;
            _seasonFlags = 0;
            _seasonQualifier = null;
            _isOpen = false;
            _isUnknown = false;
            _precision = ExtendedDateTimePrecision.Year;
            _yearExponent = null;
            _yearPrecision = null;
            _utcOffset = TimeZoneInfo.Local.BaseUtcOffset;
        }

        protected ExtendedDateTime(SerializationInfo info, StreamingContext context) : this()
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

        public DayFlags DayFlags
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

        public MonthFlags MonthFlags
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

        public SeasonFlags SeasonFlags
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

        public YearFlags YearFlags
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

            if (significand == 0)
            {
                throw new ArgumentException("significand", "The significand must be nonzero.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _yearPrecision = precision, _precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromScientificNotation(int significand, int exponent)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException("exponent", "An exponent must be positive.");
            }

            if (significand == 0)
            {
                throw new ArgumentException("significand", "The significand must be nonzero.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromSeason(int year, Season season, YearFlags yearFlags, SeasonFlags seasonFlags, string seasonQualifier)
        {
            if (season == Season.Undefined)
            {
                throw new ArgumentException("A season cannot be input as undefined.");
            }

            return new ExtendedDateTime { _year = year, _yearFlags = yearFlags, _season = season, _seasonQualifier = seasonQualifier, _seasonFlags = seasonFlags, _precision = ExtendedDateTimePrecision.Year };
        }

        public static ExtendedDateTime FromSeason(int year, Season season, SeasonFlags seasonFlags, string seasonQualifier)
        {
            return FromSeason(year, season, 0, seasonFlags, seasonQualifier);
        }

        public static ExtendedDateTime FromSeason(int year, Season season, YearFlags yearFlags, string seasonQualifier)
        {
            return FromSeason(year, season, yearFlags, 0, seasonQualifier);
        }

        public static ExtendedDateTime FromSeason(int year, Season season, string seasonQualifier)
        {
            return FromSeason(year, season, 0, 0, seasonQualifier);
        }

        public static ExtendedDateTime FromSeason(int year, Season season, YearFlags yearFlags, SeasonFlags seasonFlags)
        {
            return FromSeason(year, season, yearFlags, seasonFlags, null);
        }

        public static ExtendedDateTime FromSeason(int year, Season season, SeasonFlags seasonFlags)
        {
            return FromSeason(year, season, 0, seasonFlags, null);
        }

        public static ExtendedDateTime FromSeason(int year, Season season, YearFlags yearFlags)
        {
            return FromSeason(year, season, yearFlags, 0, null);
        }

        public static ExtendedDateTime FromSeason(int year, Season season)
        {
            return FromSeason(year, season, 0, 0, null);
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

        public ExtendedDateTime AddMonths(int count)
        {
            return ExtendedDateTimeCalculator.AddMonths(this, count);
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