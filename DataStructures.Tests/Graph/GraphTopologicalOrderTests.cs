using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphTopologicalOrderTests
{
    [Fact]
    public void TopologicalOrder_EmptyGraph_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();

        // Act
        var topologicalOrder = graph.TopologicalOrder();

        // Assert
        topologicalOrder.Should().BeEmpty();
    }

    [Fact]
    public void TopologicalOrder_GraphWithOneNode_ReturnsListWithOneNode()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddVertex(1);

        // Act
        var topologicalOrder = graph.TopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(1);
    }


    [Fact]
    public void TopologicalOrder_BalancedTree_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var topologicalOrder = graph.TopologicalOrder(2);

        // Assert
        topologicalOrder.Should().Equal(2, 6, 5, 12, 11);
    }

    [Fact]
    public void TopologicalOrder_BalancedTree_Backwards_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder(2);

        // Assert
        topologicalOrder.Should().Equal(0, 2);
    }

    [Fact]
    public void TopologicalOrder_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var topologicalOrder = graph.TopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(0, 2, 6, 5, 12, 11, 1, 4, 10, 9, 3, 8, 7);
    }

    [Fact]
    public void TopologicalOrder_BalancedTree_Backwards()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
    }


    [Fact]
    public void TopologicalOrder_5By5Matrix_From9()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var topologicalOrder = graph.TopologicalOrder(9);

        // Assert
        topologicalOrder.Should().Equal(9, 13, 10, 14, 11, 15);
    }

    [Fact]
    public void TopologicalOrder_5By5Matrix_Backwards_From9()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder(9);

        // Assert
        topologicalOrder.Should().Equal(0, 1, 4, 5, 8, 9);
    }

    [Fact]
    public void TopologicalOrder_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var topologicalOrder = graph.TopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);
    }

    [Fact]
    public void TopologicalOrder_5By5Matrix_Backwards()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
    }

    [Fact]
    public void TopologicalOrder_DoubleTick_From1()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();

        // Act
        var topologicalOrder = graph.TopologicalOrder(1);

        // Assert
        topologicalOrder.Should().Equal(1, 2);
    }

    [Fact]
    public void TopologicalOrder_DoubleTick_Backwards_From1()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder(1);

        // Assert
        topologicalOrder.Should().Equal(0, 1);
    }

    [Fact]
    public void TopologicalOrder_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();

        // Act
        var topologicalOrder = graph.TopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(4, 7, 5, 6, 0, 3, 1, 2);
    }

    [Fact]
    public void TopologicalOrder_DoubleTick_Backwards()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();

        // Act
        var topologicalOrder = graph.BackwardsTopologicalOrder();

        // Assert
        topologicalOrder.Should().Equal(0, 1, 2, 3, 4, 5, 6, 7);
    }

    //////////////////////////////////////// Sort ////////////////////////////////////////

    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedDistances = new Dictionary<int, double>
        {
            {0, 0},
            {1, 1},
            {2, 1},
            {3, 2},
            {4, 2},
            {5, 2},
            {6, 2},
            {7, 3},
            {8, 3},
            {9, 3},
            {10, 3},
            {11, 3},
            {12, 3},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath();

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }

    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_BalancedTree_From2()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();
        var expectedDistances = new Dictionary<int, double>
        {
            {2, 0},
            {5, 1},
            {6, 1},
            {11, 2},
            {12, 2},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath(2);

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }


    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_5By5Matrix_From9()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedDistances = new Dictionary<int, double>
        {
            {9, 0},
            {13, 1},
            {10, 1},
            {14, 2},
            {11, 2},
            {15, 3},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath(9);

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }

    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();
        var expectedDistances = new Dictionary<int, double>
        {
            {0, 0},
            {1, 1},
            {2, 2},
            {3, 3},
            {4, 1},
            {5, 2},
            {6, 3},
            {7, 4},
            {8, 2},
            {9, 3},
            {10, 4},
            {11, 5},
            {12, 3},
            {13, 4},
            {14, 5},
            {15, 6},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath();

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }


    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_DoubleTick_From1()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath(1);

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }

    [Fact]
    public void TopologicalOrderSingleSourceShortestPath_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.DirectedDoubleTick();
        var expectedDistances = new Dictionary<int, double>
        {
            {0, 0},
            {1, 1},
            {2, 2},
            {3, 1},
            {4, 0},
            {5, 1},
            {6, 2},
            {7, 1},
        };

        // Act
        var topologicalOrder = graph.TopologicalOrderSingleSourceShortestPath();

        // Assert
        topologicalOrder.Should().Equal(expectedDistances);
    }
}