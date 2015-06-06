using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class StartDurationTimeInterval : TimeInterval
    {
        private readonly Duration _duration;
        private readonly DateTime _start;

        public StartDurationTimeInterval(DateTime start, Duration duration)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            _start = start;
            _duration = duration;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public DateTime Start
        {
            get
            {
                return _start;
            }
        }
    }
}