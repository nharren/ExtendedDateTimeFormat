namespace System.ISO8601.Abstract
{
    public abstract class TimeInterval : IComparable, IComparable<TimeInterval>, IEquatable<TimeInterval>
    {
        private static TimeIntervalComparer _comparer;

        public static TimeIntervalComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new TimeIntervalComparer();
                }

                return _comparer;
            }
        }

        public static bool operator !=(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(TimeInterval x, TimeInterval y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public int CompareTo(TimeInterval other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is TimeInterval))
            {
                throw new ArgumentException("A TimeInterval can only be compared with another TimeInterval.");
            }

            return Comparer.Compare(this, (TimeInterval)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(TimeInterval))
            {
                return false;
            }

            return Comparer.Compare(this, (TimeInterval)obj) == 0;
        }

        public bool Equals(TimeInterval other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return GetHashCodeOverride();
        }

        public abstract TimeSpan ToTimeSpan();

        internal abstract int GetHashCodeOverride();
    }
}