using System.Text;

namespace System.EDTF.Internal.Serialization
{
    internal static class ExtendedDateTimeCollectionSerializer
    {
        internal static string Serialize(ExtendedDateTimeCollection extendedDateTimeCollection)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('{');

            for (int i = 0; i < extendedDateTimeCollection.Count; i++)
            {
                stringBuilder.Append(extendedDateTimeCollection[i].ToString());

                if (i != extendedDateTimeCollection.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append('}');

            return stringBuilder.ToString();
        }
    }
}