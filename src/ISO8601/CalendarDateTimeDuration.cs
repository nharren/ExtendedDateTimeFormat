using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class CalendarDateTimeDuration : DateTimeDuration
    {
        private readonly CalendarDateDuration _dateDuration;
        private readonly TimeDuration _timeDuration;

        public CalendarDateTimeDuration(CalendarDateDuration dateDuration, TimeDuration timeDuration)
        {
            if (dateDuration == null)
            {
                throw new ArgumentNullException(nameof(dateDuration));
            }

            if (timeDuration == null)
            {
                throw new ArgumentNullException(nameof(timeDuration));
            }

            _dateDuration = dateDuration;
            _timeDuration = timeDuration;
        }

        public int Days
        {
            get
            {
                return _dateDuration.Days.Value;
            }
        }

        public double Hours
        {
            get
            {
                return _timeDuration.Hours;
            }
        }

        public double? Minutes
        {
            get
            {
                return _timeDuration.Minutes;
            }
        }

        public int Months
        {
            get
            {
                return _dateDuration.Months.Value;
            }
        }

        public double? Seconds
        {
            get
            {
                return _timeDuration.Seconds;
            }
        }

        public long Years
        {
            get
            {
                return _dateDuration.Years;
            }
        }

        internal CalendarDateDuration DateDuration
        {
            get
            {
                return _dateDuration;
            }
        }

        internal TimeDuration TimeDuration
        {
            get
            {
                return _timeDuration;
            }
        }

        public static CalendarDateTimeDuration Parse(string input, int yearLength = 4)
        {
            return CalendarDateTimeDurationParser.Parse(input, yearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return CalendarDateTimeDurationSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _dateDuration.GetHashCode() ^ _timeDuration.GetHashCode();
        }
    }
}