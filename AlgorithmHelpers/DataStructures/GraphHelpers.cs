using System.Collections.Generic;

namespace AlgorithmHelpers;

public static class GraphHelpers
{
    public static void Add<T>(this Queue<T> queue, T item)
    {
        queue.Enqueue(item);
    }
    
    public static void Add<T>(this Stack<T> queue, T item)
    {
        queue.Push(item);
    }
}