using System;
using System.Diagnostics;

namespace AlgorithmHelpers;

public static class AnalysisHelpers
{
    public static void AnalyzeAction(this Action action, int count = 1)
    {
        GC.Collect();
        long memoryBefore = GC.GetTotalMemory(false);
        
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        for (int i = 0; i < count; i++)
            action();
        
        stopwatch.Stop();
        Console.WriteLine("Time: {0}", stopwatch.Elapsed);
        
        long memoryAfter = GC.GetTotalMemory(false);
        Console.WriteLine($"Memory: {memoryAfter - memoryBefore}kb");
    }
}