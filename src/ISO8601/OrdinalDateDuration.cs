using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class OrdinalDateDuration : DateDuration
    {
        private readonly int _days;
        private readonly long _years;

        public OrdinalDateDuration(long years, int days)
        {
            _years = years;

            if (days < 0 || days > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(_days), "Days must be a number between 0 and 999.");
            }

            _days = days;
        }

        public int Days
        {
            get
            {
                return _days;
            }
        }

        public long Years
        {
            get
            {
                return _years;
            }
        }

        public static OrdinalDateDuration Parse(string input, int yearLength = 4)
        {
            return OrdinalDateDurationParser.Parse(input, yearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return OrdinalDateDurationSerializer.Serialize(this, options);
        }

        public CalendarDateDuration ToCalendarDateDuration()
        {
            return OrdinalDateDurationConverter.ToCalendarDateDuration(this);
        }

        internal override int GetHashCodeOverride()
        {
            return _years.GetHashCode() ^ _days.GetHashCode();
        }
    }
}