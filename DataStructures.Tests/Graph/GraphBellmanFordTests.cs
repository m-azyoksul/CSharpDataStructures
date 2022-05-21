using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphBellmanFordTests
{
    [Fact]
    public void Directed_BellmanFord_Path_EquilateralTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(2);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Directed_BellmanFord_Full_EquilateralTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 2},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_DegradedTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 3);
        graph.AddEdge(3, 1, 1);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(4);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Directed_BellmanFord_Full_DegradedTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 3);
        graph.AddEdge(3, 1, 1);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 4},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_Square()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 4, 1);
        graph.AddEdge(4, 1, 1);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(2);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Directed_BellmanFord_Full_Square()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 4, 1);
        graph.AddEdge(4, 1, 1);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 2},
            {4, 3},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_NonConnected()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        graph.AddVertex(4);

        // Act
        var (distance, path) = graph.BellmanFord(1, 4);

        // Assert
        distance.Should().Be(double.NaN);
        path.Should().Equal();
    }

    [Fact]
    public void Directed_BellmanFord_Full_NonConnected()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        graph.AddVertex(4);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 2},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_NegativeWeight()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, -5);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 1, 2);

        // Act
        var (distance, path) = graph.BellmanFord(1, 2);

        // Assert
        distance.Should().Be(1);
        distance.Should().NotBe(-1);
        path.Should().Equal(1, 2);
    }

    [Fact]
    public void Directed_BellmanFord_Full_NegativeWeight()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, -5);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 1, 2);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, -4},
            {4, -2},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances[2].Should().NotBe(-1);
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_PentagonWithCenter()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 10);
        graph.AddEdge(2, 3, 9);
        graph.AddEdge(3, 4, 4);
        graph.AddEdge(4, 5, 0);
        graph.AddEdge(5, 1, 3);
        graph.AddEdge(0, 1, 1);
        graph.AddEdge(0, 2, 4);
        graph.AddEdge(0, 3, 16);
        graph.AddEdge(0, 4, 8);
        graph.AddEdge(0, 5, 6);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(19);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Directed_BellmanFord_Full_PentagonWithCenter()
    {
        // Arrange
        var graph = new DataStructures.Graph.DirectedGraph<int>();
        graph.AddEdge(1, 2, 10);
        graph.AddEdge(2, 3, 9);
        graph.AddEdge(3, 4, 4);
        graph.AddEdge(4, 5, 0);
        graph.AddEdge(5, 1, 3);
        graph.AddEdge(0, 1, 1);
        graph.AddEdge(0, 2, 4);
        graph.AddEdge(0, 3, 16);
        graph.AddEdge(0, 4, 8);
        graph.AddEdge(0, 5, 6);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 10},
            {3, 19},
            {4, 23},
            {5, 23},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var (distance, path) = graph.BellmanFord(0, 15);

        // Assert
        distance.Should().Be(6);
        path.Should().Equal(0, 1, 2, 3, 7, 11, 15);
    }

    [Fact]
    public void Directed_BellmanFord_Full_5By5Matrix()
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
        var distances = graph.BellmanFord(0);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Directed_BellmanFord_Path_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var (distance, path) = graph.BellmanFord(0, 7);

        // Assert
        distance.Should().Be(3);
        path.Should().Equal(0, 1, 3, 7);
    }

    [Fact]
    public void Directed_BellmanFord_Full_BalancedTree()
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
        var distances = graph.BellmanFord(0);

        // Assert
        distances.Should().Equal(expectedDistances);
    }


    [Fact]
    public void Undirected_BellmanFord_Path_EquilateralTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(1);
        path.Should().Equal(1, 3);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_EquilateralTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 1},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_DegradedTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 3);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(2);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_DegradedTriangle()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 3);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 2},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_Square()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 4, 1);
        graph.AddEdge(4, 1, 1);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(2);
        path.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_Square()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 4, 1);
        graph.AddEdge(4, 1, 1);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 2},
            {4, 1},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_NonConnected()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        graph.AddVertex(4);

        // Act
        var (distance, path) = graph.BellmanFord(1, 4);

        // Assert
        distance.Should().Be(double.NaN);
        path.Should().Equal();
    }

    [Fact]
    public void Undirected_BellmanFord_Full_NonConnected()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(3, 1, 1);
        graph.AddVertex(4);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, 0},
            {2, 1},
            {3, 1},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_NegativeWeight()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, -5);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 1, 2);

        // Act
        var (distance, path) = graph.BellmanFord(1, 2);

        // Assert
        distance.Should().Be(double.NegativeInfinity);
        path.Should().Equal();
    }

    [Fact]
    public void Undirected_BellmanFord_Full_NegativeWeight()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(2, 3, -5);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 1, 2);
        var expectedDistances = new Dictionary<int, double>
        {
            {1, double.NegativeInfinity},
            {2, double.NegativeInfinity},
            {3, double.NegativeInfinity},
            {4, double.NegativeInfinity},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances[2].Should().NotBe(-1);
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_PentagonWithCenter()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 10);
        graph.AddEdge(2, 3, 9);
        graph.AddEdge(3, 4, 4);
        graph.AddEdge(4, 5, 0);
        graph.AddEdge(5, 1, 3);
        graph.AddEdge(0, 1, 1);
        graph.AddEdge(0, 2, 4);
        graph.AddEdge(0, 3, 16);
        graph.AddEdge(0, 4, 8);
        graph.AddEdge(0, 5, 6);

        // Act
        var (distance, path) = graph.BellmanFord(1, 3);

        // Assert
        distance.Should().Be(7);
        path.Should().Equal(1, 5, 4, 3);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_PentagonWithCenter()
    {
        // Arrange
        var graph = new DataStructures.Graph.UndirectedGraph<int>();
        graph.AddEdge(1, 2, 10);
        graph.AddEdge(2, 3, 9);
        graph.AddEdge(3, 4, 4);
        graph.AddEdge(4, 5, 0);
        graph.AddEdge(5, 1, 3);
        graph.AddEdge(0, 1, 1);
        graph.AddEdge(0, 2, 4);
        graph.AddEdge(0, 3, 16);
        graph.AddEdge(0, 4, 8);
        graph.AddEdge(0, 5, 6);
        var expectedDistances = new Dictionary<int, double>
        {
            {0, 1},
            {1, 0},
            {2, 5},
            {3, 7},
            {4, 3},
            {5, 3},
        };

        // Act
        var distances = graph.BellmanFord(1);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var (distance, path) = graph.BellmanFord(0, 15);

        // Assert
        distance.Should().Be(6);
        // The path is seemingly random because of the usage of min heap in the implementation
        path.Should().Equal(0, 1, 2, 3, 7, 11, 15);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();
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
        var distances = graph.BellmanFord(0);

        // Assert
        distances.Should().Equal(expectedDistances);
    }

    [Fact]
    public void Undirected_BellmanFord_Path_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var (distance, path) = graph.BellmanFord(0, 7);

        // Assert
        distance.Should().Be(3);
        path.Should().Equal(0, 1, 3, 7);
    }

    [Fact]
    public void Undirected_BellmanFord_Full_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();
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
        var distances = graph.BellmanFord(0);

        // Assert
        distances.Should().Equal(expectedDistances);
    }
}