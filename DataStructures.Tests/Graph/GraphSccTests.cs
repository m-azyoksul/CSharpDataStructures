using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphSccTests
{
    [Fact]
    public void Directed_TarjanMap_Success()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(6, 0);
        graph.AddEdge(6, 2);
        graph.AddEdge(3, 4);
        graph.AddEdge(6, 4);
        graph.AddEdge(2, 0);
        graph.AddEdge(0, 1);
        graph.AddEdge(4, 5);
        graph.AddEdge(5, 6);
        graph.AddEdge(3, 7);
        graph.AddEdge(7, 5);
        graph.AddEdge(1, 2);
        graph.AddEdge(7, 3);
        graph.AddEdge(5, 0);
        var expectedMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 2},
            {4, 1},
            {5, 1},
            {6, 1},
            {7, 2},
        };

        // Act
        var (sccCount, sccArray) = graph.SccMap();

        // Assert
        sccCount.Should().Be(3);
        sccArray.Should().Equal(expectedMap);
    }

    [Fact]
    public void Directed_TarjanList_Success()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(6, 0);
        graph.AddEdge(6, 2);
        graph.AddEdge(3, 4);
        graph.AddEdge(6, 4);
        graph.AddEdge(2, 0);
        graph.AddEdge(0, 1);
        graph.AddEdge(4, 5);
        graph.AddEdge(5, 6);
        graph.AddEdge(3, 7);
        graph.AddEdge(7, 5);
        graph.AddEdge(1, 2);
        graph.AddEdge(7, 3);
        graph.AddEdge(5, 0);
        var expectedList = new List<List<int>>
        {
            new() {0, 1, 2},
            new() {4, 5, 6},
            new() {3, 7},
        };

        // Act
        var sccList = graph.SccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public void Directed_TarjanMap_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 12},
            {1, 6},
            {2, 11},
            {3, 2},
            {4, 5},
            {5, 9},
            {6, 10},
            {7, 0},
            {8, 1},
            {9, 3},
            {10, 4},
            {11, 7},
            {12, 8},
        };

        // Act
        var (sccCount, sccMap) = graph.SccMap();

        // Assert
        sccCount.Should().Be(13);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Directed_TarjanList_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedList = new List<List<int>>
        {
            new() {7},
            new() {8},
            new() {3},
            new() {9},
            new() {10},
            new() {4},
            new() {1},
            new() {11},
            new() {12},
            new() {5},
            new() {6},
            new() {2},
            new() {0},
        };

        // Act
        var sccList = graph.SccList();

        // Assert
        sccList.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public void Directed_TarjanMap_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedMap = new Dictionary<int, int>
        {
            {0, 3},
            {1, 1},
            {2, 0},
            {3, 2},
            {4, 7},
            {5, 5},
            {6, 4},
            {7, 6},
        };

        // Act
        var (sccCount, sccMap) = graph.SccMap();

        // Assert
        sccCount.Should().Be(8);
        sccMap.Should().Equal(expectedMap);
    }

    [Fact]
    public void Directed_TarjanList_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedList = new List<List<int>>
        {
            new() {2},
            new() {1},
            new() {3},
            new() {0},
            new() {6},
            new() {5},
            new() {7},
            new() {4},
        };

        // Act
        var sccList = graph.SccList();

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