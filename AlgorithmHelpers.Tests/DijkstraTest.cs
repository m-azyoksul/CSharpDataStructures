using System.Collections.Generic;
using GraphQuiz1;
using Xunit;

namespace Graph.Tests;

public class DijkstraTest
{
    [Fact]
    public void Directed_Dijkstra_Full_DegradedTriangle()
    {
        // Arrange
        var vertexList = new List<List<(int To, int Weight)>>
        {
            new() {(1, 1), (2, 3)},
            new() {(2, 1)},
            new(),
        };

        // Act
        var distances = vertexList.Dijkstra(0);

        // Assert
        Assert.Equal(new[] {0, 1, 2}, distances);
    }

    [Fact]
    public void Directed_Dijkstra_Path_PentagonWithCenter()
    {
        // Arrange
        var vertexList = new List<List<(int To, int Weight)>>
        {
            new() {(1, 1), (2, 4), (3, 16), (4, 8), (5, 16)},
            new() {(2, 10)},
            new() {(3, 9)},
            new() {(4, 4)},
            new() {(5, 0)},
            new() {(1, 3)},
        };

        // Act
        var distances = vertexList.Dijkstra(1);

        // Assert
        Assert.Equal(new[] {-1, 0, 10, 19, 23, 23}, distances);
    }
}