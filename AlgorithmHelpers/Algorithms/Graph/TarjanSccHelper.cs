using System;
using System.Collections.Generic;

namespace AlgorithmHelpers.Algorithms.Graph;

/// <summary>
/// An implementation of Tarjan's Strongly Connected Components algorithm using an adjacency list.
/// Implementation inspired by https://github.com/williamfiset/Algorithms
///
/// Time complexity: O(V+E)
/// Space complexity: O(V)
/// </summary>
public static class TarjanSccHelper
{
    private const int Unvisited = -1;

    /// <summary>
    /// Tarjan's Strongly Connected Components algorithm using an adjacency list
    /// </summary>
    /// <param name="graph">The graph as a list of directed edges for each node</param>
    /// <returns>Number of strongly connected components and which component each node belongs to</returns>
    public static (int, int[]) TarjanSccSolve(this List<List<int>> graph)
    {
        // Initialize
        int[] ids = new int[graph.Count];
        int newId = 0;
        int[] lowestReachable = new int[graph.Count];
        int[] sccArray = new int[graph.Count];
        int sccCount = 0;
        bool[] inStack = new bool[graph.Count];
        Stack<int> stack = new Stack<int>();
        for (var i = 0; i < ids.Length; i++)
            ids[i] = Unvisited;

        // Start DFS from each node
        for (int i = 0; i < graph.Count; i++)
            if (ids[i] == Unvisited)
                VisitNode(i, graph, ids, ref newId, lowestReachable, sccArray, ref sccCount, inStack, stack);

        return (sccCount, sccArray);
    }

    /// <summary>
    /// Visits nodes in a depth-first fashion
    /// </summary>
    /// <param name="node">The current node index</param>
    /// <param name="graph">The input edge list</param>
    /// <param name="ids">Id of each node</param>
    /// <param name="newId">The id of the next node to be visited</param>
    /// <param name="lowestReachable">Indicates for each node the reachable node with lowest id</param>
    /// <param name="sccArray">Which scc each node is in</param>
    /// <param name="sccCount">Number of sccs</param>
    /// <param name="inStack">Flags if each node is stack</param>
    /// <param name="stack">The graph traversal stack</param>
    private static void VisitNode(
        int node, List<List<int>> graph,
        int[] ids,
        ref int newId,
        int[] lowestReachable,
        int[] sccArray,
        ref int sccCount,
        bool[] inStack,
        Stack<int> stack
    )
    {
        ids[node] = lowestReachable[node] = newId;
        newId++;
        inStack[node] = true;
        stack.Push(node);

        foreach (int nextNode in graph[node])
        {
            // If node has not been visited yet, visit it
            if (ids[nextNode] == Unvisited)
                VisitNode(nextNode, graph, ids, ref newId, lowestReachable, sccArray, ref sccCount, inStack, stack);

            // If node is in stack, update lowest reachable value
            if (inStack[nextNode])
                lowestReachable[node] = Math.Min(lowestReachable[node], lowestReachable[nextNode]);
        }

        // If lowest reachable value has not changed we're at the root node
        if (ids[node] != lowestReachable[node])
            return;

        // Empty the seen stack until back to root node
        for (int nodeToPop = stack.Pop();; nodeToPop = stack.Pop())
        {
            inStack[nodeToPop] = false;
            sccArray[nodeToPop] = sccCount;
            if (nodeToPop == node)
                break;
        }

        sccCount++;
    }

    /// <summary>
    /// Converts the output of <see cref="TarjanSccSolve"/> method to a scc list representation.
    /// </summary>
    /// <param name="sccData">Output of the <see cref="TarjanSccSolve"/> method</param>
    /// <returns>List of strongly connected components each containing a list of nodes</returns>
    public static List<List<int>> TarjanSccToList(this (int count, int[] array) sccData)
    {
        // Initialize
        var sccList = new List<List<int>>(sccData.count);
        for (int i = 0; i < sccData.count; i++)
            sccList.Add(new List<int>());

        // Map the data
        for (int i = 0; i < sccData.array.Length; i++)
            sccList[sccData.array[i]].Add(i);

        return sccList;
    }
}