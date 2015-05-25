using System.ExtendedDateTimeFormat.Internal.Abstract;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Globalization;

namespace System.ExtendedDateTimeFormat.Internal.Durations
{
    internal class CalendarDateTimeDuration : DateTimeDuration
    {
        private readonly CalendarDateDuration _dateDuration;
        private readonly TimeDuration _timeDuration;

        public CalendarDateTimeDuration(int years, int months, int days, int hours, int minutes, double seconds)
        {
            if (years < 0 || years >= 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "Years must be a number between 0 and 9999.");
            }

            if (months < 0 || months > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be a number between 0 and 60.");
            }

            if (seconds < 0 || seconds > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds must be a number between 0 and 60.");
            }

            _dateDuration = new CalendarDateDuration(years, months, days);
            _timeDuration = new TimeDuration(hours, minutes, seconds);
        }

        public CalendarDateTimeDuration(int years, int months, int days, int hours, double minutes)
        {
            if (years < 0 || years >= 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "Years must be a number between 0 and 9999.");
            }

            if (months < 0 || months > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be a number between 0 and 60.");
            }

            _dateDuration = new CalendarDateDuration(years, months, days);
            _timeDuration = new TimeDuration(hours, minutes);
        }

        public CalendarDateTimeDuration(int years, int months, int days, double hours)
        {
            if (years < 0 || years >= 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "Years must be a number between 0 and 9999.");
            }

            if (months < 0 || months > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Hours must be a number between 0 and 24.");
            }

            _dateDuration = new CalendarDateDuration(years, months, days);
            _timeDuration = new TimeDuration(hours);
        }

        internal CalendarDateTimeDuration(CalendarDateDuration dateDuration, TimeDuration timeDuration)
        {
            _dateDuration = dateDuration;
            _timeDuration = timeDuration;
        }

        public double Days
        {
            get
            {
                return _dateDuration.Days;
            }
        }

        public int FractionLength
        {
            get
            {
                return _timeDuration.FractionLength;
            }

            set
            {
                _timeDuration.FractionLength = value;
            }
        }

        public double Hours
        {
            get
            {
                return _timeDuration.Hours;
            }
        }

        public double Minutes
        {
            get
            {
                return _timeDuration.Minutes;
            }
        }

        public double Months
        {
            get
            {
                return _dateDuration.Months;
            }
        }

        public double Seconds
        {
            get
            {
                return _timeDuration.Seconds;
            }
        }

        public double Years
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

        public static CalendarDateTimeDuration Parse(string input, int fractionLength = 0)
        {
            return CalendarDateTimeDurationParser.Parse(input, fractionLength);
        }

        public override string ToString()
        {
            return ToString(true, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? DecimalSeparator.Dot : DecimalSeparator.Comma);
        }

        public virtual string ToString(bool withComponentSeparators, DecimalSeparator decimalSeparator)
        {
            return CalendarDateTimeDurationSerializer.Serialize(this, withComponentSeparators, decimalSeparator);
        }
    }
}