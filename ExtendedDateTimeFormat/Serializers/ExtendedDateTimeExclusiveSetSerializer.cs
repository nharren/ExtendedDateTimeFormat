using System.Text;

namespace System.ExtendedDateTimeFormat.Serializers
{
    public static class ExtendedDateTimeExclusiveSetSerializer
    {
        public static string Serialize(ExtendedDateTimeExclusiveSet extendedDateTimeExclusiveSet)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('[');

            for (int i = 0; i < extendedDateTimeExclusiveSet.Count; i++)
            {
                stringBuilder.Append(extendedDateTimeExclusiveSet[i].ToString());

                if (i != extendedDateTimeExclusiveSet.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append(']');

            return stringBuilder.ToString();
        }
    }
}