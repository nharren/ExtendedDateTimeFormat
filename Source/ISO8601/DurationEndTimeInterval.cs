using System;
using System.Collections.Generic;
using System.ISO8601.Abstract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ISO8601
{
    public class DurationEndTimeInterval : TimeInterval
    {
        private readonly Duration _duration;
        private readonly DateTime _end;

        public DurationEndTimeInterval(Duration duration, DateTime end)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }
            
            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            _duration = duration;
            _end = end;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public DateTime End
        {
            get
            {
                return _end;
            }
        }
    }
}
