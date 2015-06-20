namespace System.ISO8601.Abstract
{
    public abstract class Duration : IComparable, IComparable<Duration>, IEquatable<Duration>
    {
        private static DurationComparer _comparer;

        public static DurationComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new DurationComparer();
                }

                return _comparer;
            }
        }

        public static bool operator !=(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(Duration x, Duration y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public int CompareTo(Duration other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Duration))
            {
                throw new ArgumentException("A duration can only be compared with another duration.");
            }

            return Comparer.Compare(this, (Duration)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Duration))
            {
                return false;
            }

            return Comparer.Compare(this, (Duration)obj) == 0;
        }

        public bool Equals(Duration other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return GetHashCodeOverride();
        }

        internal abstract int GetHashCodeOverride();
    }
}