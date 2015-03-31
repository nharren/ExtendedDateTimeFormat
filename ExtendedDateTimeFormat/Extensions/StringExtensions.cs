using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat
{
    public static class StringExtensions
    {
        public static bool ContainsBefore(this string s, char c1, char c2)
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
    }
}
