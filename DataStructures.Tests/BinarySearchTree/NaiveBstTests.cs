using System;
using DataStructures.BinarySearchTree;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.BinarySearchTree;

public class NaiveBstTests
{
    [Fact]
    public void Should_Work()
    {
        // Arrange
        var naiveBst = new NaiveBst<int>();
        
        // Act & Assert
        naiveBst.Contains(0).Should().BeFalse();
        naiveBst.Remove(0).Should().BeFalse();
        Action popMinFromEmpty = () => naiveBst.PopMin();
        popMinFromEmpty.Should().Throw<InvalidOperationException>();
        Action popMaxFromEmpty = () => naiveBst.PopMax();
        popMaxFromEmpty.Should().Throw<InvalidOperationException>();
        Action minFromEmpty = () => naiveBst.Min();
        minFromEmpty.Should().Throw<InvalidOperationException>();
        Action maxFromEmpty = () => naiveBst.Max();
        maxFromEmpty.Should().Throw<InvalidOperationException>();
        
        naiveBst.Add(3);
        naiveBst.Contains(3).Should().BeTrue();
        naiveBst.Add(5);
        naiveBst.Add(4);
        naiveBst.Add(8);
        naiveBst.Add(7);
        naiveBst.Add(6);
        naiveBst.Add(2);
        naiveBst.Add(1);

        naiveBst.Min().Should().Be(1);
        naiveBst.Max().Should().Be(8);
        naiveBst.Contains(1).Should().BeTrue();
        naiveBst.Contains(6).Should().BeTrue();
        naiveBst.Contains(0).Should().BeFalse();
        naiveBst.Contains(9).Should().BeFalse();
        naiveBst.ToSortedList().Should().Equal(1, 2, 3, 4, 5, 6, 7, 8);
        naiveBst.Height().Should().Be(5);

        naiveBst.Remove(0).Should().BeFalse();
        naiveBst.Remove(9).Should().BeFalse();
        naiveBst.Remove(5).Should().BeTrue();
        naiveBst.Remove(3).Should().BeTrue();
        naiveBst.Remove(2).Should().BeTrue();
        naiveBst.Remove(1).Should().BeTrue();

        naiveBst.PopMax().Should().Be(8);
        naiveBst.PopMin().Should().Be(4);
        naiveBst.PopMin().Should().Be(6);
        naiveBst.PopMax().Should().Be(7);
        
        naiveBst.IsEmpty().Should().BeTrue();
        
        naiveBst.Add(0);
        naiveBst.Clear();
        naiveBst.IsEmpty().Should().BeTrue();
    }
}