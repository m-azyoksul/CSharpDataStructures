using System;
using System.Collections.Generic;
using AlgorithmHelpers.Algorithms.Graph;
using Xunit;

namespace AlgorithmHelpers.Tests.Algorithms.Graph;

public class TarjanSccHelperTests
{
    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 8; i++)
            graph.Add(new());

        graph[6].Add(0);
        graph[6].Add(2);
        graph[3].Add(4);
        graph[6].Add(4);
        graph[2].Add(0);
        graph[0].Add(1);
        graph[4].Add(5);
        graph[5].Add(6);
        graph[3].Add(7);
        graph[7].Add(5);
        graph[1].Add(2);
        graph[7].Add(3);
        graph[5].Add(0);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(3, sccCount);
        Assert.Equal(new List<int> {0, 0, 0, 2, 1, 1, 1, 2}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {0, 1, 2},
            new() {4, 5, 6},
            new() {3, 7},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc_2()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 2; i++)
            graph.Add(new());

        graph[0].Add(1);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(2, sccCount);
        Assert.Equal(new List<int> {1, 0}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {1},
            new() {0},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc_3()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 2; i++)
            graph.Add(new());

        graph[0].Add(1);
        graph[1].Add(0);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(1, sccCount);
        Assert.Equal(new List<int> {0, 0}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {0, 1},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc_4()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 5; i++)
            graph.Add(new());

        graph[0].Add(1);
        graph[1].Add(0);
        graph[1].Add(2);
        graph[2].Add(3);
        graph[3].Add(1);
        graph[4].Add(0);
        graph[4].Add(3);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(2, sccCount);
        Assert.Equal(new List<int> {0, 0, 0, 0, 1}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {0, 1, 2, 3},
            new() {4},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc_5()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 6; i++)
            graph.Add(new());

        graph[0].Add(2);
        graph[0].Add(5);
        graph[2].Add(1);
        graph[2].Add(3);
        graph[2].Add(5);
        graph[3].Add(0);
        graph[3].Add(1);
        graph[3].Add(2);
        graph[4].Add(5);
        graph[5].Add(1);
        graph[5].Add(4);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(3, sccCount);
        Assert.Equal(new List<int> {2, 0, 2, 2, 1, 1}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {1},
            new() {4, 5},
            new() {0, 2, 3},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectScc_6()
    {
        // Arrange
        var graph = new List<List<int>>();
        for (int i = 0; i < 6; i++)
            graph.Add(new());

        graph[0].Add(1);
        graph[1].Add(2);
        graph[2].Add(3);
        graph[3].Add(4);
        graph[4].Add(5);
        graph[5].Add(0);

        // Act
        var (sccCount, sccArray) = graph.TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(1, sccCount);
        Assert.Equal(new List<int> {0, 0, 0, 0, 0, 0}, sccArray);
        Assert.Equal(new List<List<int>>
        {
            new() {0, 1, 2, 3, 4, 5},
        }, sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsCorrectEmptyInput()
    {
        // Act
        var (sccCount, sccArray) = new List<List<int>>().TarjanSccSolve();
        var sccList = (sccCount, sccArray).TarjanSccToList();

        // Assert
        Assert.Equal(0, sccCount);
        Assert.Equal(new List<int>(), sccArray);
        Assert.Equal(new List<List<int>>(), sccList);
    }

    [Fact]
    public void TarjanSccHelper_GetScc_ReturnsErrorOnNullInput()
    {
        // Act
        var exception = Record.Exception(() => TarjanSccHelper.TarjanSccSolve(null));

        // Assert
        Assert.IsType<NullReferenceException>(exception);
    }
}