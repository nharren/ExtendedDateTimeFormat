using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;
using System.Globalization;

namespace System.ISO8601
{
    public class DesignatedStandardDuration : DesignatedDuration
    {
        private readonly double _days;
        private readonly double _hours;
        private readonly double _minutes;
        private readonly double _months;
        private readonly double _seconds;
        private readonly double _years;
        private int _fractionLength;
        private CalendarDateTimePrecision _precision;

        public DesignatedStandardDuration(int years, int months, int days, int hours, int minutes, double seconds)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
            _precision = CalendarDateTimePrecision.Second;

            var fractionParts = seconds.ToString().Split('.',',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public DesignatedStandardDuration(int years, int months, int days, int hours, double minutes)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _precision = CalendarDateTimePrecision.Minute;

            var fractionParts = minutes.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public DesignatedStandardDuration(int years, int months, int days, double hours)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _precision = CalendarDateTimePrecision.Hour;

            var fractionParts = hours.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public DesignatedStandardDuration(int years, int months, double days)
        {
            _years = years;
            _months = months;
            _days = days;
            _precision = CalendarDateTimePrecision.Day;

            var fractionParts = days.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public DesignatedStandardDuration(int years, double months)
        {
            _years = years;
            _months = months;
            _precision = CalendarDateTimePrecision.Month;

            var fractionParts = months.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public DesignatedStandardDuration(double years)
        {
            _years = years;
            _precision = CalendarDateTimePrecision.Year;

            var fractionParts = years.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
        }

        public double Days
        {
            get
            {
                return _days;
            }
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

        public double Months
        {
            get
            {
                return _months;
            }
        }

        public double Seconds
        {
            get
            {
                return _seconds;
            }
        }

        public double Years
        {
            get
            {
                return _years;
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

        public static DesignatedStandardDuration Parse(string input)
        {
            return DesignatedDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? DecimalSeparator.Dot : DecimalSeparator.Comma);
        }

        public virtual string ToString(DecimalSeparator decimalSeparator)
        {
            return DesignatedDurationSerializer.Serialize(this, decimalSeparator);
        }
    }
}