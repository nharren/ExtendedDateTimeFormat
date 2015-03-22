using System.Text;

namespace System.ExtendedDateTimeFormat.Serializers
{
    public static class ExtendedDateTimeIntervalSerializer
    {
        public static string Serialize(ExtendedDateTimeInterval extendedDateTimeInterval)
        {
            var stringBuilder = new StringBuilder();

            if (extendedDateTimeInterval.Start == null)
            {
                return "Error: An interval must have both a start and end defined. Use \"ExtendedDateTime.Unknown\" if the date is unknown.";
            }

            stringBuilder.Append(extendedDateTimeInterval.Start.ToString());

            stringBuilder.Append("/");

            if (extendedDateTimeInterval.End == null)
            {
                return "Error: An interval must have both a start and end defined. Use \"ExtendedDateTime.Unknown\" if the date is unknown.";
            }

            stringBuilder.Append(extendedDateTimeInterval.End.ToString());

            return stringBuilder.ToString();
        }
    }
}