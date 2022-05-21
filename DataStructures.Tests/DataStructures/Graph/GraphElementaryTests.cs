using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphElementaryTests
{
    [Fact]
    public void DirectedGraph_AddEdge_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        graph.AddEdge(0, 1);

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_AddEdge_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        graph.AddEdge(0, 1);

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(1);
        graph.Vertices[1].Connections[0].To.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_AddEdgePair_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        graph.AddEdge((0, 1));

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_AddEdgePair_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        graph.AddEdge((0, 1));

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(1);
        graph.Vertices[1].Connections[0].To.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_AddEdgeList_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        graph.AddEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Assert
        graph.Vertices.Count.Should().Be(3);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(1);
        graph.Vertices[1].Connections[0].To.Should().Be(2);
        graph.Vertices[2].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_AddEdgeList_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        graph.AddEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Assert
        graph.Vertices.Count.Should().Be(3);
        graph.Vertices[0].Connections.Count.Should().Be(1);
        graph.Vertices[0].Connections[0].To.Should().Be(1);
        graph.Vertices[1].Connections.Count.Should().Be(2);
        graph.Vertices[1].Connections[0].To.Should().Be(0);
        graph.Vertices[1].Connections[1].To.Should().Be(2);
        graph.Vertices[2].Connections.Count.Should().Be(1);
        graph.Vertices[2].Connections[0].To.Should().Be(1);
    }

    [Fact]
    public void DirectedGraph_RemoveEdge_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        graph.RemoveEdge(0, 1);

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_RemoveEdge_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        graph.RemoveEdge(0, 1);

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_RemoveEdge_Fail()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        var action = () => graph.RemoveEdge(1, 0);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UndirectedGraph_RemoveEdge_Fail()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        var action = () => graph.RemoveEdge(0, 2);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DirectedGraph_RemoveEdgePair_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        graph.RemoveEdge((0, 1));

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_RemoveEdgePair_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddEdge(0, 1);

        // Act
        graph.RemoveEdge((0, 1));

        // Assert
        graph.Vertices.Count.Should().Be(2);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_RemoveEdgeList_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Act
        graph.RemoveEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Assert
        graph.Vertices.Count.Should().Be(3);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
        graph.Vertices[2].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_RemoveEdgeList_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Act
        graph.RemoveEdges(new List<(int, int)> {(0, 1), (1, 2)});

        // Assert
        graph.Vertices.Count.Should().Be(3);
        graph.Vertices[0].Connections.Count.Should().Be(0);
        graph.Vertices[1].Connections.Count.Should().Be(0);
        graph.Vertices[2].Connections.Count.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_AddVertex_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        graph.AddVertex(0);

        // Assert
        graph.Vertices.Count.Should().Be(1);
    }

    [Fact]
    public void UndirectedGraph_AddVertex_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        graph.AddVertex(0);

        // Assert
        graph.Vertices.Count.Should().Be(1);
    }

    [Fact]
    public void DirectedGraph_AddVertex_Fail()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        var action = () => graph.AddVertex(0);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UndirectedGraph_AddVertex_Fail()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        var action = () => graph.AddVertex(0);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DirectedGraph_RemoveVertex_Success()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        graph.RemoveVertex(0);

        // Assert
        graph.Vertices.Count.Should().Be(0);
    }

    [Fact]
    public void UndirectedGraph_RemoveVertex_Success()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        graph.RemoveVertex(0);

        // Assert
        graph.Vertices.Count.Should().Be(0);
    }

    [Fact]
    public void DirectedGraph_RemoveVertex_Fail()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        var action = () => graph.RemoveVertex(0);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UndirectedGraph_RemoveVertex_Fail()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        var action = () => graph.RemoveVertex(0);

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}