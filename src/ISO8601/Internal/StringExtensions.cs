namespace System.ISO8601.Internal
{
    internal static class StringExtensions
    {
        internal static bool ContainsBefore(this string s, char c1, char c2)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c1)
                {
                    return true;
                }

                if (s[i] == c2)
                {
                    return false;
                }
            }

            return false;
        }

        internal static bool ContainsAfter(this string s, char c1, char c2)
        {
            var pastC2 = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c1 && pastC2)
                {
                    return true;
                }

                if (s[i] == c2)
                {
                    pastC2 = true;
                }
            }

            return false;
        }
    }
}