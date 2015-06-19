using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class StartEndTimeInterval : TimeInterval
    {
        private readonly TimePoint _end;
        private readonly TimePoint _start;

        public StartEndTimeInterval(TimePoint start, TimePoint end)
        {
            _start = start;
            _end = end;
        }

        public TimePoint End
        {
            get
            {
                return _end;
            }
        }

        public TimePoint Start
        {
            get
            {
                return _start;
            }
        }

        public static StartEndTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return StartEndTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        internal override int GetHashCodeOverride()
        {
            return _start.GetHashCode() ^ _end.GetHashCode();
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return ToString(options, options);
        }

        public virtual string ToString(ISO8601Options startOptions, ISO8601Options endOptions)
        {
            return StartEndTimeIntervalSerializer.Serialize(this, startOptions, endOptions);
        }

        public override TimeSpan ToTimeSpan()
        {
            return StartEndTimeIntervalConverter.ToTimeSpan(this);
        }
    }
}