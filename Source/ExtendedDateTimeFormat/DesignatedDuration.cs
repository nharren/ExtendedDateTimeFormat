using System.ExtendedDateTimeFormat.Abstract;

namespace System.ExtendedDateTimeFormat
{
    public class DesignatedDuration : Duration
    {
        private readonly double _days;
        private readonly double _hours;
        private readonly double _minutes;
        private readonly double _months;
        private readonly double _seconds;
        private readonly double _years;

        public DesignatedDuration(int years, int months, int days, int hours, int minutes, double seconds)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }

        public DesignatedDuration(int years, int months, int days, int hours, double minutes)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
            _minutes = minutes;
        }

        public DesignatedDuration(int years, int months, int days, double hours)
        {
            _years = years;
            _months = months;
            _days = days;
            _hours = hours;
        }

        public DesignatedDuration(int years, int months, double days)
        {
            _years = years;
            _months = months;
            _days = days;
        }

        public DesignatedDuration(int years, double months)
        {
            _years = years;
            _months = months;
        }

        public DesignatedDuration(double years)
        {
            _years = years;
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
    }
}