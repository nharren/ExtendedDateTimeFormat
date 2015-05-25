using System.ExtendedDateTimeFormat.Internal.Abstract;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Globalization;

namespace System.ExtendedDateTimeFormat.Internal.Durations
{
    internal class TimeDuration : Duration
    {
        private readonly double _hours;
        private readonly double _minutes;
        private readonly double _seconds;
        private int _fractionLength;

        public TimeDuration(int hours, int minutes, double seconds)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            _hours = hours;

            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be a number between 0 and 60.");
            }

            _minutes = minutes;

            if (seconds < 0 || seconds > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds must be a number between 0 and 60.");
            }

            _seconds = seconds;

            var fractionParts = seconds.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public TimeDuration(int hours, double minutes)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            _hours = hours;

            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be a number between 0 and 60.");
            }

            _minutes = minutes;

            var fractionParts = minutes.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public TimeDuration(double hours)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            _hours = hours;

            var fractionParts = hours.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public double Hours
        {
            get
            {
                return _hours;
            }
        }

        public double Minutes
        {
            get
            {
                return _minutes;
            }
        }

        public double Seconds
        {
            get
            {
                return _seconds;
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

        public static TimeDuration Parse(string input, int fractionLength = 0)
        {
            return TimeDurationParser.Parse(input, fractionLength);
        }

        public override string ToString()
        {
            return ToString(true, true, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? DecimalSeparator.Dot : DecimalSeparator.Comma);
        }

        public virtual string ToString(bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator decimalSeparator)
        {
            return TimeDurationSerializer.Serialize(this, withTimeDesignator, withComponentSeparators, decimalSeparator);
        }
    }
}