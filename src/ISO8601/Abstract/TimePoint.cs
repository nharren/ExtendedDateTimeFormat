namespace System.ISO8601.Abstract
{
    public abstract class TimePoint
    {
        public static TimeSpan operator -(TimePoint x, TimePoint y)
        {
            return DateTimeCalculator.Subtract(x, y);
        }

        public static TimePoint operator +(TimePoint x, Duration y)
        {
            return DateTimeCalculator.Add(x, y);
        }
    }
}