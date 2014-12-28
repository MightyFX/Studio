using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MightyFX
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<T> AsReadOnly<T>(this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        public static void AddRange<T>(this IList<T> destination, IEnumerable<T> source)
        {
            foreach (var value in source)
            {
                destination.Add(value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var value in enumerable)
            {
                action(value);
            }
        }
    }
}
