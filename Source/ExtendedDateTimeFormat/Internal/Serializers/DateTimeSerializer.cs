namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    public class DateTimeSerializer
    {
        public static string Serialize(CalendarDateTime dateTime, DateType withDateType, bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return string.Format("{0}{1}", dateTime.Date.ToString(withSeparators), dateTime.Time.ToString(withTimeDesignator, withSeparators, withUtcOffset));
        }
    }
}