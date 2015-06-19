using System.Collections.Generic;

namespace System.EDTF.Internal
{
    internal static class EnumerableExtensions
    {
        internal static bool ContainsBefore<TSource>(this IEnumerable<TSource> source, TSource x, TSource y)
        {
            return ContainsBefore(source, x, y, null);
        }

        internal static bool ContainsBefore<TSource>(IEnumerable<TSource> source, TSource x, TSource y, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var element in source)
            {
                if (comparer.Equals(element, x))
                {
                    return true;
                }

                if (comparer.Equals(element, y))
                {
                    return false;
                }
            }

            return false;
        }
    }
}