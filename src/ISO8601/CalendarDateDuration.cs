using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class CalendarDateDuration : DateDuration
    {
        private readonly int? _centuries;
        private readonly int? _days;
        private readonly int? _months;
        private readonly long _years;

        public CalendarDateDuration(long years, int months, int days) : this(years, months)
        {
            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            _days = days;
        }

        public CalendarDateDuration(long years, int months) : this(years)
        {
            if (months < 0 || months > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            _months = months;
        }

        public CalendarDateDuration(long years)
        {
            _years = years;
        }

        private CalendarDateDuration(int centuries)
        {
            _centuries = centuries;
        }

        public int? Centuries
        {
            get
            {
                return _centuries;
            }
        }

        public int? Days
        {
            get
            {
                return _days;
            }
        }

        public int? Months
        {
            get
            {
                return _months;
            }
        }

        public long Years
        {
            get
            {
                return _years;
            }
        }

        public static CalendarDateDuration FromCenturies(int centuries)
        {
            return new CalendarDateDuration(centuries);
        }

        public static CalendarDateDuration Parse(string input, int yearLength = 4)
        {
            return CalendarDateDurationParser.Parse(input, yearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return CalendarDateDurationSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _centuries.GetHashCode() ^ _years.GetHashCode() ^ _months.GetHashCode() ^ _days.GetHashCode();
        }
    }
}