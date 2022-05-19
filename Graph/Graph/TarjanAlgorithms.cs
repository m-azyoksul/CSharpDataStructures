using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph;

public partial class DirectedGraph<TData>
{
    /// <summary>
    /// Find all strongly connected components using the Tarjan algorithm.
    /// Implementation inspired by https://github.com/williamfiset/Algorithms
    ///
    /// Time complexity: O(V + E)
    /// Space complexity: O(V)
    /// </summary>
    /// <returns>(The number of strongly connected components, for each vertex (id of vertex, index of scc))</returns>
    public (int SccCount, Dictionary<int, int> SccDictionary) TarjanSccMap()
    {
        var id = Vertices.ToDictionary(v => v.Key, _ => -1);
        var lowLink = Vertices.ToDictionary(v => v.Key, _ => 0);
        var sccDict = Vertices.ToDictionary(v => v.Key, _ => 0);
        var inStack = Vertices.ToDictionary(v => v.Key, _ => false);
        var stack = new Stack<int>();
        int newId = 0;
        int sccCount = 0;

        // Start DFS from each node
        foreach (var vertex in Vertices.Keys)
        {
            if (id[vertex] == -1) // Unvisited
            {
                SccDfs(vertex, id, lowLink, sccDict, inStack, stack, ref newId, ref sccCount);
            }
        }

        return (sccCount, sccDict);
    }

    private void SccDfs(int v,
        Dictionary<int, int> id,
        Dictionary<int, int> lowLink,
        Dictionary<int, int> sccDict,
        Dictionary<int, bool> inStack,
        Stack<int> stack,
        ref int newId,
        ref int sccCount)
    {
        lowLink[v] = id[v] = newId++;
        inStack[v] = true;
        stack.Push(v);

        foreach (var connection in AllConnections(v))
        {
            // If node has not been visited yet, visit it
            if (id[connection.To] == -1) // Unvisited
                SccDfs(connection.To, id, lowLink, sccDict, inStack, stack, ref newId, ref sccCount);

            // If node is in stack, update lowest reachable value
            if (inStack[connection.To])
                lowLink[v] = Math.Min(lowLink[v], lowLink[connection.To]);
        }

        // If lowest reachable value has not changed we're at the root node
        if (id[v] != lowLink[v])
            return;

        // Empty the seen stack until back to root node
        for (int nodeToPop = stack.Pop();; nodeToPop = stack.Pop())
        {
            inStack[nodeToPop] = false;
            sccDict[nodeToPop] = sccCount;
            if (nodeToPop == v)
                break;
        }

        sccCount++;
    }

    public List<List<int>> TarjanSccList()
    {
        // Call the SCC algorithm
        (int count, Dictionary<int, int> sccDict) = TarjanSccMap();

        // Initialize
        var sccList = new List<List<int>>(count);
        for (int i = 0; i < count; i++)
            sccList.Add(new List<int>());

        // Map the data
        foreach (var v in Vertices.Keys)
            sccList[sccDict[v]].Add(v);

        return sccList;
    }
}