using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class RecurringTimeInterval
    {
        private readonly TimeInterval _interval;
        private readonly int? _recurrences;

        public RecurringTimeInterval(TimeInterval interval, int? recurrences)
        {
            _interval = interval;
            _recurrences = recurrences;
        }

        public TimeInterval Interval
        {
            get
            {
                return _interval;
            }
        }

        public int? Recurrences
        {
            get
            {
                return _recurrences;
            }
        }
    }
}