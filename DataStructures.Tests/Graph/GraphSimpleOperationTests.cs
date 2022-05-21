using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphSimpleOperationTests
{
    [Fact]
    public void Transpose_Success()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(0, 1);
        
        // Act
        var transpose = graph.Transpose();
        
        // Assert
        transpose.Vertices[0].Connections.Should().HaveCount(0);
        transpose.Vertices[1].Connections.Should().HaveCount(1);
        transpose.Vertices[1].Connections.Should().Contain(0);
    }
    
    [Fact]
    public void Transpose_Success_With_Multiple_Edges()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 2);
        
        // Act
        var transpose = graph.Transpose();
        
        // Assert
        transpose.Vertices[0].Connections.Should().HaveCount(0);
        transpose.Vertices[1].Connections.Should().HaveCount(1);
        transpose.Vertices[1].Connections.Should().Contain(0);
        transpose.Vertices[2].Connections.Should().HaveCount(2);
        transpose.Vertices[2].Connections.Should().Contain(0);
        transpose.Vertices[2].Connections.Should().Contain(1);
    }
}