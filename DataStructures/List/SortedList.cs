using System;
using System.Collections.Generic;

namespace DataStructures.List;

/// <typeparam name="T">Needs to be IComparable otherwise the sorting is meaningless</typeparam>
public class SortedList<T> : List<T>
    where T : IComparable<T>
{
    public void InsertSorted(T item)
    {
        var index = BinarySearch(item);
        if (index < 0)
            index = ~index;
        Insert(index, item);
    }
}