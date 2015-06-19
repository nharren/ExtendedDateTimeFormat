using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class OrdinalDate : Date
    {
        private readonly int _day;
        private readonly long _year;

        public OrdinalDate(long year, int day)
        {
            var daysInYear = ISO8601Calculator.DaysInYear(year);

            if (day < 1 || day > daysInYear)
            {
                throw new ArgumentOutOfRangeException("year", $"The day must be a value from 1 to {daysInYear}.");
            }

            _year = year;
            _day = day;
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
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return OrdinalDateSerializer.Serialize(this, options);
        }

        public WeekDate ToWeekDate(WeekDatePrecision precision)
        {
            return OrdinalDateConverter.ToWeekDate(this, precision);
        }

        public WeekDate ToWeekDate()
        {
            return OrdinalDateConverter.ToWeekDate(this, WeekDatePrecision.Day);
        }

        internal override int GetHashCodeOverride()
        {
            return unchecked((int)Year) ^ (Day << 22);
        }
    }
}