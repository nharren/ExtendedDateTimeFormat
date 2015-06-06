using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;
using System.Globalization;

namespace System.ISO8601
{
    internal class CalendarDateTimeDuration : DateTimeDuration
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