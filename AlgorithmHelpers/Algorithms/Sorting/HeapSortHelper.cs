using System;
using System.Collections.Generic;

namespace AlgorithmHelpers.Algorithms.Sorting;

public static class HeapSortHelper
{
    public static void HeapSort(this int[] array)
    {
        int heapSize = array.Length;

        for (int i = heapSize / 2 - 1; i >= 0; i--)
            Heapify(array, heapSize, i);

        for (int i = heapSize - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            heapSize--;
            Heapify(array, heapSize, 0);
        }
    }

    private static void Heapify(int[] array, int heapSize, int index)
    {
        int largest = index;
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heapSize && array[left] > array[largest])
                largest = left;

            if (right < heapSize && array[right] > array[largest])
                largest = right;

            if (largest == index)
                return;

            (array[index], array[largest]) = (array[largest], array[index]);
            index = largest;
        }
    }
    
    

    public static void HeapSort<T>(this IList<T> array)
        where T : IComparable<T>
    {
        int heapSize = array.Count;

        for (int i = heapSize / 2 - 1; i >= 0; i--)
            Heapify(array, heapSize, i);

        for (int i = heapSize - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            heapSize--;
            Heapify(array, heapSize, 0);
        }
    }

    private static void Heapify<T>(IList<T> array, int heapSize, int index)
        where T : IComparable<T>
    {
        int largest = index;
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heapSize && array[left].CompareTo(array[largest]) > 0)
                largest = left;

            if (right < heapSize && array[right].CompareTo(array[largest]) > 0)
                largest = right;

            if (largest == index)
                return;

            (array[index], array[largest]) = (array[largest], array[index]);
            index = largest;
        }
    }
    
    

    public static void HeapSort<T>(this IList<T> array, IComparer<T> comparer)
    {
        array.HeapSort(comparer.Compare);
    }
    
    public static void HeapSort<T>(this IList<T> array, Func<T, T, int> comparePredicate)
    {
        int heapSize = array.Count;

        for (int i = heapSize / 2 - 1; i >= 0; i--)
            Heapify(array, heapSize, i, comparePredicate);

        for (int i = heapSize - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            heapSize--;
            Heapify(array, heapSize, 0, comparePredicate);
        }
    }

    private static void Heapify<T>(IList<T> array, int heapSize, int index, Func<T, T, int> comparePredicate)
    {
        int largest = index;
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heapSize && comparePredicate(array[left], array[largest]) > 0)
                largest = left;

            if (right < heapSize && comparePredicate(array[right], array[largest]) > 0)
                largest = right;

            if (largest == index)
                return;

            (array[index], array[largest]) = (array[largest], array[index]);
            index = largest;
        }
    }
}