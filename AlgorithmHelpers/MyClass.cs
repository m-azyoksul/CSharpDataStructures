using System;
using System.Collections.Generic;
using System.Diagnostics;
using AlgorithmHelpers.Algorithms.Sorting;

namespace AlgorithmHelpers;

public static class MyClass
{
    public static IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
    {
        var ret = new List<IList<int>>();
        
        for(int i = 0; i < connections.Count; i++)
        {
            var server = connections[i];
            connections.RemoveAt(i);

            if (!IsConnected(n, connections))
                ret.Add(server);
            
            connections.Insert(i, server);
        }

        return ret;
    }

    private static bool IsConnected(int n, IList<IList<int>> connections)
    {
        return false;
    }

    public static void TestQuickSort()
    {
        long totalTimeElapsed = 0;
        int iterationCount = 5;

        for (var i = 0; i < iterationCount; i++)
        {
            var arr = Helpers.GenerateRandomArray(10000);
            //var arr = new[]{(4, 7), (10, 0), (4, 4), (5, 9), (1, 7), (9, 9), (9, 1), (4, 0), (19, 5)};
            //Console.WriteLine("Array: " + string.Join(", ", arr));
            var sw = new Stopwatch();
            sw.Start();
            arr.MergeSort();
            sw.Stop();
            totalTimeElapsed += sw.ElapsedMilliseconds;
            //Console.WriteLine("Array: " + string.Join(", ", arr));
            Console.WriteLine("Sort: {0}", sw.Elapsed);
            if (!arr.IsSorted())
                Console.WriteLine("Not sorted");
            //Console.WriteLine("------");
        }

        Console.WriteLine("Average: {0}", totalTimeElapsed / iterationCount);
    }
}