using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class DurationTimeInterval : TimeInterval
    {
        private readonly Duration _duration;

        public DurationTimeInterval(int years, int months, int days, int hours, int minutes, double seconds)
        {
            _duration = new DesignatedDuration(years, months, days, hours, minutes, seconds);
        }

        public DurationTimeInterval(int years, int months, int days, int hours, double minutes)
        {
            _duration = new DesignatedDuration(years, months, days, hours, minutes);
        }

        public DurationTimeInterval(int years, int months, int days, double hours)
        {
            _duration = new DesignatedDuration(years, months, days, hours);
        }

        public DurationTimeInterval(int years, int months, double days)
        {
            _duration = new DesignatedDuration(years, months, days);
        }

        public DurationTimeInterval(int years, double months)
        {
            _duration = new DesignatedDuration(years, months);
        }

        public DurationTimeInterval(double years)
        {
            _duration = new DesignatedDuration(years);
        }

        internal DurationTimeInterval(Duration duration)
        {
            _duration = duration;
        }

        public static DurationTimeInterval FromWeeks(double weeks)
        {
            return new DurationTimeInterval(DesignatedDuration.FromWeeks(weeks));
        }
    }
}