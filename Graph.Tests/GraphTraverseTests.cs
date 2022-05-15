using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphTraverseTests
{
    [Fact]
    public void Directed_BfsTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.BfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 4, 2, 5, 8, 3, 6, 9, 12, 7, 10, 13, 11, 14, 15}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.BfsTraversal(6);

        // Assert
        Assert.Equal(new[] {6, 7, 10, 11, 14, 15}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.BfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.BfsTraversal(4);

        // Assert
        Assert.Equal(new[] {4, 9, 10}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.BfsTraversal(2);

        // Assert
        Assert.Equal(new[] {2, 5, 0, 3, 1, 6, 4}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.BfsTraversal(5);

        // Assert
        Assert.Equal(new[] {5, 0, 2, 3, 1, 6, 4}, traverseList);
    }

    [Fact]
    public void Directed_BfsTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.BfsTraversal(0));
    }


    [Fact]
    public void Undirected_BfsTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.BfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 4, 2, 5, 8, 3, 6, 9, 12, 7, 10, 13, 11, 14, 15}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.BfsTraversal(6);

        // Assert
        Assert.Equal(new[] {6, 5, 7, 2, 10, 4, 1, 9, 3, 11, 14, 0, 8, 13, 15, 12}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.BfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.BfsTraversal(4);

        // Assert
        Assert.Equal(new[] {4, 1, 9, 10, 0, 3, 2, 7, 8, 5, 6, 11, 12}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.BfsTraversal(2);

        // Assert
        Assert.Equal(new[] {2, 0, 5, 1, 3, 4, 6}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.BfsTraversal(5);

        // Assert
        Assert.Equal(new[] {5, 2, 0, 3, 1, 6, 4}, traverseList);
    }

    [Fact]
    public void Undirected_BfsTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.BfsTraversal(0));
    }


    [Fact]
    public void Directed_DfsTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 7, 11, 15, 6, 10, 14, 5, 9, 13, 4, 8, 12}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversal(6);

        // Assert
        Assert.Equal(new[] {6, 7, 11, 15, 10, 14}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 3, 7, 8, 4, 9, 10, 2, 5, 11, 12, 6}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversal(4);

        // Assert
        Assert.Equal(new[] {4, 9, 10}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.DfsTraversal(2);

        // Assert
        Assert.Equal(new[] {2, 5, 0, 1, 3, 6, 4}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.DfsTraversal(5);

        // Assert
        Assert.Equal(new[] {5, 0, 1, 3, 6, 4, 2}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversal_Collar_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsTraversal(0));
    }


    [Fact]
    public void Undirected_DfsTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 2, 3, 7, 6, 5, 4, 8, 9, 10, 11, 15, 14, 13, 12}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversal(6);

        // Assert
        Assert.Equal(new[] {6, 5, 4, 0, 1, 2, 3, 7, 11, 10, 9, 8, 12, 13, 14, 15}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversal(0);

        // Assert
        Assert.Equal(new[] {0, 1, 3, 7, 8, 4, 9, 10, 2, 5, 11, 12, 6}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversal(4);

        // Assert
        Assert.Equal(new[] {4, 1, 0, 2, 5, 11, 12, 6, 3, 7, 8, 9, 10}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.DfsTraversal(2);

        // Assert
        Assert.Equal(new[] {2, 0, 1, 3, 6, 4, 5}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.DfsTraversal(5);

        // Assert
        Assert.Equal(new[] {5, 2, 0, 1, 3, 6, 4}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsTraversal(0));
    }


    [Fact]
    public void Directed_DfsTraversalIterative_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversalIterative(0);

        // Assert
        Assert.Equal(new List<int> {0, 1, 2, 3, 7, 11, 15, 6, 10, 14, 5, 9, 13, 4, 8, 12}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversalIterative(6);

        // Assert
        Assert.Equal(new[] {6, 7, 11, 15, 10, 14}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversalIterative(0);

        // Assert
        Assert.Equal(new[] {0, 1, 3, 7, 8, 4, 9, 10, 2, 5, 11, 12, 6}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversalIterative(4);

        // Assert
        Assert.Equal(new[] {4, 9, 10}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.DfsTraversalIterative(2);

        // Assert
        Assert.Equal(new List<int> {2, 5, 0, 1, 3, 6, 4}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();

        // Act
        var traverseList = graph.DfsTraversalIterative(5);

        // Assert
        Assert.Equal(new List<int> {5, 0, 1, 3, 6, 4, 2}, traverseList);
    }

    [Fact]
    public void Directed_DfsTraversalIterative_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsTraversalIterative(0));
    }


    [Fact]
    public void Undirected_DfsTraversalIterative_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversalIterative(0);

        // Assert
        Assert.Equal(new List<int> {0, 1, 2, 3, 7, 6, 5, 4, 8, 9, 10, 11, 15, 14, 13, 12}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var traverseList = graph.DfsTraversalIterative(6);

        // Assert
        Assert.Equal(new List<int> {6, 5, 4, 0, 1, 2, 3, 7, 11, 10, 9, 8, 12, 13, 14, 15}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversalIterative(0);

        // Assert
        Assert.Equal(new List<int> {0, 1, 3, 7, 8, 4, 9, 10, 2, 5, 11, 12, 6}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var traverseList = graph.DfsTraversalIterative(4);

        // Assert
        Assert.Equal(new List<int> {4, 1, 0, 2, 5, 11, 12, 6, 3, 7, 8, 9, 10}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.DfsTraversalIterative(2);

        // Assert
        Assert.Equal(new List<int> {2, 0, 1, 3, 6, 4, 5}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var traverseList = graph.DfsTraversalIterative(5);

        // Assert
        Assert.Equal(new List<int> {5, 2, 0, 1, 3, 6, 4}, traverseList);
    }

    [Fact]
    public void Undirected_DfsTraversalIterative_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsTraversalIterative(0));
    }


    /////////////////// Edge traversal ///////////////////


    [Fact]
    public void Directed_BfsEdgeTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (0, 1, true),
            (0, 4, true),
            (1, 2, true),
            (1, 5, true),
            (4, 5, true),
            (5, 4, false),
            (4, 8, true),
            (2, 3, true),
            (2, 6, true),
            (5, 6, true),
            (6, 5, false),
            (5, 9, true),
            (8, 9, true),
            (9, 8, false),
            (8, 12, true),
            (3, 7, true),
            (6, 7, true),
            (7, 6, false),
            (6, 10, true),
            (9, 10, true),
            (10, 9, false),
            (9, 13, true),
            (12, 13, true),
            (13, 12, false),
            (7, 11, true),
            (10, 11, true),
            (11, 10, false),
            (10, 14, true),
            (13, 14, true),
            (14, 13, false),
            (11, 15, true),
            (14, 15, true),
            (15, 14, false),
            (15, 11, false),
            (14, 10, false),
            (11, 7, false),
            (13, 9, false),
            (10, 6, false),
            (7, 3, false),
            (12, 8, false),
            (9, 5, false),
            (6, 2, false),
            (3, 2, false),
            (8, 4, false),
            (5, 1, false),
            (2, 1, false),
            (4, 0, false),
            (1, 0, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (6, 7, true),
            (6, 10, true),
            (7, 11, true),
            (10, 11, true),
            (11, 10, false),
            (10, 14, true),
            (11, 15, true),
            (14, 15, true),
            (15, 14, false),
            (15, 11, false),
            (14, 10, false),
            (11, 7, false),
            (10, 6, false),
            (7, 6, false),
        };

        // Act
        var edgeList = graph.BfsEdgeTraversal(6);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (0, 1, true),
            (0, 2, true),
            (1, 3, true),
            (1, 4, true),
            (2, 5, true),
            (2, 6, true),
            (3, 7, true),
            (3, 8, true),
            (4, 9, true),
            (4, 10, true),
            (5, 11, true),
            (5, 12, true),
            (12, 5, false),
            (11, 5, false),
            (10, 4, false),
            (9, 4, false),
            (8, 3, false),
            (7, 3, false),
            (6, 2, false),
            (5, 2, false),
            (4, 1, false),
            (3, 1, false),
            (2, 0, false),
            (1, 0, false),
        };

        // Act
        var edgeList = graph.BfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (4, 9, true),
            (4, 10, true),
            (10, 4, false),
            (9, 4, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (2, 5, true),
            (5, 0, true),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (0, 1, true),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (3, 6, true),
            (1, 3, true),
            (3, 1, false),
            (1, 4, true),
            (1, 6, true),
            (6, 1, false),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (4, 1, false),
            (6, 3, false),
            (1, 0, false),
            (3, 5, false),
            (0, 5, false),
            (5, 2, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(2);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (5, 0, true),
            (5, 2, true),
            (5, 3, true),
            (0, 1, true),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (2, 5, true),
            (5, 2, false),
            (3, 6, true),
            (1, 3, true),
            (3, 1, false),
            (1, 4, true),
            (1, 6, true),
            (6, 1, false),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (4, 1, false),
            (6, 3, false),
            (1, 0, false),
            (3, 5, false),
            (2, 5, false),
            (0, 5, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(5);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_BfsEdgeTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.BfsEdgeTraversal(0));
    }


    [Fact]
    public void Undirected_BfsEdgeTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (0, 1, true),
            (0, 4, true),
            (1, 2, true),
            (1, 5, true),
            (4, 5, true),
            (5, 4, false),
            (4, 8, true),
            (2, 3, true),
            (2, 6, true),
            (5, 6, true),
            (6, 5, false),
            (5, 9, true),
            (8, 9, true),
            (9, 8, false),
            (8, 12, true),
            (3, 7, true),
            (6, 7, true),
            (7, 6, false),
            (6, 10, true),
            (9, 10, true),
            (10, 9, false),
            (9, 13, true),
            (12, 13, true),
            (13, 12, false),
            (7, 11, true),
            (10, 11, true),
            (11, 10, false),
            (10, 14, true),
            (13, 14, true),
            (14, 13, false),
            (11, 15, true),
            (14, 15, true),
            (15, 14, false),
            (15, 11, false),
            (14, 10, false),
            (11, 7, false),
            (13, 9, false),
            (10, 6, false),
            (7, 3, false),
            (12, 8, false),
            (9, 5, false),
            (6, 2, false),
            (3, 2, false),
            (8, 4, false),
            (5, 1, false),
            (2, 1, false),
            (4, 0, false),
            (1, 0, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(0);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Undirected_BfsEdgeTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (6, 5, true),
            (6, 7, true),
            (6, 2, true),
            (6, 10, true),
            (5, 4, true),
            (5, 1, true),
            (5, 9, true),
            (7, 3, true),
            (7, 11, true),
            (2, 1, true),
            (1, 2, false),
            (2, 3, true),
            (3, 2, false),
            (10, 9, true),
            (9, 10, false),
            (10, 11, true),
            (11, 10, false),
            (10, 14, true),
            (4, 0, true),
            (4, 8, true),
            (1, 0, true),
            (0, 1, false),
            (9, 8, true),
            (8, 9, false),
            (9, 13, true),
            (11, 15, true),
            (14, 13, true),
            (13, 14, false),
            (14, 15, true),
            (15, 14, false),
            (8, 12, true),
            (13, 12, true),
            (12, 13, false),
            (12, 8, false),
            (15, 11, false),
            (13, 9, false),
            (8, 4, false),
            (0, 4, false),
            (14, 10, false),
            (11, 7, false),
            (3, 7, false),
            (9, 5, false),
            (1, 5, false),
            (4, 5, false),
            (10, 6, false),
            (2, 6, false),
            (7, 6, false),
            (5, 6, false),
        };

        // Act
        var edgeList = graph.BfsEdgeTraversal(6);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Undirected_BfsEdgeTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (0, 1, true),
            (0, 2, true),
            (1, 3, true),
            (1, 4, true),
            (2, 5, true),
            (2, 6, true),
            (3, 7, true),
            (3, 8, true),
            (4, 9, true),
            (4, 10, true),
            (5, 11, true),
            (5, 12, true),
            (12, 5, false),
            (11, 5, false),
            (10, 4, false),
            (9, 4, false),
            (8, 3, false),
            (7, 3, false),
            (6, 2, false),
            (5, 2, false),
            (4, 1, false),
            (3, 1, false),
            (2, 0, false),
            (1, 0, false),
        };

        // Act
        var edgeList = graph.BfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Undirected_BfsEdgeTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (4, 1, true),
            (4, 9, true),
            (4, 10, true),
            (1, 0, true),
            (1, 3, true),
            (0, 2, true),
            (3, 7, true),
            (3, 8, true),
            (2, 5, true),
            (2, 6, true),
            (5, 11, true),
            (5, 12, true),
            (12, 5, false),
            (11, 5, false),
            (6, 2, false),
            (5, 2, false),
            (8, 3, false),
            (7, 3, false),
            (2, 0, false),
            (3, 1, false),
            (0, 1, false),
            (10, 4, false),
            (9, 4, false),
            (1, 4, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_BfsEdgeTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (0, 0, true),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(2);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_BfsEdgeTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>
        {
            (5, 0, true),
            (5, 2, true),
            (5, 3, true),
            (0, 1, true),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (2, 5, true),
            (5, 2, false),
            (3, 6, true),
            (1, 3, true),
            (3, 1, false),
            (1, 4, true),
            (1, 6, true),
            (6, 1, false),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (4, 1, false),
            (6, 3, false),
            (1, 0, false),
            (3, 5, false),
            (2, 5, false),
            (0, 5, false),
        };

        // Act
        var traverseList = graph.BfsEdgeTraversal(5);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Undirected_BfsEdgeTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.BfsEdgeTraversal(0));
    }


    [Fact]
    public void Directed_DfsEdgeTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedVertexList = new List<int>
            {0, 1, 2, 3, 7, 11, 15, 11, 7, 3, 2, 6, 7, 6, 10, 11, 10, 14, 15, 14, 10, 6, 2, 1, 5, 6, 5, 9, 10, 9, 13, 14, 13, 9, 5, 1, 0, 4, 5, 4, 8, 9, 8, 12, 13, 12, 8, 4, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedVertexList = new List<int> {6, 7, 11, 15, 11, 7, 6, 10, 11, 10, 14, 15, 14, 10, 6};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(6);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedVertexList = new List<int> {0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedVertexList = new List<int> {4, 9, 4, 10, 4};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var traverseList = graph.DfsEdgeTraversal(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (2, 5, true),
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
            (5, 2, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversal(2);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 5, true),
            (5, 2, false),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversal(5);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsEdgeTraversal(0));
    }


    [Fact]
    public void Undirected_DfsEdgeTraversal_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedVertexList = new List<int>
            {0, 1, 2, 3, 7, 6, 5, 4, 0, 4, 8, 9, 10, 11, 7, 11, 15, 14, 13, 12, 8, 12, 13, 9, 13, 14, 10, 14, 15, 11, 10, 6, 10, 9, 5, 9, 8, 4, 5, 1, 5, 6, 2, 6, 7, 3, 2, 1, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(0);

        // Assert
        edgeList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversal_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedVertexList = new List<int>
            {6, 5, 4, 0, 1, 2, 3, 7, 6, 7, 11, 10, 9, 8, 4, 8, 12, 13, 14, 15, 11, 15, 14, 10, 14, 13, 9, 13, 12, 8, 9, 5, 9, 10, 6, 10, 11, 7, 3, 2, 6, 2, 1, 5, 1, 0, 4, 5, 6};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(6);

        // Assert
        edgeList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversal_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedVertexList = new List<int> {0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversal(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversal_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedVertexList = new List<int> {4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var traverseList = graph.DfsEdgeTraversal(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_DfsEdgeTraversal_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (2, 5, true),
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
            (5, 2, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversal(2);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_DfsEdgeTraversal_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 5, true),
            (5, 2, false),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversal(5);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversal_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsEdgeTraversal(0));
    }


    [Fact]
    public void Directed_DfsEdgeTraversalIterative_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        var expectedVertexList = new List<int>
            {0, 1, 2, 3, 7, 11, 15, 11, 7, 3, 2, 6, 7, 6, 10, 11, 10, 14, 15, 14, 10, 6, 2, 1, 5, 6, 5, 9, 10, 9, 13, 14, 13, 9, 5, 1, 0, 4, 5, 4, 8, 9, 8, 12, 13, 12, 8, 4, 0};
        var stack = new Stack<int>();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>();
        for (var i = 0; i < expectedVertexList.Count - 1; i++)
        {
            if (stack.Count == 0 || expectedVertexList[i + 1] != stack.Peek())
            {
                expectedEdgeList.Add((expectedVertexList[i], expectedVertexList[i + 1], true));
                stack.Push(expectedVertexList[i]);
            }
            else
            {
                expectedEdgeList.Add((expectedVertexList[i], expectedVertexList[i + 1], false));
                stack.Pop();
            }
        }

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedVertexList = new List<int> {6, 7, 11, 15, 11, 7, 6, 10, 11, 10, 14, 15, 14, 10, 6};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(6);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedVertexList = new List<int> {0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedVertexList = new List<int> {4, 9, 4, 10, 4};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (2, 5, true),
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
            (5, 2, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(2);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.DirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 5, true),
            (5, 2, false),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(5);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Directed_DfsEdgeTraversalIterative_InvalidVertex()
    {
        // Arrange
        var graph = new DirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsEdgeTraversalIterative(0));
    }


    [Fact]
    public void Undirected_DfsEdgeTraversalIterative_5x5Matrix_From0()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedVertexList = new List<int>
            {0, 1, 2, 3, 7, 6, 5, 4, 0, 4, 8, 9, 10, 11, 7, 11, 15, 14, 13, 12, 8, 12, 13, 9, 13, 14, 10, 14, 15, 11, 10, 6, 10, 9, 5, 9, 8, 4, 5, 1, 5, 6, 2, 6, 7, 3, 2, 1, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(0);

        // Assert
        edgeList.Should().Equal(expectedEdgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversalIterative_5x5Matrix_From6()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
        var expectedVertexList = new List<int>
            {6, 5, 4, 0, 1, 2, 3, 7, 6, 7, 11, 10, 9, 8, 4, 8, 12, 13, 14, 15, 11, 15, 14, 10, 14, 13, 9, 13, 12, 8, 9, 5, 9, 10, 6, 10, 11, 7, 3, 2, 6, 2, 1, 5, 1, 0, 4, 5, 6};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(6);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversalIterative_BalancedTree_From0()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedVertexList = new List<int> {0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var edgeList = graph.DfsEdgeTraversalIterative(0);

        // Assert
        Assert.Equal(expectedEdgeList, edgeList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversalIterative_BalancedTree_From4()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
        var expectedVertexList = new List<int> {4, 1, 0, 2, 5, 11, 5, 12, 5, 2, 6, 2, 0, 1, 3, 7, 3, 8, 3, 1, 4, 9, 4, 10, 4};
        var expectedEdgeList = ExpectedEdgeList(expectedVertexList);

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(4);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_DfsEdgeTraversalIterative_Collar_From2()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (2, 5, true),
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
            (5, 2, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(2);

        // Assert
        traverseList.Should().Equal(expectedEdgeList);
    }

    [Fact(Skip = "Class is not built to handle non-simple graphs")]
    public void Undirected_DfsEdgeTraversalIterative_Collar_From5()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();
        var expectedEdgeList = new List<(int From, int To, bool Forwards)>
        {
            (5, 0, true),
            (0, 1, true),
            (1, 3, true),
            (3, 6, true),
            (6, 1, true),
            (1, 6, false),
            (6, 4, true),
            (4, 6, false),
            (6, 3, false),
            (3, 1, false),
            (1, 4, true),
            (4, 1, false),
            (1, 6, true),
            (6, 1, false),
            (1, 0, false),
            (0, 2, true),
            (2, 5, true),
            (5, 2, false),
            (2, 0, false),
            (0, 3, true),
            (3, 0, false),
            (0, 5, false),
            (5, 2, true),
            (2, 5, false),
            (5, 3, true),
            (3, 5, false),
        };

        // Act
        var traverseList = graph.DfsEdgeTraversalIterative(5);

        // Assert
        Assert.Equal(expectedEdgeList, traverseList);
    }

    [Fact]
    public void Undirected_DfsEdgeTraversalIterative_InvalidVertex()
    {
        // Arrange
        var graph = new UndirectedGraph<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => graph.DfsEdgeTraversalIterative(0));
    }


    private static List<(int From, int To, bool Forward)> ExpectedEdgeList(List<int> expectedVertexList)
    {
        var stack = new Stack<int>();
        var expectedEdgeList = new List<(int From, int To, bool Forward)>();
        for (var i = 0; i < expectedVertexList.Count - 1; i++)
        {
            if (stack.Count == 0 || expectedVertexList[i + 1] != stack.Peek())
            {
                expectedEdgeList.Add((expectedVertexList[i], expectedVertexList[i + 1], true));
                stack.Push(expectedVertexList[i]);
            }
            else
            {
                expectedEdgeList.Add((expectedVertexList[i], expectedVertexList[i + 1], false));
                stack.Pop();
            }
        }

        return expectedEdgeList;
    }
}