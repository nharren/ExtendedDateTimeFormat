using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

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
        private int _fractionLength;

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

            var fractionParts = seconds.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public DesignatedDuration(int? years, int? months, int? days, int? hours, double minutes)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;

            var fractionParts = minutes.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public DesignatedDuration(int? years, int? months, int? days, double hours)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;

            var fractionParts = hours.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public DesignatedDuration(int? years, int? months, double days)
        {
            _years = years;
            _months = months;
            _days = days;

            var fractionParts = days.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public DesignatedDuration(int? years, double months)
        {
            _years = years;
            _months = months;

            var fractionParts = months.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public DesignatedDuration(double years)
        {
            _years = years;

            var fractionParts = years.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        private DesignatedDuration(double? weeks)
        {
            _weeks = weeks;

            var fractionParts = weeks.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public double? Days
        {
            get
            {
                return _days;
            }
        }

        public int FractionLength
        {
            get
            {
                return _fractionLength;
            }

            set
            {
                _fractionLength = value;
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

        public virtual string ToString(ISO8601FormatInfo formatInfo)
        {
            return DesignatedDurationSerializer.Serialize(this, formatInfo);
        }
    }
}