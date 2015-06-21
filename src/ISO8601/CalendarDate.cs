using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class CalendarDate : Date
    {
        private readonly int _day;
        private readonly int _month;
        private readonly long _year;
        private int _century;
        private CalendarDatePrecision _precision;

        public CalendarDate(long year, int month, int day) : this(year, month)
        {
            if (day < 1 || day > ISO8601Calculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("day", $"The day must be a value from 1 to {ISO8601Calculator.DaysInMonth(year, month)}.");
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

        public CalendarDate(long year)
        {
            _year = year;
            _century = ISO8601Calculator.CenturyOfYear(year);
            _precision = CalendarDatePrecision.Year;
        }

        private CalendarDate()
        {
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

            set
            {
                _precision = value;
            }
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public static CalendarDate FromCentury(int century)
        {
            return new CalendarDate
            {
                _century = century,
                _precision = CalendarDatePrecision.Century
            };
        }

        public static TimeSpan operator -(CalendarDate x, CalendarDate y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static CalendarDate Parse(string input, int yearLength = 4)
        {
            if (yearLength < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(yearLength), "The year length must be four or greater.");
            }

            return CalendarDateParser.Parse(input, yearLength);
        }

        public OrdinalDate ToOrdinalDate()
        {
            return CalendarDateConverter.ToOrdinalDate(this);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return CalendarDateSerializer.Serialize(this, options);
        }

        public WeekDate ToWeekDate(WeekDatePrecision precision)
        {
            return CalendarDateConverter.ToWeekDate(this, precision);
        }

        public WeekDate ToWeekDate()
        {
            return CalendarDateConverter.ToWeekDate(this, WeekDatePrecision.Day);
        }

        internal override int GetHashCodeOverride()
        {
            return unchecked((int)Year) ^ (Month << 28) ^ (Day << 22);
        }
    }
}