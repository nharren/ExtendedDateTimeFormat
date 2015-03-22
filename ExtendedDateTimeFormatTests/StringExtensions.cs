using System.Text;

namespace ExtendedDateTimeFormatTests
{
    public static class StringExtensions
    {
        public static string Indent(this string targetString, int indentLevel)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < indentLevel; i++)
            {
                stringBuilder.Append("    ");
            }

            stringBuilder.Append(targetString);

            return stringBuilder.ToString();
        }
    }
}