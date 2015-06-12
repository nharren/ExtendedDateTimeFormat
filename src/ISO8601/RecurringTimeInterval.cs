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

        public static RecurringTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return RecurringTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public virtual string ToString(ISO8601FormatInfo formatInfo)
        {
            return ToString(formatInfo, formatInfo);
        }

        public virtual string ToString(ISO8601FormatInfo leftFormatInfo, ISO8601FormatInfo rightFormatInfo)
        {
            return RecurringTimeIntervalSerializer.Serialize(this, leftFormatInfo, rightFormatInfo);
        }
    }
}