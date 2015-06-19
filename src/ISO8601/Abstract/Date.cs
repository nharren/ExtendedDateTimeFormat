namespace System.ISO8601.Abstract
{
    public abstract class Date : TimePoint, IComparable, IComparable<Date>, IEquatable<Date>
    {
        private static DateComparer _comparer;

        public static DateComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new DateComparer();
                }

                return _comparer;
            }
        }

        public static bool operator !=(Date x, Date y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(Date x, Date y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(Date x, Date y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(Date x, Date y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(Date x, Date y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(Date x, Date y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public int CompareTo(Date other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Date))
            {
                throw new ArgumentException("A date can only be compared with another date.");
            }

            return Comparer.Compare(this, (Date)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Date))
            {
                return false;
            }

            return Comparer.Compare(this, (Date)obj) == 0;
        }

        public bool Equals(Date other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return GetHashCodeOverride();
        }
    }
}