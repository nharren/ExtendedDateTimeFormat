namespace System.ISO8601
{
    sealed public class ISO8601Options : ICloneable
    {
        public static readonly ISO8601Options Default = new ISO8601Options();
        private char _decimalSeparator = ',';
        private int _fractionLength = 0;
        private bool _isExpanded = false;
        private bool _useComponentSeparators = true;
        private bool _useTimeDesignator = true;
        private bool _useUtcOffset = true;
        private int _yearLength = 4;

        public char DecimalSeparator
        {
            get
            {
                return _decimalSeparator;
            }

            set
            {
                if (_decimalSeparator != '.' && _decimalSeparator != ',')
                {
                    throw new InvalidOperationException("The decimal separator must be either a comma or a period.");
                }

                _decimalSeparator = value;
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

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
            }
        }

        public bool UseComponentSeparators
        {
            get
            {
                return _useComponentSeparators;
            }

            set
            {
                _useComponentSeparators = value;
            }
        }

        public bool UseTimeDesignator
        {
            get
            {
                return _useTimeDesignator;
            }

            set
            {
                _useTimeDesignator = value;
            }
        }

        public bool UseUtcOffset
        {
            get
            {
                return _useUtcOffset;
            }

            set
            {
                _useUtcOffset = value;
            }
        }

        public int YearLength
        {
            get
            {
                return _yearLength;
            }

            set
            {
                _yearLength = value;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}