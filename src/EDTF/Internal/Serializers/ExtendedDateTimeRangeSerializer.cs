using System.Text;

namespace System.EDTF.Internal.Serializers
{
    internal static class ExtendedDateTimeRangeSerializer
    {
        internal static string Serialize(ExtendedDateTimeRange extendedDateTimeRange)
        {
            var stringBuilder = new StringBuilder();

            if (extendedDateTimeRange.Start != null && (ExtendedDateTime)extendedDateTimeRange.Start != ExtendedDateTime.Minimum)
            {
                stringBuilder.Append(extendedDateTimeRange.Start.ToString());
            }

            stringBuilder.Append("..");

            if (extendedDateTimeRange.End != null && (ExtendedDateTime)extendedDateTimeRange.End != ExtendedDateTime.Maximum)
            {
                stringBuilder.Append(extendedDateTimeRange.End.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}