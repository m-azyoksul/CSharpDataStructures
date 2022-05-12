using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmHelpers;

public static class ListHelper
{
    public static bool HasDuplicate<T>(this IEnumerable<T> list)
    {
        var set = new HashSet<T>();
        return list.Any(item => !set.Add(item));
    }

    public static void ForEachReversed<T>(this List<T> list, Action<T> action)
    {
        for (var i = list.Count - 1; i >= 0; i--)
            action(list[i]);
    }
}