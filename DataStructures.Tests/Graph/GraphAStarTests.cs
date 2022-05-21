using System;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphAStarTests
{
    [Fact]
    public void Directed_AStar_Square()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(0, 1, 1);
        graph.AddEdge(0, 4, 3);
        graph.AddEdge(1, 5, 4);
        graph.AddEdge(4, 5, 1);

        // Act
        var (length, path) = graph.AStar(0, 5, (a, b) => Math.Abs(b - a));

        // Assert
        length.Should().Be(4);
        path.Should().Equal(0, 4, 5);
    }
}