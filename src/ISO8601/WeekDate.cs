using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class WeekDate : Date
    {
        private readonly int _day;
        private readonly int _week;
        private readonly long _year;
        private WeekDatePrecision _precision;

        public WeekDate(long year, int week, int day) : this(year, week)
        {
            if (day < 1 || day > 7)
            {
                throw new ArgumentOutOfRangeException("day", "The day must be a value from 1 to 7.");
            }

            _day = day;
            _precision = WeekDatePrecision.Day;
        }

        public WeekDate(long year, int week)
        {
            int weeksInYear = ISO8601Calculator.WeeksInYear(year);

            if (week < 1 || week > weeksInYear)
            {
                throw new ArgumentOutOfRangeException("week", string.Format("The week must be a value from 1 to {0}.", weeksInYear));
            }

            _year = year;
            _week = week;
            _precision = WeekDatePrecision.Week;
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

            set
            {
                _precision = value;
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

        public static WeekDate Parse(string input, int yearLength = 4)
        {
            return WeekDateParser.Parse(input, yearLength);
        }

        public CalendarDate ToCalendarDate(CalendarDatePrecision precision)
        {
            return WeekDateConverter.ToCalendarDate(this, precision);
        }

        public CalendarDate ToCalendarDate()
        {
            return WeekDateConverter.ToCalendarDate(this, CalendarDatePrecision.Day);
        }

        public OrdinalDate ToOrdinalDate()
        {
            return WeekDateConverter.ToOrdinalDate(this);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return WeekDateSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _year.GetHashCode() ^ (_week << 28) ^ (_day << 22);
        }
    }
}