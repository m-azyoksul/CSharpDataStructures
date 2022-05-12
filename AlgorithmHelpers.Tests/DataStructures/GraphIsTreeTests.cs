using AlgorithmHelpers.DataStructures;
using Xunit;

namespace AlgorithmHelpers.Tests.DataStructures;

public class GraphIsTreeTests
{
    [Fact]
    public void Directed_GraphIsTree_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_GraphWithOneVertex_ReturnsTrue()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_TreeWithTwoVertices_ReturnsTrue()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_DoubleConnectedTwoVertices_ReturnsFalse()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_5By5Matrix_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }

    [Fact]
    public void Directed_GraphIsTree_Collar_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }


    [Fact]
    public void Undirected_GraphIsTree_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_GraphWithOneVertex_ReturnsTrue()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddVertex(0);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_TreeWithTwoVertices_ReturnsTrue()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_DoubleConnectedTwoVertices_ReturnsFalse()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.True(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_5By5Matrix_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }

    [Fact]
    public void Undirected_GraphIsTree_Collar_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var isTree = graph.IsTree();

        // Assert
        Assert.False(isTree);
    }
}