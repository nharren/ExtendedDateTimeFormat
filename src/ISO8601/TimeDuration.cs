using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class TimeDuration : Duration
    {
        private readonly double _hours;
        private readonly double? _minutes;
        private readonly double? _seconds;

        public TimeDuration(int hours, int minutes, double seconds) : this(hours, minutes)
        {
            if (seconds < 0 || seconds > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds must be a number between 0 and 60.");
            }

            _seconds = seconds;
        }

        public TimeDuration(int hours, double minutes) : this(hours)
        {
            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be a number between 0 and 60.");
            }

            _minutes = minutes;
        }

        public TimeDuration(double hours)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            _hours = hours;
        }

        public double Hours
        {
            get
            {
                return _hours;
            }
        }

        public double? Minutes
        {
            get
            {
                return _minutes;
            }
        }

        public double? Seconds
        {
            get
            {
                return _seconds;
            }
        }

        public static TimeDuration Parse(string input)
        {
            return TimeDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return TimeDurationSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _hours.GetHashCode() ^ _minutes.GetHashCode() ^ _seconds.GetHashCode();
        }
    }
}