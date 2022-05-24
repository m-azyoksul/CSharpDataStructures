using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphArticulationPointsTests
{
    [Fact]
    public void Undirected_ArticulationPoints_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Undirected5By5Matrix();

        // Act
        var articulationPoints = graph.ArticulationPoints();

        // Assert
        articulationPoints.Count.Should().Be(0);
    }

    [Fact]
    public void Undirected_ArticulationPoints_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedBalancedTree();

        // Act
        var articulationPoints = graph.ArticulationPoints();

        // Assert
        articulationPoints.Should().Equal(3, 1, 4, 5, 2, 0);
    }

    [Fact]
    public void Undirected_ArticulationPoints_Collar()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedCollar();

        // Act
        var articulationPoints = graph.ArticulationPoints();

        // Assert
        articulationPoints.Count.Should().Be(0);
    }

    [Fact]
    public void Undirected_ArticulationPoints_DoubleTick()
    {
        // Arrange
        var graph = GraphsToTest.UndirectedDoubleTick();

        // Act
        var articulationPoints = graph.ArticulationPoints();

        // Assert
        articulationPoints.Should().Equal(1, 0, 5, 4);
    }
}