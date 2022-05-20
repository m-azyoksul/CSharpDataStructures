using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphSccTests
{
    [Fact]
    public void Directed_Map_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0},
        };

        // Act
        var (sccCount, sccMap) = graph.TarjanSccMap();

        // Assert
        sccCount.Should().Be(1);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Directed_List_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedList = new List<List<int>>
        {
            new() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
        };

        // Act
        var sccList = graph.TarjanSccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public void Directed_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 1},
            {5, 1},
            {6, 1},
            {7, 1},
        };

        // Act
        var (sccCount, sccMap) = graph.TarjanSccMap();

        // Assert
        sccCount.Should().Be(2);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Directed_List_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedList = new List<List<int>>
        {
            new() {0, 1, 2, 3},
            new() {4, 5, 6, 7},
        };

        // Act
        var sccList = graph.TarjanSccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }
    
    [Fact]
    public void Undirected_Map_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0},
        };

        // Act
        var (sccCount, sccMap) = graph.SccMap();

        // Assert
        sccCount.Should().Be(1);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Undirected_List_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedList = new List<List<int>>
        {
            new() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
        };

        // Act
        var sccList = graph.SccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public void Undirected_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedDoubleTick();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 1},
            {5, 1},
            {6, 1},
            {7, 1},
        };

        // Act
        var (sccCount, sccMap) = graph.SccMap();

        // Assert
        sccCount.Should().Be(2);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Undirected_List_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedDoubleTick();
        var expectedList = new List<List<int>>
        {
            new() {0, 1, 2, 3},
            new() {4, 5, 6, 7},
        };

        // Act
        var sccList = graph.SccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }
}