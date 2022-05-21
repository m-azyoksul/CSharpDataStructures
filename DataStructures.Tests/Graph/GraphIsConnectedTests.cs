using Xunit;

namespace Graph.Tests;

public class GraphIsConnectedTests
{
    [Fact]
    public void Directed_IsConnected_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_SingleVertexGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_SingleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_DoubleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_SelfLoop_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_MultipleSelfLoops_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_SelfLoopWithOtherEdges_ReturnsTrue()
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
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_5By5Matrix_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_Collar_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_Connected531_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.DirectedConnected531();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Directed_IsConnected_Cycle10000_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCycle10000();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_Window_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedWindow();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_Star_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedStar();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Directed_IsConnected_InboundStar_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.DirectedInboundStar();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void Undirected_IsConnected_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_SingleVertexGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_SingleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_DoubleEdgeGraph_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_SelfLoop_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_MultipleSelfLoops_ReturnsTrue()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddVertex(0);
        graph.AddEdge(0, 0);
        graph.AddEdge(0, 0);

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_SelfLoopWithOtherEdges_ReturnsTrue()
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
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_5By5Matrix_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_BalancedTree_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_Collar_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_Connected531_ReturnsFalse()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedConnected531();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Undirected_IsConnected_Cycle10000_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCycle10000();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_Window_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedWindow();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Undirected_IsConnected_Star_ReturnsTrue()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedStar();

        // Act
        var result = graph.IsConnected();

        // Assert
        Assert.True(result);
    }
}