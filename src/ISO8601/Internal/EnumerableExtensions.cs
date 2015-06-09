using System.Collections.Generic;
using System.Linq;

namespace System.ISO8601.Internal
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

        internal static bool ContainsAfter<TSource>(this IEnumerable<TSource> source, TSource x, TSource y)
        {
            return ContainsAfter(source, x, y, null);
        }

        internal static bool ContainsAfter<TSource>(this IEnumerable<TSource> source, TSource x, TSource y, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var pastY = false;

            foreach (var element in source)
            {
                if (comparer.Equals(element, x) && pastY)
                {
                    return true;
                }

                if (comparer.Equals(element, y))
                {
                    pastY = true;
                }
            }

            return false;
        }

        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] values)
        {
            return ContainsAny(source, null, values);
        }

        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer, params TSource[] values)
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
                if (values.Any(e => comparer.Equals(element, e)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}