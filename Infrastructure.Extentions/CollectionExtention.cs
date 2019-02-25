using System;
using System.Collections.Generic;

namespace Infrastructure.Extentions
{
    public static class CollectionExtention
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }

        public static void ForEach<T>(this IList<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }
    }
}
