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
}