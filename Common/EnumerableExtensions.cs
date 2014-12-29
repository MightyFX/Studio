using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace MightyFX
{
    /// <summary>
    /// Useful methods when dealing with enumerations.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Adds all the elements in the <paramref name="source"/> to the <paramref name="destination"/> list.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="destination">The destination list.</param>
        /// <param name="source">The source of the items to add.</param>
        public static void AddRange<T>(this IList<T> destination, IEnumerable<T> source)
        {
            foreach (var value in source)
            {
                destination.Add(value);
            }
        }

        /// <summary>
        /// Applies the <paramref name="action"/> to all the elements in the <param name="enumerable"></param>.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="enumerable">The enumerable to act upon.</param>
        /// <param name="action">The action to apply to every item.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Returns a comma-separated list of the items in the enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to stringify.</param>
        /// <returns>A comma-separated list as a sting.</returns>
        public static string ToCsv(this IEnumerable enumerable)
        {
            var builder = new StringBuilder();

            foreach (var item in enumerable)
            {
                if (builder.Length != 0)
                {
                    builder.Append(", ");
                }

                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
