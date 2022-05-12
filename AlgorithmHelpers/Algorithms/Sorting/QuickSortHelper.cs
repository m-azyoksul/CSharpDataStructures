using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgorithmHelpers.Algorithms.Sorting;

/// <summary>
/// Partition algorithm reference: https://cs.stackexchange.com/a/104825/121265
/// </summary>
public static class QuickSortHelper
{
    public static void QuickSort<T>(this IList<T> array, int degreeOfParallelism = 0) where T : IComparable<T>
    {
        if (array is int[] intArray)
        {
            if (degreeOfParallelism > 0)
                intArray.QuickSort(0, intArray.Length - 1, degreeOfParallelism);
            else
                intArray.QuickSort(0, intArray.Length - 1);
        }
        else
        {
            if (degreeOfParallelism > 0)
                array.QuickSort(0, array.Count - 1, degreeOfParallelism);
            else
                array.QuickSort(0, array.Count - 1);
        }
    }


    private static void QuickSort(this int[] array, int left, int right, int degreeOfParallelism)
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right);

        if (degreeOfParallelism > 0)
        {
            var t = Task.Run(() => array.QuickSort(left, leftPartition - 1, degreeOfParallelism - 1));
            array.QuickSort(rightPartition + 1, right, degreeOfParallelism - 1);
            t.Wait();
        }
        else
        {
            QuickSort(array, left, leftPartition - 1);
            QuickSort(array, rightPartition, right);
        }
    }

    private static void QuickSort(this int[] array, int left, int right)
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right);

        // Sort the sublists
        QuickSort(array, left, leftPartition - 1);
        QuickSort(array, rightPartition, right);
    }

    private static (int, int) Partition(int[] arr, int left, int right)
    {
        var l = left;
        var r = left;
        var u = right;

        var pivot = arr[Median3(left, right, (left + right) / 2, arr)];

        while (r <= u)
        {
            if (arr[r] < pivot)
            {
                (arr[l], arr[r]) = (arr[r], arr[l]);
                l++;
                r++;
            }
            else if (arr[r] > pivot)
            {
                (arr[r], arr[u]) = (arr[u], arr[r]);
                u--;
            }
            else
                r++;
        }

        return (l, r);
    }

    private static int Median3(int a, int b, int c, int[] arr)
    {
        if (arr[a].CompareTo(arr[b]) >= 0)
            if (arr[b].CompareTo(arr[c]) >= 0)
                return b;
            else if (arr[c].CompareTo(arr[a]) >= 0)
                return a;
            else
                return c;
        if (arr[a].CompareTo(arr[c]) >= 0)
            return a;
        if (arr[c].CompareTo(arr[b]) >= 0)
            return b;
        return c;
    }


    private static void QuickSort<T>(this IList<T> array, int left, int right, int degreeOfParallelism) where T : IComparable<T>
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right);

        if (degreeOfParallelism > 0)
        {
            var t = Task.Run(() => array.QuickSort(left, leftPartition - 1, degreeOfParallelism - 1));
            array.QuickSort(rightPartition, right, degreeOfParallelism - 1);
            t.Wait();
        }
        else
        {
            array.QuickSort(left, leftPartition - 1);
            array.QuickSort(rightPartition, right);
        }
    }

    private static void QuickSort<T>(this IList<T> array, int left, int right) where T : IComparable<T>
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right);

        array.QuickSort(left, leftPartition - 1);
        array.QuickSort(rightPartition, right);
    }

    private static (int, int) Partition<T>(IList<T> arr, int left, int right) where T : IComparable<T>
    {
        var l = left;
        var r = left;
        var u = right;

        var pivot = arr[Median3(left, right, (left + right) / 2, arr)];

        while (r <= u)
        {
            switch (arr[r].CompareTo(pivot))
            {
                case < 0:
                    (arr[l], arr[r]) = (arr[r], arr[l]);
                    l++;
                    r++;
                    break;
                case > 0:
                    (arr[r], arr[u]) = (arr[u], arr[r]);
                    u--;
                    break;
                default:
                    r++;
                    break;
            }
        }

        return (l, r);
    }

    private static int Median3<T>(int a, int b, int c, IList<T> arr) where T : IComparable<T>
    {
        if (arr[a].CompareTo(arr[b]) >= 0)
            if (arr[b].CompareTo(arr[c]) >= 0)
                return b;
            else if (arr[c].CompareTo(arr[a]) >= 0)
                return a;
            else
                return c;
        if (arr[a].CompareTo(arr[c]) >= 0)
            return a;
        if (arr[c].CompareTo(arr[b]) >= 0)
            return b;
        return c;
    }

    

    public static void QuickSort<T>(this IList<T> array, IComparer<T> comparer, int degreeOfParallelism = 0)
    {
        array.QuickSort(comparer.Compare, degreeOfParallelism);
    }
    
    public static void QuickSort<T>(this IList<T> array, Func<T, T, int> comparePredicate, int degreeOfParallelism = 0)
    {
        if (degreeOfParallelism > 0)
            array.QuickSort(0, array.Count - 1, comparePredicate, degreeOfParallelism);
        else
            array.QuickSort(0, array.Count - 1, comparePredicate);
    }

    private static void QuickSort<T>(this IList<T> array, int left, int right, Func<T, T, int> comparePredicate, int degreeOfParallelism)
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right, comparePredicate);

        if (degreeOfParallelism > 0)
        {
            var t = Task.Run(() => array.QuickSort(left, leftPartition - 1, comparePredicate, degreeOfParallelism - 1));
            array.QuickSort(rightPartition, right, comparePredicate, degreeOfParallelism - 1);
            t.Wait();
        }
        else
        {
            array.QuickSort(left, leftPartition - 1, comparePredicate);
            array.QuickSort(rightPartition, right, comparePredicate);
        }
    }

    private static void QuickSort<T>(this IList<T> array, int left, int right, Func<T, T, int> comparePredicate)
    {
        if (left >= right)
            return;

        // Partition the list
        var (leftPartition, rightPartition) = Partition(array, left, right, comparePredicate);

        array.QuickSort(left, leftPartition - 1, comparePredicate);
        array.QuickSort(rightPartition, right, comparePredicate);
    }

    private static (int, int) Partition<T>(IList<T> arr, int left, int right, Func<T, T, int> comparePredicate)
    {
        var l = left;
        var r = left;
        var u = right;

        var pivot = arr[Median3(left, right, (left + right) / 2, arr, comparePredicate)];

        while (r <= u)
        {
            switch (comparePredicate(arr[r], pivot))
            {
                case < 0:
                    (arr[l], arr[r]) = (arr[r], arr[l]);
                    l++;
                    r++;
                    break;
                case > 0:
                    (arr[r], arr[u]) = (arr[u], arr[r]);
                    u--;
                    break;
                default:
                    r++;
                    break;
            }
        }

        return (l, r);
    }

    private static int Median3<T>(int a, int b, int c, IList<T> arr, Func<T, T, int> comparePredicate)
    {
        if (comparePredicate(arr[a], arr[b]) >= 0)
            if (comparePredicate(arr[b], arr[c]) >= 0)
                return b;
            else if (comparePredicate(arr[c], arr[a]) >= 0)
                return a;
            else
                return c;
        if (comparePredicate(arr[a], arr[c]) >= 0)
            return a;
        if (comparePredicate(arr[c], arr[b]) >= 0)
            return b;
        return c;
    }
}