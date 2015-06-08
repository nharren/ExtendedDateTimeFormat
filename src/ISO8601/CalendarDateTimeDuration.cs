using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;
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

        public static CalendarDateTimeDuration Parse(string input, int yearLength = 4, int fractionLength = 0)
        {
            return CalendarDateTimeDurationParser.Parse(input, yearLength, fractionLength);
        }

        public override string ToString()
        {
            return ToString(true, 0, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? DecimalSeparator.Dot : DecimalSeparator.Comma);
        }

        public virtual string ToString(bool withComponentSeparators, int fractionLength, DecimalSeparator decimalSeparator)
        {
            return CalendarDateTimeDurationSerializer.Serialize(this, withComponentSeparators, fractionLength, decimalSeparator);
        }
    }
}