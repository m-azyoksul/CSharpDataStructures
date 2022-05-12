using System;
using System.Collections.Generic;
using Xunit;

namespace AlgorithmHelpers.Tests.DataStructures;

public class GraphBridgesTests
{
    [Fact]
    public void Undirected_Bridges_5x5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var bridges = graph.Bridges();

        // Assert
        Assert.Equal(Array.Empty<(int, int)>(), bridges);
    }

    [Fact]
    public void Undirected_Bridges_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var bridges = graph.Bridges();

        // Assert
        Assert.Equal(new List<(int V1, int V2)>
        {
            (3, 7),
            (3, 8),
            (1, 3),
            (4, 9),
            (4, 10),
            (1, 4),
            (0, 1),
            (5, 11),
            (5, 12),
            (2, 5),
            (2, 6),
            (0, 2),
        }, bridges);
    }

    [Fact]
    public void Undirected_Bridges_Collar()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var bridges = graph.Bridges();

        // Assert
        Assert.Equal(new List<(int V1, int V2)>(), bridges);
    }

    [Fact]
    public void Undirected_BridgesIterative_5x5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var bridges = graph.BridgesIterative();

        // Assert
        Assert.Equal(Array.Empty<(int, int)>(), bridges);
    }

    [Fact]
    public void Undirected_BridgesIterative_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var bridges = graph.BridgesIterative();

        // Assert
        Assert.Equal(new List<(int V1, int V2)>
        {
            (3, 7),
            (3, 8),
            (1, 3),
            (4, 9),
            (4, 10),
            (1, 4),
            (0, 1),
            (5, 11),
            (5, 12),
            (2, 5),
            (2, 6),
            (0, 2),
        }, bridges);
    }

    [Fact]
    public void Undirected_BridgesIterative_Collar()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var bridges = graph.BridgesIterative();

        // Assert
        Assert.Equal(new List<(int V1, int V2)>(), bridges);
    }
}