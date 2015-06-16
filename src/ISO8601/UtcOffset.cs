namespace System.ISO8601
{
    public struct UtcOffset
    {
        private static readonly UtcOffset _unsetUtcOffset = new UtcOffset(true, 0, 0, UtcPrecision.Hour);
        private readonly int _hours;
        private readonly bool _isUnset;
        private readonly int _minutes;
        private readonly UtcPrecision _precision;

        public UtcOffset(int hours) : this(false, hours, 0, UtcPrecision.Hour)
        {
        }

        public UtcOffset(int hours, int minutes) : this(false, hours, minutes, UtcPrecision.Minute)
        {
        }

        private UtcOffset(bool isUnset, int hours, int minutes, UtcPrecision precision)
        {
            _isUnset = isUnset;
            _hours = hours;
            _precision = precision;
            _minutes = minutes;
        }

        public static UtcOffset Unset
        {
            get
            {
                return _unsetUtcOffset;
            }
        }

        public int Hours
        {
            get
            {
                return _hours;
            }
        }

        public bool IsUnset
        {
            get
            {
                return _isUnset;
            }
        }

        public int Minutes
        {
            get
            {
                return _minutes;
            }
        }

        public UtcPrecision Precision
        {
            get
            {
                return _precision;
            }
        }

        public static TimeSpan operator -(UtcOffset x, UtcOffset y)
        {
            return DateTimeCalculator.Subtract(x, y);
        }

        public override int GetHashCode()
        {
            return _hours ^ (_minutes << 6) ^ (_isUnset.GetHashCode() << 12);
        }
    }
}