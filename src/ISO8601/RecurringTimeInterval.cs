using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class RecurringTimeInterval
    {
        private readonly TimeInterval _interval;
        private readonly int? _recurrences;

        public RecurringTimeInterval(TimeInterval interval, int? recurrences)
        {
            _interval = interval;
            _recurrences = recurrences;
        }

        public TimeInterval Interval
        {
            get
            {
                return _interval;
            }
        }

        public int? Recurrences
        {
            get
            {
                return _recurrences;
            }
        }

        public static RecurringTimeInterval Parse(string input, int startYearLength, int endYearLength)
        {
            return RecurringTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString();
        }

        public virtual string ToString(bool withTimeDesignator = true, bool withComponentSeparators = true, DecimalSeparator startDecimalSeparator = DecimalSeparator.Comma, bool withUtcOffset = true, bool isExpanded = false, int yearLength = 4, int fractionLength = 0, DecimalSeparator durationDecimalSeparator = DecimalSeparator.Comma)
        {
            return RecurringTimeIntervalSerializer.Serialize(this, withTimeDesignator, withComponentSeparators, startDecimalSeparator, withUtcOffset, isExpanded, yearLength, fractionLength, durationDecimalSeparator);
        }
    }
}