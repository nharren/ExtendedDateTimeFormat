using System.ISO8601.Abstract;
using System.ISO8601.Internal.Conversion;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

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

        public virtual string ToString(DateTimeFormatInfo formatInfo)
        {
            return OrdinalDateDurationSerializer.Serialize(this, formatInfo);
        }

        public CalendarDateDuration ToCalendarDateDuration()
        {
            return OrdinalDateDurationConverter.ToCalendarDateDuration(this);
        }
    }
}