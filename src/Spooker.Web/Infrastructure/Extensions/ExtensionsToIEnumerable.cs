using System;
using System.Collections.Generic;

namespace Spooker.Web.Infrastructure.Extensions
{
    public static class ExtensionsToIEnumerable
    {
        /// <summary>
        /// Generate a string representation of an object.
        /// </summary>
        /// <param name="elements">the instance to represent as string</param>
        /// <param name="action">callback on each element</param>
        /// <returns>a string representation of the object</returns>
        /// <remarks>relies on reflection to detect the properties and their values, used in the generate string</remarks>
        /// <exception cref="ArgumentNullException">when the instance is null</exception>
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
                action(element);
        }
    }
}