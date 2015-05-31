namespace System.EDTF
{
    public struct ExtendedTimeSpan
    {
        private readonly int _exclusiveDays;
        private readonly int _months;
        private readonly TimeSpan _timeSpan;
        private readonly double _totalMonths;
        private readonly double _totalYears;
        private readonly int _years;

        internal ExtendedTimeSpan(int years, double totalYears, int months, double totalMonths, int exclusiveDays, TimeSpan timeSpan)
        {
            _years = years;
            _totalYears = totalYears;
            _months = months;
            _totalMonths = totalMonths;
            _exclusiveDays = exclusiveDays;
            _timeSpan = timeSpan;
        }

        public int Days
        {
            get
            {
                return _exclusiveDays;
            }
        }

        public int Hours
        {
            get
            {
                return _timeSpan.Hours;
            }
        }

        public int Minutes
        {
            get
            {
                return _timeSpan.Minutes;
            }
        }

        public int Months
        {
            get
            {
                return _months;
            }
        }

        public int Seconds
        {
            get
            {
                return _timeSpan.Seconds;
            }
        }

        public TimeSpan TimeSpan
        {
            get
            {
                return _timeSpan;
            }
        }

        public double TotalDays
        {
            get
            {
                return _timeSpan.TotalDays;
            }
        }

        public double TotalHours
        {
            get
            {
                return _timeSpan.TotalHours;
            }
        }

        public double TotalMinutes
        {
            get
            {
                return _timeSpan.TotalMinutes;
            }
        }

        public double TotalMonths
        {
            get
            {
                return _totalMonths;
            }
        }

        public double TotalSeconds
        {
            get
            {
                return _timeSpan.TotalSeconds;
            }
        }

        public double TotalYears
        {
            get
            {
                return _totalYears;
            }
        }

        public int Years
        {
            get
            {
                return _years;
            }
        }
    }
}