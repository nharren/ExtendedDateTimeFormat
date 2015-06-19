using System.ISO8601.Internal.Comparers;

namespace System.ISO8601.Abstract
{
    public abstract class DateTime : TimePoint, IComparable, IComparable<DateTime>, IEquatable<DateTime>
    {
        private static DateTimeComparer _comparer;

        public static DateTimeComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new DateTimeComparer();
                }

                return _comparer;
            }
        }

        public static bool operator !=(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(DateTime x, DateTime y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public int CompareTo(DateTime other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is DateTime))
            {
                throw new ArgumentException("A DateTime can only be compared with another DateTime.");
            }

            return Comparer.Compare(this, (DateTime)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DateTime))
            {
                return false;
            }

            return Comparer.Compare(this, (DateTime)obj) == 0;
        }

        public bool Equals(DateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return GetHashCodeOverride();
        }
    }
}