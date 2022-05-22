using System;
using DataStructures.Heap;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.Heap;

public class HeapTests
{
    [Fact]
    public void MinHeap_Add_Should_Add()
    {
        // Arrange & Act
        var heap = new MinHeap<int> {9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

        // Assert
        heap.Count.Should().Be(10);
        heap.Peek().Should().Be(0);
        heap.Should().Equal(0, 1, 4, 3, 2, 8, 5, 9, 6, 7);
    }

    [Fact]
    public void MaxHeap_Add_Should_Add()
    {
        // Arrange & Act
        var heap = new MaxHeap<int> {0, 1, 2, 3, 4, 5};

        // Assert
        heap.Count.Should().Be(6);
        heap.Peek().Should().Be(5);
        heap.Should().Equal(5, 3, 4, 0, 2, 1);
    }

    [Fact]
    public void MinHeap_Pop_Should_Pop()
    {
        // Arrange
        var heap = new MinHeap<int> {0, 1, 2};

        // Act
        var result0 = heap.Pop();
        var result1 = heap.Pop();
        var result2 = heap.Pop();

        // Assert
        result0.Should().Be(0);
        result1.Should().Be(1);
        result2.Should().Be(2);
        heap.Count.Should().Be(0);
    }

    [Fact]
    public void MaxHeap_Pop_Should_Pop()
    {
        // Arrange
        var heap = new MaxHeap<int> {0, 1, 2};

        // Act
        var result0 = heap.Pop();
        var result1 = heap.Pop();
        var result2 = heap.Pop();

        // Assert
        result0.Should().Be(2);
        result1.Should().Be(1);
        result2.Should().Be(0);
        heap.Count.Should().Be(0);
    }

    [Fact]
    public void MinHeap_Pop_Should_Throw_When_Empty()
    {
        // Arrange
        var heap = new MinHeap<int>();

        // Act
        Action action = () => heap.Pop();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MaxHeap_Pop_Should_Throw_When_Empty()
    {
        // Arrange
        var heap = new MaxHeap<int>();

        // Act
        Action action = () => heap.Pop();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MinHeap_Peek_Should_Throw_When_Empty()
    {
        // Arrange
        var heap = new MinHeap<int>();

        // Act
        Action action = () => heap.Peek();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MaxHeap_Peek_Should_Throw_When_Empty()
    {
        // Arrange
        var heap = new MaxHeap<int>();

        // Act
        Action action = () => heap.Peek();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MinHeap_Remove_Should_Remove()
    {
        // Arrange
        var heap = new MinHeap<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        // Act
        heap.Remove(5);

        // Assert
        heap.Count.Should().Be(9);
        heap.Should().Equal(0, 1, 2, 3, 4, 9, 6, 7, 8);

        // Act
        heap.Remove(1);

        // Assert
        heap.Count.Should().Be(8);
        heap.Should().Equal(0, 3, 2, 7, 4, 9, 6, 8);

        // Act
        heap.Remove(0);

        // Assert
        heap.Count.Should().Be(7);
        heap.Should().Equal(2, 3, 6, 7, 4, 9, 8);

        // Act
        heap.Remove(8);

        // Assert
        heap.Count.Should().Be(6);
        heap.Should().Equal(2, 3, 6, 7, 4, 9);
    }

    [Fact]
    public void MaxHeap_Remove_Should_Remove()
    {
        // Arrange
        var heap = new MaxHeap<int> {9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

        // Act
        heap.Remove(5);

        // Assert
        heap.Count.Should().Be(9);
        heap.Should().Equal(9, 8, 7, 6, 0, 4, 3, 2, 1);

        // Act
        heap.Remove(0);

        // Assert
        heap.Count.Should().Be(8);
        heap.Should().Equal(9, 8, 7, 6, 1, 4, 3, 2);

        // Act
        heap.Remove(8);

        // Assert
        heap.Count.Should().Be(7);
        heap.Should().Equal(9, 6, 7, 2, 1, 4, 3);

        // Act
        heap.Remove(9);

        // Assert
        heap.Count.Should().Be(6);
        heap.Should().Equal(7, 6, 4, 2, 1, 3);
    }

    [Fact]
    public void MinHeap_Remove_Should_Throw_When_Not_Found()
    {
        // Arrange
        var heap = new MinHeap<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        // Act
        var action = () => heap.Remove(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MaxHeap_Remove_Should_Throw_When_Not_Found()
    {
        // Arrange
        var heap = new MaxHeap<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        // Act
        var action = () => heap.Remove(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}