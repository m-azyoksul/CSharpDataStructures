using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphShortestPathTests
{
    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_Success()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(0, 15);
        var shortestPath = graph.ShortestPath(0, 15);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(0, 1, 2, 3, 7, 11, 15);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_Success()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(0, 11);
        var shortestPath = graph.ShortestPath(0, 11);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(0, 2, 5, 11);
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(5, 14);
        var shortestPath = graph.ShortestPath(5, 14);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(5, 6, 10, 14);
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(4, 3);
        var shortestPath = graph.ShortestPath(4, 3);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void Directed_ShortestPath_Directed5x5Matrix_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(3, 8);
        var shortestPath = graph.ShortestPath(3, 8);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(1, 10);
        var shortestPath = graph.ShortestPath(1, 10);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(1, 4, 10);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(1, 0);
        var shortestPath = graph.ShortestPath(1, 0);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void Directed_ShortestPath_DirectedBalancedTree_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(8, 7);
        var shortestPath = graph.ShortestPath(8, 7);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_Success()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(2, 4);
        var shortestPath = graph.ShortestPath(2, 4);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(2, 5, 0, 1, 4);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(5, 6);
        var shortestPath = graph.ShortestPath(5, 6);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(5, 3, 6);
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_NoPath()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(3, 0);
        var shortestPath = graph.ShortestPath(3, 0);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void Directed_ShortestPath_DirectedCollar_NoPath_2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(4, 2);
        var shortestPath = graph.ShortestPath(4, 2);

        // Assert
        isTherePath.Should().BeFalse();
        shortestPath.Should().BeNull();
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(0, 15);
        var shortestPath = graph.ShortestPath(0, 15);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(0, 1, 2, 3, 7, 11, 15);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(5, 14);
        var shortestPath = graph.ShortestPath(5, 14);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(5, 6, 10, 14);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success_3()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(4, 3);
        var shortestPath = graph.ShortestPath(4, 3);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(4, 5, 6, 7, 3);
    }

    [Fact]
    public void UnDirected_ShortestPath_Directed5x5Matrix_Success_4()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var isTherePath = graph.IsTherePath(3, 8);
        var shortestPath = graph.ShortestPath(3, 8);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(3, 2, 1, 0, 4, 8);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(0, 11);
        var shortestPath = graph.ShortestPath(0, 11);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(0, 2, 5, 11);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(1, 10);
        var shortestPath = graph.ShortestPath(1, 10);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(1, 4, 10);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success_3()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(1, 0);
        var shortestPath = graph.ShortestPath(1, 0);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(1, 0);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedBalancedTree_Success_4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var isTherePath = graph.IsTherePath(8, 7);
        var shortestPath = graph.ShortestPath(8, 7);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(8, 3, 7);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(2, 4);
        var shortestPath = graph.ShortestPath(2, 4);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(2, 0, 1, 4);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success_2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(5, 6);
        var shortestPath = graph.ShortestPath(5, 6);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(5, 3, 6);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success_3()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(3, 0);
        var shortestPath = graph.ShortestPath(3, 0);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(3, 0);
    }

    [Fact]
    public void UnDirected_ShortestPath_DirectedCollar_Success_4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var isTherePath = graph.IsTherePath(4, 2);
        var shortestPath = graph.ShortestPath(4, 2);

        // Assert
        isTherePath.Should().BeTrue();
        shortestPath.Should().Equal(4, 1, 0, 2);
    }
}