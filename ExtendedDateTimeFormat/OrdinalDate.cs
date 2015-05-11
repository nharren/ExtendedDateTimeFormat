using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class OrdinalDate : Date
    {
        private readonly int _addedYearLength;
        private readonly int _day;
        private readonly long _year;

        public OrdinalDate(long year, int day, int addedYearLength = 0)
        {
            var daysInYear = DateCalculator.DaysInYear(year);

            if (day < 1 || day > daysInYear)
            {
                throw new ArgumentOutOfRangeException("year", string.Format("The day must be a value from 1 to {0}.", daysInYear));
            }

            _year = year;
            _day = day;
            _addedYearLength = 0;
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

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public OrdinalDate Parse(string input, int addedYearLength = 0)
        {
            return OrdinalDateParser.Parse(input, addedYearLength);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool hyphenate)
        {
            return OrdinalDateSerializer.Serialize(this, hyphenate);
        }
    }
}