using System.ISO8601.Abstract;

namespace System.ISO8601
{
    public class StartEndTimeInterval : TimeInterval
    {
        private readonly Abstract.DateTime _end;
        private readonly Abstract.DateTime _start;

        public StartEndTimeInterval(CalendarDateTime start, CalendarDateTime end)
        {
            if (start.Precision != TimePrecision.Second)
            {
                throw new ArgumentException("The starting calendar datetime must be precise to the second.");
            }

            _start = start;
            _end = end;
        }

        public StartEndTimeInterval(OrdinalDateTime start, OrdinalDateTime end)
        {
            if (start.Precision != TimePrecision.Second)
            {
                throw new ArgumentException("The starting ordinal datetime must be precise to the second.");
            }

            _start = start;
            _end = end;
        }

        public StartEndTimeInterval(WeekDateTime start, WeekDateTime end)
        {
            if (start.Precision != TimePrecision.Second)
            {
                throw new ArgumentException("The starting week datetime must be precise to the second.");
            }

            _start = start;
            _end = end;
        }

        public Abstract.DateTime End
        {
            get
            {
                return _end;
            }
        }

        public Abstract.DateTime Start
        {
            get
            {
                return _start;
            }
        }
    }
}