using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class DurationEndTimeInterval : TimeInterval
    {
        private readonly Duration _duration;
        private readonly TimePoint _end;

        public DurationEndTimeInterval(Duration duration, TimePoint end)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            _duration = duration;
            _end = end;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public TimePoint End
        {
            get
            {
                return _end;
            }
        }

        public static DurationEndTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return DurationEndTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString();
        }

        public virtual string ToString(bool isExpanded = false, int yearLength = 4, int fractionLength = 0, DecimalSeparator durationDecimalSeparator = DecimalSeparator.Comma, bool withTimeDesignator = true, bool withComponentSeparators = true, DecimalSeparator endDecimalSeparator = DecimalSeparator.Comma, bool withUtcOffset = true)
        {
            return DurationEndTimeIntervalSerializer.Serialize(this, isExpanded, yearLength, fractionLength, durationDecimalSeparator, withTimeDesignator, withComponentSeparators, endDecimalSeparator, withUtcOffset);
        }
    }
}