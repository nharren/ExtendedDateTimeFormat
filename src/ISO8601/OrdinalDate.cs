using System.ISO8601.Abstract;
using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class OrdinalDate : Date, IComparable, IComparable<Date>, IEquatable<Date>
    {
        private static DateComparer _comparer;
        private readonly int _day;
        private readonly long _year;
        private int _yearLength;
        private bool _isExpanded;

        public OrdinalDate(long year, int day)
        {
            if (year < 0 || year > 9999)
            {
                _isExpanded = true;
            }

            var daysInYear = DateCalculator.DaysInYear(year);

            if (day < 1 || day > daysInYear)
            {
                throw new ArgumentOutOfRangeException("year", string.Format("The day must be a value from 1 to {0}.", daysInYear));
            }

            _year = year;
            _day = day;
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

        public int YearLength
        {
            get
            {
                return _yearLength;
            }

            set
            {
                _yearLength = value;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
            }
        }

        public static bool operator !=(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(OrdinalDate x, Date y)
        {
            return Comparer.Compare(x, y) >= 0;
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
            return unchecked((int)Year) ^ (Day << 22);
        }

        public static OrdinalDate Parse(string input, int yearLength = 4)
        {
            return OrdinalDateParser.Parse(input, yearLength);
        }

        public CalendarDate ToCalendarDate(CalendarDatePrecision precision)
        {
            return OrdinalDateConverter.ToCalendarDate(this, precision);
        }

        public CalendarDate ToCalendarDate()
        {
            return OrdinalDateConverter.ToCalendarDate(this, CalendarDatePrecision.Day);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool hyphenate)
        {
            return OrdinalDateSerializer.Serialize(this, hyphenate);
        }

        public WeekDate ToWeekDate(WeekDatePrecision precision)
        {
            return OrdinalDateConverter.ToWeekDate(this, precision);
        }

        public WeekDate ToWeekDate()
        {
            return OrdinalDateConverter.ToWeekDate(this, WeekDatePrecision.Day);
        }
    }
}