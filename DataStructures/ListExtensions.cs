using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures;

public static class ListExtensions
{
    public static bool HasDuplicate<T>(this IEnumerable<T> list)
    {
        var set = new HashSet<T>();
        return list.Any(item => !set.Add(item));
    }
    
    public static void Add<T>(this Queue<T> queue, T item)
    {
        queue.Enqueue(item);
    }
    
    public static void Add<T>(this Stack<T> stack, T item)
    {
        stack.Push(item);
    }

    public static void ForEachReversed<T>(this List<T> list, Action<T> action)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            action(list[i]);
        }
    }
}