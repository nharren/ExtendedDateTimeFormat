using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class ExtendedDateTimeRangeSerializer
    {
        internal static string Serialize(ExtendedDateTimeRange extendedDateTimeRange)
        {
            var stringBuilder = new StringBuilder();

            if (extendedDateTimeRange.Start != null)
            {
                stringBuilder.Append(extendedDateTimeRange.Start.ToString());
            }

            stringBuilder.Append("..");

            if (extendedDateTimeRange.End != null)
            {
                stringBuilder.Append(extendedDateTimeRange.End.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}