using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.ExtendedDateTimeFormat.Internal.Types;

namespace System.ExtendedDateTimeFormat
{
    public class CalendarDate : Date, IComparable, IComparable<Date>, IEquatable<Date>
    {
        private int _addedYearLength;
        private readonly int _century;
        private readonly int _day;
        private readonly int _month;
        private readonly CalendarDatePrecision _precision;
        private readonly long _year;
        private static DateComparer _comparer;

        public CalendarDate(long year, int month, int day) : this(year, month)
        {
            var daysInMonth = DateCalculator.DaysInMonth(year, month);

            if (day < 1 || day > daysInMonth)
            {
                throw new ArgumentOutOfRangeException("day", string.Format("The day must be a value from 1 to {0}.", daysInMonth));
            }

            _day = day;
            _precision = CalendarDatePrecision.Day;
        }

        public CalendarDate(long year, int month) : this(year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month", "The month must be a value from 1 to 12.");
            }

            _month = month;
            _precision = CalendarDatePrecision.Month;
        }

        public CalendarDate(int century)
        {
            _century = century;
            _precision = CalendarDatePrecision.Century;
        }

        public CalendarDate(long year)
        {
            if (year < 0 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year", "The year must be a value from 0 to 9999.");
            }

            _year = year;
            _century = DateCalculator.CenturyOfYear(year);
            _precision = CalendarDatePrecision.Year;
        }

        public int AddedYearLength
        {
            get
            {
                return _addedYearLength;
            }
            set
            {
                _addedYearLength = value;
            }
        }

        public int Century
        {
            get
            {
                return _century;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
        }

        public CalendarDatePrecision Precision
        {
            get
            {
                return _precision;
            }
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public static TimeSpan operator -(CalendarDate x, CalendarDate y)
        {
            return DateCalculator.Subtract(x, y);
        }

        public static bool operator !=(CalendarDate x, Date y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(CalendarDate x, Date y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(CalendarDate x, Date y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(CalendarDate x, Date y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(CalendarDate x, Date y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(CalendarDate x, Date y)
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
            return unchecked((int)Year) ^ (Month << 28) ^ (Day << 22);
        }

        public static CalendarDate Parse(string input, int addedYearLength = 0)
        {
            return CalendarDateParser.Parse(input, addedYearLength);
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

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool hyphenate)
        {
            return CalendarDateSerializer.Serialize(this, hyphenate);
        }

        public OrdinalDate ToOrdinalDate()
        {
            return CalendarDateConverter.ToOrdinalDate(this);
        }

        public WeekDate ToWeekDate(WeekDatePrecision precision)
        {
            return CalendarDateConverter.ToWeekDate(this, precision);
        }
    }
}