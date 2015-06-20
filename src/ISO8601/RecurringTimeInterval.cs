using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class RecurringTimeInterval : IComparable, IComparable<RecurringTimeInterval>, IEquatable<RecurringTimeInterval>
    {
        private static RecurringTimeIntervalComparer _comparer;
        private readonly TimeInterval _interval;
        private readonly int? _recurrences;

        public RecurringTimeInterval(TimeInterval interval, int? recurrences)
        {
            _interval = interval;
            _recurrences = recurrences;
        }

        public static RecurringTimeIntervalComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new RecurringTimeIntervalComparer();
                }

                return _comparer;
            }
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

        public static bool operator !=(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(RecurringTimeInterval x, RecurringTimeInterval y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static RecurringTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return RecurringTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public int CompareTo(RecurringTimeInterval other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is RecurringTimeInterval))
            {
                throw new ArgumentException("A RecurringTimeInterval can only be compared with another RecurringTimeInterval.");
            }

            return Comparer.Compare(this, (RecurringTimeInterval)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(RecurringTimeInterval))
            {
                return false;
            }

            return Comparer.Compare(this, (RecurringTimeInterval)obj) == 0;
        }

        public bool Equals(RecurringTimeInterval other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return _interval.GetHashCode() ^ _recurrences.GetHashCode();
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return ToString(options, options);
        }

        public virtual string ToString(ISO8601Options leftOptions, ISO8601Options rightOptions)
        {
            return RecurringTimeIntervalSerializer.Serialize(this, leftOptions, rightOptions);
        }
    }
}