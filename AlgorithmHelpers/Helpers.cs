using System;
using System.Collections.Generic;

namespace AlgorithmHelpers;

public static class Helpers
{
    public static int[] GenerateRandomArray(int size, int maxValue = Int32.MaxValue)
    {
        var array = new int[size];
        var random = new Random();
        for (var i = 0; i < size; i++)
            array[i] = random.Next(maxValue);
        return array;
    }

    public static bool IsSorted(this int[] array)
    {
        for (var i = 1; i < array.Length; i++)
            if (array[i - 1] > array[i])
                return false;
        return true;
    }

    public static bool IsSorted(this List<int> array)
    {
        for (var i = 1; i < array.Count; i++)
            if (array[i - 1] > array[i])
                return false;
        return true;
    }

    public static bool IsSorted<T>(this List<T> array)
        where T : IComparable<T>
    {
        for (var i = 1; i < array.Count; i++)
            if (array[i - 1].CompareTo(array[i]) > 0)
                return false;
        return true;
    }

    public static bool IsSorted<T>(this List<T> array, Func<T, T, int> comparePredicate)
    {
        for (var i = 1; i < array.Count; i++)
            if (comparePredicate(array[i - 1], array[i]) > 0)
                return false;
        return true;
    }

    public static bool IsSorted<T>(this List<T> array, IComparer<T> comparer)
    {
        for (var i = 1; i < array.Count; i++)
            if (comparer.Compare(array[i - 1], array[i]) > 0)
                return false;
        return true;
    }

    public static int FirstUnsortedIndex(this int[] array)
    {
        for (var i = 1; i < array.Length; i++)
            if (array[i - 1] > array[i])
                return i;
        return -1;
    }
}