using Xunit;

namespace AlgorithmHelpers.Tests.DataStructures;

public class GraphShortestPathTests
{
    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_Success()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(0, 15);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 7, 11, 15}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(5, 14);

        // Assert
        Assert.Equal(new[] {5, 6, 10, 14}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(4, 3);

        // Assert
        Assert.Null(shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(3, 8);

        // Assert
        Assert.Null(shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_Success()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(0, 11);

        // Assert
        Assert.Equal(new[] {0, 2, 5, 11}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(1, 10);

        // Assert
        Assert.Equal(new[] {1, 4, 10}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(1, 0);

        // Assert
        Assert.Null(shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(8, 7);

        // Assert
        Assert.Null(shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_Success()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(2, 4);

        // Assert
        Assert.Equal(new[] {2, 5, 0, 1, 4}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(5, 6);

        // Assert
        Assert.Equal(new[] {5, 3, 6}, shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(3, 0);

        // Assert
        Assert.Null(shortestPath);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(4, 2);

        // Assert
        Assert.Null(shortestPath);
    }
    
    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(0, 15);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 7, 11, 15}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(5, 14);

        // Assert
        Assert.Equal(new[] {5, 6, 10, 14}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(4, 3);

        // Assert
        Assert.Equal(new[] {4, 5, 6, 7, 3}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var shortestPath = graph.ShortestPath(3, 8);

        // Assert
        Assert.Equal(new[] {3, 2, 1, 0, 4, 8}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(0, 11);

        // Assert
        Assert.Equal(new[] {0, 2, 5, 11}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(1, 10);

        // Assert
        Assert.Equal(new[] {1, 4, 10}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(1, 0);

        // Assert
        Assert.Equal(new[] {1, 0}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var shortestPath = graph.ShortestPath(8, 7);

        // Assert
        Assert.Equal(new[] {8, 3, 7}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(2, 4);

        // Assert
        Assert.Equal(new[] {2, 0, 1, 4}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(5, 6);

        // Assert
        Assert.Equal(new[] {5, 3, 6}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(3, 0);

        // Assert
        Assert.Equal(new[] {3, 0}, shortestPath);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var shortestPath = graph.ShortestPath(4, 2);

        // Assert
        Assert.Equal(new[] {4, 1, 0, 2}, shortestPath);
    }
}