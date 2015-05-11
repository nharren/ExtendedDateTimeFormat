using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class ExtendedDateTimePossibilityCollectionSerializer
    {
        internal static string Serialize(ExtendedDateTimePossibilityCollection extendedDateTimePossibilityCollection)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('[');

            for (int i = 0; i < extendedDateTimePossibilityCollection.Count; i++)
            {
                stringBuilder.Append(extendedDateTimePossibilityCollection[i].ToString());

                if (i != extendedDateTimePossibilityCollection.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append(']');

            return stringBuilder.ToString();
        }
    }
}