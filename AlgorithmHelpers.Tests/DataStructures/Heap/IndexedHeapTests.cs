using AlgorithmHelpers.DataStructures;
using FluentAssertions;
using Xunit;

namespace AlgorithmHelpers.Tests.DataStructures;

public class IndexedHeapTests
{
    [Fact]
    public void IndexedMinHeap_AddPeekPop()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (1, 1),
            (2, 2),
            (3, 3)
        };

        // Act & Assert
        heap.ContainsKey(1).Should().BeTrue();
        heap.ContainsKey(2).Should().BeTrue();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(1).Should().Be(0);
        heap.IndexOfKey(2).Should().Be(1);
        heap.IndexOfKey(3).Should().Be(2);
        heap.ValueOfKey(1).Should().Be(1);
        heap.ValueOfKey(2).Should().Be(2);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((1, 1));

        var item1 = heap.Pop();
        item1.Should().Be((1, 1));

        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeTrue();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(2).Should().Be(0);
        heap.IndexOfKey(3).Should().Be(1);
        heap.ValueOfKey(2).Should().Be(2);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((2, 2));

        var item2 = heap.Pop();
        item2.Should().Be((2, 2));

        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(3).Should().Be(0);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((3, 3));

        var item3 = heap.Pop();
        item3.Should().Be((3, 3));

        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeFalse();
    }

    [Fact]
    public void IndexedMinHeap_AddPop2()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (3, 3),
            (2, 2),
            (1, 1)
        };

        // Assert
        heap.Peek().Should().Be((1, 1));
        var item1 = heap.Pop();
        item1.Should().Be((1, 1));
        var item2 = heap.Pop();
        item2.Should().Be((2, 2));
        var item3 = heap.Pop();
        item3.Should().Be((3, 3));
    }

    [Fact]
    public void IndexedMinHeap_AddPopUpdate()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4),
            (5, 5),
            (6, 6),
        };

        // Assert
        var item1 = heap.Pop();
        item1.Should().Be((1, 1));

        heap.UpdateKey(2, -2);

        var item2 = heap.Pop();
        item2.Should().Be((2, -2));

        heap.UpdateKey(3, 10);
        heap.Add(7, 0);
        heap.UpdateKey(4, -4);

        var item3 = heap.Pop();
        item3.Should().Be((4, -4));

        var item4 = heap.Pop();
        item4.Should().Be((7, 0));
        
        heap.RemoveKey(5);

        heap.IndexOfKey(5).Should().Be(0);
        heap.IndexOfKey(6).Should().Be(1);
        heap.IndexOfKey(3).Should().Be(2);

        heap.ValueOfKey(5).Should().Be(5);
        heap.ValueOfKey(6).Should().Be(6);
        heap.ValueOfKey(3).Should().Be(10);
    }
}