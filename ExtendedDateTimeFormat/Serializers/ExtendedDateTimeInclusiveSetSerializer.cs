using System.Text;

namespace System.ExtendedDateTimeFormat.Serializers
{
    public static class ExtendedDateTimeInclusiveSetSerializer
    {
        public static string Serialize(ExtendedDateTimeInclusiveSet extendedDateTimeInclusiveSet)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('{');

            for (int i = 0; i < extendedDateTimeInclusiveSet.Count; i++)
            {
                stringBuilder.Append(extendedDateTimeInclusiveSet[i].ToString());

                if (i != extendedDateTimeInclusiveSet.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append('}');

            return stringBuilder.ToString();
        }
    }
}