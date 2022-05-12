using System.Threading.Tasks;

namespace AlgorithmHelpers.Algorithms.Sorting;

public static class MergeSortHelper
{
    public static void MergeSort(this int[] array, int degreeOfParallelism = 0)
    {
        if(degreeOfParallelism > 0)
            MergeSort(array, 0, array.Length - 1, degreeOfParallelism);
        else
            MergeSort(array, 0, array.Length - 1);
    }

    private static void MergeSort(int[] array, int left, int right, int degreeOfParallelism)
    {
        if (left >= right)
            return;
        
        int middle = (left + right) / 2;
        if (degreeOfParallelism > 0)
        {
            var t = Task.Run(() =>
                MergeSort(array, left, middle, degreeOfParallelism - 1)
            );
            MergeSort(array, middle + 1, right, degreeOfParallelism - 1);
            t.Wait();
        }
        else
        {
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);
        }
        Merge(array, left, middle, right);
    }

    private static void MergeSort(int[] array, int left, int right)
    {
        if (left >= right)
            return;
        
        int middle = (left + right) / 2;
        MergeSort(array, left, middle);
        MergeSort(array, middle + 1, right);
        InPlaceMerge(array, left, middle, right);
    }

    private static void Merge(int[] arr, int left, int middle, int right)
    {
        var mergedArray = new int[right - left + 1];
        int i = left; // left index
        int j = middle + 1; // right index
        int k = 0; // merged index

        while (i <= middle && j <= right)
        {
            if (arr[i] < arr[j])
            {
                mergedArray[k] = arr[i];
                i++;
            }
            else
            {
                mergedArray[k] = arr[j];
                j++;
            }

            k++;
        }
        while (i <= middle)
        {
            mergedArray[k] = arr[i];
            i++;
            k++;
        }
        while (j <= right)
        {
            mergedArray[k] = arr[j];
            j++;
            k++;
        }

        for (int l = 0; l < mergedArray.Length; l++)
            arr[left + l] = mergedArray[l];
    }
    
    private static void InPlaceMerge(int[] arr, int left, int middle, int right)
    {
        // TODO: implement
    }
}