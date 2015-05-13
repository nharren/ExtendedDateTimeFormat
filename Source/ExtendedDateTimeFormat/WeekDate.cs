using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class WeekDate : Date, IComparable, IComparable<Date>, IEquatable<Date>
    {
        private static DateComparer _comparer;
        private readonly int _addedYearLength;
        private readonly int _day;
        private readonly WeekDatePrecision _precision;
        private readonly int _week;
        private readonly long _year;

        public WeekDate(long year, int week, int day, int addedYearLength = 0) : this(year, week, addedYearLength)
        {
            if (day < 1 || day > 7)
            {
                throw new ArgumentOutOfRangeException("day", "The day must be a value from 1 to 7.");
            }

            _day = day;
            _precision = WeekDatePrecision.Day;
        }

        public WeekDate(long year, int week, int addedYearLength = 0)
        {
            if (year < 0 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year", "The year must be a value from 0 to 9999.");
            }

            int weeksInYear = DateCalculator.WeeksInYear(year);

            if (week < 1 || week > weeksInYear)
            {
                throw new ArgumentOutOfRangeException("week", string.Format("The week must be a value from 1 to {0}.", weeksInYear));
            }

            _year = year;
            _week = week;
            _addedYearLength = addedYearLength;
            _precision = WeekDatePrecision.Week;
        }

        public static DateComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new DateComparer();
                }

                return _comparer;
            }
        }

        public int AddedYearLength
        {
            get
            {
                return _addedYearLength;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }

        public WeekDatePrecision Precision
        {
            get
            {
                return _precision;
            }
        }

        public int Week
        {
            get
            {
                return _week;
            }
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public static bool operator !=(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(WeekDate x, Date y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static WeekDate Parse(string input, int addedYearLength)
        {
            return WeekDateParser.Parse(input, addedYearLength);
        }

        public int CompareTo(Date other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Date))
            {
                throw new ArgumentException("A calendar date can only be compared with other dates.");
            }

            return Comparer.Compare(this, (Date)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Date))
            {
                return false;
            }

            return Comparer.Compare(this, (Date)obj) == 0;
        }

        public bool Equals(Date other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return unchecked((int)Year) ^ (Week << 28) ^ (Day << 22);
        }

        public CalendarDate ToCalendarDate(CalendarDatePrecision precision)
        {
            return WeekDateConverter.ToCalendarDate(this, precision);
        }

        public OrdinalDate ToOrdinalDate()
        {
            return WeekDateConverter.ToOrdinalDate(this);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool hyphenate)
        {
            return WeekDateSerializer.Serialize(this, hyphenate);
        }
    }
}