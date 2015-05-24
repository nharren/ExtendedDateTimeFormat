using System.ExtendedDateTimeFormat.Abstract;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class OrdinalDateDuration : DateDuration
    {
        private readonly int _days;
        private readonly int _years;

        public OrdinalDateDuration(int years, int days)
        {
            if (years < 0 || years > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "Years must be a number between 0 and 9999.");
            }

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

        public int Years
        {
            get
            {
                return _years;
            }
        }

        public static OrdinalDateDuration Parse(string input)
        {
            return OrdinalDateDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool withComponentSeparators)
        {
            return OrdinalDateDurationSerializer.Serialize(this, withComponentSeparators);
        }
    }
}