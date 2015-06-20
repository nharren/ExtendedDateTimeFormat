using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class DesignatedDuration : Duration
    {
        private readonly double? _days;
        private readonly double? _hours;
        private readonly double? _minutes;
        private readonly double? _months;
        private readonly double? _seconds;
        private readonly double? _weeks;
        private readonly double? _years;

        public DesignatedDuration(int? years, int? months, int? days, int? hours, int? minutes, double? seconds)
        {
            if (years == null && months == null && days == null && hours == null && minutes == null && seconds == null)
            {
                throw new InvalidOperationException("A designated duration must have at least one defined component.");
            }

            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }

        public DesignatedDuration(int? years, int? months, int? days, int? hours, double minutes)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
        }

        public DesignatedDuration(int? years, int? months, int? days, double hours)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
        }

        public DesignatedDuration(int? years, int? months, double days)
        {
            _years = years;
            _months = months;
            _days = days;
        }

        public DesignatedDuration(int? years, double months)
        {
            _years = years;
            _months = months;
        }

        public DesignatedDuration(double years)
        {
            _years = years;
        }

        private DesignatedDuration(double? weeks)
        {
            _weeks = weeks;
        }

        public double? Days
        {
            get
            {
                return _days;
            }
        }

        public double? Hours
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

        public double? Months
        {
            get
            {
                return _months;
            }
        }

        public double? Seconds
        {
            get
            {
                return _seconds;
            }
        }

        public double? Weeks
        {
            get
            {
                return _weeks;
            }
        }

        public double? Years
        {
            get
            {
                return _years;
            }
        }

        public static DesignatedDuration FromWeeks(double weeks)
        {
            return new DesignatedDuration(weeks: weeks);
        }

        public static DesignatedDuration Parse(string input)
        {
            return DesignatedDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return DesignatedDurationSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _years.GetHashCode() ^ _months.GetHashCode() ^ _weeks.GetHashCode() ^ _days.GetHashCode() ^ _hours.GetHashCode() ^ _minutes.GetHashCode() ^ _seconds.GetHashCode();
        }
    }
}