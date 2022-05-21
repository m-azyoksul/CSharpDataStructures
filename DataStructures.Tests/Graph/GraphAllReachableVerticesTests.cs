using FluentAssertions;
using Xunit;

namespace Graph.Tests;

public class GraphAllReachableVerticesTests
{
    [Fact]
    public void AllReachableVertices_BalancedTree()
    {
        // Arrange
        var graph = GraphsToTest.DirectedBalancedTree();

        // Act
        var result = graph.AllReachableVertices();

        // Assert
        result.Should().ContainKey(0);
        result[0].Should().BeEquivalentTo(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12});

        result.Should().ContainKey(1);
        result[1].Should().BeEquivalentTo(new[] {1, 3, 4, 7, 8, 9, 10});

        result.Should().ContainKey(4);
        result[4].Should().BeEquivalentTo(new[] {4, 9, 10});

        result.Should().ContainKey(5);
        result[5].Should().BeEquivalentTo(new[] {5, 11, 12});
    }

    [Fact]
    public void AllReachableVertices_5By5Matrix()
    {
        // Arrange
        var graph = GraphsToTest.Directed5By5Matrix();

        // Act
        var result = graph.AllReachableVertices();

        // Assert
        result.Should().ContainKey(0);
        result[0].Should().BeEquivalentTo(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15});

        result.Should().ContainKey(3);
        result[3].Should().BeEquivalentTo(new[] {3, 7, 11, 15});

        result.Should().ContainKey(6);
        result[6].Should().BeEquivalentTo(new[] {6, 7, 10, 11, 14, 15});
    }
}