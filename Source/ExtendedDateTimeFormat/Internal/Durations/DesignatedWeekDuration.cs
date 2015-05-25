using System.ExtendedDateTimeFormat.Internal.Abstract;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Globalization;

namespace System.ExtendedDateTimeFormat.Internal.Durations
{
    internal class DesignatedWeekDuration : Duration
    {
        private int _fractionLength;
        private readonly double _weeks;

        public DesignatedWeekDuration(double weeks)
        {
            _weeks = weeks;

            var fractionParts = weeks.ToString().Split('.', ',');

            _fractionLength = int.Parse(fractionParts[1]) == 0 ? 0 : fractionParts[1].Length;
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

        public double Weeks
        {
            get
            {
                return _weeks;
            }
        }

        public static DesignatedWeekDuration Parse(string input)
        {
            return DesignatedWeekDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? DecimalSeparator.Dot : DecimalSeparator.Comma);
        }

        public virtual string ToString(DecimalSeparator decimalSeparator)
        {
            return DesignatedWeekDurationSerializer.Serialize(this, decimalSeparator);
        }
    }
}