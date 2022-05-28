using DataStructures.List;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.List;

public class SortedListTests
{
    [Fact]
    public void Should_Add_Sorted_Items()
    {
        // Arrange
        var list = new SortedList<int>();

        // Act
        list.InsertSorted(5);
        list.InsertSorted(2);
        list.InsertSorted(3);
        list.InsertSorted(1);
        list.InsertSorted(4);

        // Assert
        list.Count.Should().Be(5);
        list.Should().Equal(1, 2, 3, 4, 5);
    }
}