namespace System.ISO8601.Abstract
{
    public abstract class TimePoint
    {
        public static TimeSpan operator -(TimePoint x, TimePoint y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static TimePoint operator +(TimePoint x, Duration y)
        {
            return ISO8601Calculator.Add(x, y);
        }

        public static TimePoint operator -(TimePoint x, Duration y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }
    }
}