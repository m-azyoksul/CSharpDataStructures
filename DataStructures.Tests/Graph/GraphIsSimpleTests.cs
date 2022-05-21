using Xunit;

namespace Graph.Tests;

public class GraphIsSimpleTests
{
    [Fact]
    public void Directed_IsSimple_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_SingleNodeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_SingleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_DoubleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_SelfLoop_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Directed_IsSimple_MultipleSelfLoops_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Directed_IsSimple_SelfLoopWithOtherEdges_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 0);
        graph.AddEdge(1, 2);
        graph.AddEdge(0, 2);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Directed_IsSimple_5By5Matrix_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_Collar_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_Connected531_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedConnected531();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_Cycle10000_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCycle10000();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_Window_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.DirectedWindow();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Directed_IsSimple_Star_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedStar();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsSimple_InboundStar_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedInboundStar();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void Undirected_IsSimple_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_SingleNodeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_SingleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_DoubleEdgeGraph_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_SelfLoop_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_MultipleSelfLoops_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_SelfLoopWithOtherEdges_ReturnsFalse()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 0);
        graph.AddEdge(1, 2);
        graph.AddEdge(0, 2);

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_5By5Matrix_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_Collar_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_Connected531_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedConnected531();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_Cycle10000_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCycle10000();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsSimple_Window_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedWindow();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsSimple_Star_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedStar();

        // Act
        var result = graph.IsSimple();

        // Assert
        Assert.True(result);
    }
}