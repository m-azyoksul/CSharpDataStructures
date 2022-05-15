using AlgorithmHelpers.DataStructures;
using FluentAssertions;
using Xunit;

namespace AlgorithmHelpers.Tests.DataStructures;

public class MinHeapTests
{
    [Fact]
    public void Add_Pop()
    {
        // Arrange
        var heap = new MinHeap<int, int>
        {
            (1, 1),
            (2, 2),
            (3, 3)
        };

        // Assert
        heap.Peek().Key.Should().Be(1);
        heap.Peek().Value.Should().Be(1);
        var item1 = heap.Pop();
        item1.Key.Should().Be(1);
        item1.Value.Should().Be(1);
        var item2 = heap.Pop();
        item2.Key.Should().Be(2);
        item2.Value.Should().Be(2);
        var item3 = heap.Pop();
        item3.Key.Should().Be(3);
        item3.Value.Should().Be(3);
    }


    [Fact]
    public void Add_Pop_Many()
    {
        // Arrange
        var heap = new MinHeap<int, int>
        {
            (3, 3),
            (2, 2),
            (1, 1)
        };

        // Assert
        heap.Peek().Key.Should().Be(1);
        heap.Peek().Value.Should().Be(1);
        var item1 = heap.Pop();
        item1.Key.Should().Be(1);
        item1.Value.Should().Be(1);
        var item2 = heap.Pop();
        item2.Key.Should().Be(2);
        item2.Value.Should().Be(2);
        var item3 = heap.Pop();
        item3.Key.Should().Be(3);
        item3.Value.Should().Be(3);
    }
}