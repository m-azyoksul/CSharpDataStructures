using System;
using DataStructures.Heap;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.Heap;

public class IndexedHeapTests
{
    [Fact]
    public void IndexedMinHeap_Add_Should_Add_And_Pop_Should_Pop()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (3, 3),
            (2, 2),
            (1, 1),
        };

        // Act & Assert
        heap.Should().Equal((1, 1), (3, 3), (2, 2));
        heap.ContainsKey(1).Should().BeTrue();
        heap.ContainsKey(2).Should().BeTrue();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(1).Should().Be(0);
        heap.IndexOfKey(2).Should().Be(2);
        heap.IndexOfKey(3).Should().Be(1);
        heap.ValueOfKey(1).Should().Be(1);
        heap.ValueOfKey(2).Should().Be(2);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((1, 1));
    }

    [Fact]
    public void IndexedMinHeap_Add_Should_Throw_On_Duplicate()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.Add((0, 1));

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_Pop_Should_Pop()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (3, 3),
            (2, 2),
            (1, 1),
        };

        // Assert
        var item1 = heap.Pop();
        item1.Should().Be((1, 1));

        heap.Should().Equal((2, 2), (3, 3));
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

        heap.Should().Equal((3, 3));
        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(3).Should().Be(0);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((3, 3));

        var item3 = heap.Pop();
        item3.Should().Be((3, 3));

        heap.Should().Equal();
        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeFalse();
    }

    [Fact]
    public void IndexedMinHeap_Pop_Should_Throw_On_Empty()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>();

        // Act
        var action = () => heap.Pop();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_UpdateKey_Should_Update_Key()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (5, 5),
            (4, 4),
            (3, 3),
            (2, 2),
            (1, 1),
            (0, 0),
        };

        // Act
        heap.UpdateKey(3, -1);

        // Assert
        heap.ContainsKey(3).Should().BeTrue();
        heap.ValueOfKey(3).Should().Be(-1);
        heap.Should().Equal((3, -1), (0, 0), (1, 1), (5, 5), (2, 2), (4, 4));

        // Act
        heap.UpdateKey(4, 0);

        // Assert
        heap.ContainsKey(4).Should().BeTrue();
        heap.ValueOfKey(4).Should().Be(0);
        heap.Should().Equal((3, -1), (0, 0), (4, 0), (5, 5), (2, 2), (1, 1));

        // Act
        heap.UpdateKey(3, 6);

        // Assert
        heap.ContainsKey(3).Should().BeTrue();
        heap.ValueOfKey(3).Should().Be(6);
        heap.Should().Equal((0, 0), (2, 2), (4, 0), (5, 5), (3, 6), (1, 1));
    }

    [Fact]
    public void IndexedMinHeap_UpdateKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.UpdateKey(-1, 0);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_RemoveKey_Should_Remove_Key()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int>
        {
            (5, 5),
            (4, 4),
            (3, 3),
            (2, 2),
            (1, 1),
            (0, 0),
        };

        // Act
        heap.RemoveKey(3);

        // Assert
        heap.ContainsKey(3).Should().BeFalse();
        heap.Should().Equal((0, 0), (2, 2), (1, 1), (5, 5), (4, 4));

        // Act
        heap.RemoveKey(0);

        // Assert
        heap.ContainsKey(0).Should().BeFalse();
        heap.Should().Equal((1, 1), (2, 2), (4, 4), (5, 5));

        // Act
        heap.RemoveKey(5);

        // Assert
        heap.ContainsKey(5).Should().BeFalse();
        heap.Should().Equal((1, 1), (2, 2), (4, 4));
    }

    [Fact]
    public void IndexedMinHeap_RemoveKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.RemoveKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_IndexOfKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.IndexOfKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_ValueOfKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMinHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.ValueOfKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMinHeap_Should_Work()
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

        heap.IndexOfKey(6).Should().Be(0);
        heap.IndexOfKey(3).Should().Be(1);

        heap.ValueOfKey(6).Should().Be(6);
        heap.ValueOfKey(3).Should().Be(10);
    }


    [Fact]
    public void IndexedMaxHeap_Add_Should_Add_And_Pop_Should_Pop()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int>
        {
            (1, 1),
            (2, 2),
            (3, 3),
        };

        // Act & Assert
        heap.Should().Equal((3, 3), (1, 1), (2, 2));
        heap.ContainsKey(1).Should().BeTrue();
        heap.ContainsKey(2).Should().BeTrue();
        heap.ContainsKey(3).Should().BeTrue();
        heap.IndexOfKey(1).Should().Be(1);
        heap.IndexOfKey(2).Should().Be(2);
        heap.IndexOfKey(3).Should().Be(0);
        heap.ValueOfKey(1).Should().Be(1);
        heap.ValueOfKey(2).Should().Be(2);
        heap.ValueOfKey(3).Should().Be(3);
        heap.Peek().Should().Be((3, 3));
    }

    [Fact]
    public void IndexedMaxHeap_Add_Should_Throw_On_Duplicate()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.Add((0, 1));

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMaxHeap_Pop_Should_Pop()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int>
        {
            (1, 1),
            (2, 2),
            (3, 3),
        };

        // Assert
        var item1 = heap.Pop();
        item1.Should().Be((3, 3));

        heap.Should().Equal((2, 2), (1, 1));
        heap.ContainsKey(1).Should().BeTrue();
        heap.ContainsKey(2).Should().BeTrue();
        heap.ContainsKey(3).Should().BeFalse();
        heap.IndexOfKey(1).Should().Be(1);
        heap.IndexOfKey(2).Should().Be(0);
        heap.ValueOfKey(1).Should().Be(1);
        heap.ValueOfKey(2).Should().Be(2);
        heap.Peek().Should().Be((2, 2));

        var item2 = heap.Pop();
        item2.Should().Be((2, 2));

        heap.Should().Equal((1, 1));
        heap.ContainsKey(1).Should().BeTrue();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeFalse();
        heap.IndexOfKey(1).Should().Be(0);
        heap.ValueOfKey(1).Should().Be(1);
        heap.Peek().Should().Be((1, 1));

        var item3 = heap.Pop();
        item3.Should().Be((1, 1));

        heap.Should().Equal();
        heap.ContainsKey(1).Should().BeFalse();
        heap.ContainsKey(2).Should().BeFalse();
        heap.ContainsKey(3).Should().BeFalse();
    }

    [Fact]
    public void IndexedMaxHeap_Pop_Should_Throw_On_Empty()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int>();

        // Act
        var action = () => heap.Pop();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMaxHeap_UpdateKey_Should_Update_Key()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int>
        {
            (0, 0),
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4),
            (5, 5),
        };

        // Act
        heap.UpdateKey(3, -1);

        // Assert
        heap.ContainsKey(3).Should().BeTrue();
        heap.ValueOfKey(3).Should().Be(-1);
        heap.Should().Equal((5, 5), (2, 2), (4, 4), (0, 0), (3, -1), (1, 1));

        // Act
        heap.UpdateKey(4, 6);

        // Assert
        heap.ContainsKey(4).Should().BeTrue();
        heap.ValueOfKey(4).Should().Be(6);
        heap.Should().Equal((4, 6), (2, 2), (5, 5), (0, 0), (3, -1), (1, 1));

        // Act
        heap.UpdateKey(0, 7);

        // Assert
        heap.ContainsKey(0).Should().BeTrue();
        heap.ValueOfKey(0).Should().Be(7);
        heap.Should().Equal((0, 7), (4, 6), (5, 5), (2, 2), (3, -1), (1, 1));
    }

    [Fact]
    public void IndexedMaxHeap_UpdateKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.UpdateKey(-1, 0);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMaxHeap_RemoveKey_Should_Remove_Key()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int>
        {
            (0, 0),
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4),
            (5, 5),
        };

        // Act
        heap.RemoveKey(3);

        // Assert
        heap.ContainsKey(3).Should().BeFalse();
        heap.Should().Equal((5, 5), (2, 2), (4, 4), (0, 0), (1, 1));

        // Act
        heap.RemoveKey(0);

        // Assert
        heap.ContainsKey(0).Should().BeFalse();
        heap.Should().Equal((5, 5), (2, 2), (4, 4), (1, 1));

        // Act
        heap.RemoveKey(5);

        // Assert
        heap.ContainsKey(5).Should().BeFalse();
        heap.Should().Equal((4, 4), (2, 2), (1, 1));
    }

    [Fact]
    public void IndexedMaxHeap_RemoveKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.RemoveKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMaxHeap_IndexOfKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.IndexOfKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IndexedMaxHeap_ValueOfKey_Should_Throw_On_Absent()
    {
        // Arrange
        var heap = new IndexedMaxHeap<int, int> {(0, 0), (1, 1), (2, 2), (3, 3)};

        // Act
        var action = () => heap.ValueOfKey(-1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}