using System;
using AlgorithmHelpers.DataStructures;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.DataStructures.BinarySearchTree;

public class AvlTreeTests
{
    [Fact]
    public void Should_Rotate_Left_On_Add()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(1);
        var min1 = tree.Min();
        var max1 = tree.Max();

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(1);
        tree.Root.Left.Should().BeNull();
        tree.Root.Right.Should().BeNull();
        tree.Root.Height.Should().Be(1);
        min1.Should().Be(1);
        max1.Should().Be(1);

        // Act
        tree.Add(2);
        var min2 = tree.Min();
        var max2 = tree.Max();

        // Assert
        tree.Root.Value.Should().Be(1);
        tree.Root.Left.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(2);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Height.Should().Be(1);
        tree.Root.Height.Should().Be(2);
        min2.Should().Be(1);
        max2.Should().Be(2);

        // Act
        tree.Add(3);
        var min3 = tree.Min();
        var max3 = tree.Max();

        // Assert
        tree.Root.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Height.Should().Be(1);
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Height.Should().Be(1);
        tree.Root.Height.Should().Be(2);
        min3.Should().Be(1);
        max3.Should().Be(3);
    }

    [Fact]
    public void Should_Rotate_Right_On_Add()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(3);
        var min1 = tree.Min();
        var max1 = tree.Max();

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(3);
        tree.Root.Left.Should().BeNull();
        tree.Root.Right.Should().BeNull();
        tree.Root.Height.Should().Be(1);
        min1.Should().Be(3);
        max1.Should().Be(3);

        // Act
        tree.Add(2);
        var min2 = tree.Min();
        var max2 = tree.Max();

        // Assert
        tree.Root.Value.Should().Be(3);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Height.Should().Be(1);
        tree.Root.Right.Should().BeNull();
        tree.Root.Height.Should().Be(2);
        min2.Should().Be(2);
        max2.Should().Be(3);

        // Act
        tree.Add(1);
        var min3 = tree.Min();
        var max3 = tree.Max();

        // Assert
        tree.Root.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Height.Should().Be(1);
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Height.Should().Be(1);
        tree.Root.Height.Should().Be(2);
        min3.Should().Be(1);
        max3.Should().Be(3);
    }

    [Fact]
    public void Should_Rotate_Left_Right_On_Add()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(4);
        tree.Add(1);
        tree.Add(5);
        tree.Add(0);
        tree.Add(2);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(4);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(2);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(5);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(3);

        // Act
        tree.Add(3);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(5);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(3);
    }

    [Fact]
    public void Should_Rotate_Right_Left_On_Add()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(1);
        tree.Add(0);
        tree.Add(4);
        tree.Add(2);
        tree.Add(5);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(1);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(2);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(5);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(3);

        // Act
        tree.Add(3);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(5);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
    }

    [Fact]
    public void Should_Not_Rotate_Then_Rotate_Left_On_PopMin()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(5);
        tree.Add(2);
        tree.Add(6);
        tree.Add(0);
        tree.Add(3);
        tree.Add(7);
        tree.Add(1);
        tree.Add(4);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(5);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().NotBeNull();
        tree.Root.Left.Left.Right!.Value.Should().Be(1);
        tree.Root.Left.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(3);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().NotBeNull();
        tree.Root.Left.Right.Right!.Value.Should().Be(4);
        tree.Root.Left.Right.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(6);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(4);

        // Act
        var min1 = tree.PopMin();

        // Assert
        min1.Should().Be(0);
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(5);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(3);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().NotBeNull();
        tree.Root.Left.Right.Right!.Value.Should().Be(4);
        tree.Root.Left.Right.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(6);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(4);

        // Act
        var min2 = tree.PopMin();

        // Assert
        min2.Should().Be(1);
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(5);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(3);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(4);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(6);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(3);
    }

    [Fact]
    public void Should_Not_Rotate_Then_Rotate_Left_Right_On_PopMax()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(2);
        tree.Add(1);
        tree.Add(5);
        tree.Add(0);
        tree.Add(3);
        tree.Add(7);
        tree.Add(4);
        tree.Add(6);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(5);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().NotBeNull();
        tree.Root.Right.Left.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Right.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.Left.Should().NotBeNull();
        tree.Root.Right.Right.Left!.Value.Should().Be(6);
        tree.Root.Right.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(4);

        // Act
        var max1 = tree.PopMax();

        // Assert
        max1.Should().Be(7);
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(5);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().NotBeNull();
        tree.Root.Right.Left.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Right.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(6);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(4);

        // Act
        var max2 = tree.PopMax();

        // Assert
        max2.Should().Be(6);
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(5);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Height.Should().Be(3);
    }

    [Fact]
    public void Should_Remove()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(4);
        tree.Add(2);
        tree.Add(5);
        tree.Add(1);
        tree.Add(3);
        tree.Add(6);
        tree.Add(0);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(4);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(3);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(5);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(6);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();

        // Act
        tree.Remove(5);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(6);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();

        // Act
        tree.Remove(1);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(3);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().NotBeNull();
        tree.Root.Right.Right!.Value.Should().Be(6);
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();

        // Act
        tree.Remove(0);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(4);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(3);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(6);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
    }

    [Fact]
    public void Should_Remove2()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        tree.Add(3);
        tree.Add(1);
        tree.Add(4);
        tree.Add(0);
        tree.Add(2);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(3);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.Should().NotBeNull();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Right.Should().NotBeNull();
        tree.Root.Left.Right!.Value.Should().Be(2);
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();

        // Act
        tree.Remove(4);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(1);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.Left.Should().NotBeNull();
        tree.Root.Right.Left!.Value.Should().Be(2);
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();

        // Act
        tree.Remove(1);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(2);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().NotBeNull();
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();

        // Act
        tree.Remove(2);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(3);
        tree.Root.Left.Should().NotBeNull();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Right.Should().BeNull();

        // Act
        tree.Remove(0);

        // Assert
        tree.Root.Should().NotBeNull();
        tree.Root!.Value.Should().Be(3);
        tree.Root.Left.Should().BeNull();
        tree.Root.Right.Should().BeNull();

        // Act
        tree.Remove(3);

        // Assert
        tree.Root.Should().BeNull();
    }

    [Fact]
    public void Should_Remove_Fail()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        var removed1 = tree.Remove(0);

        // Assert
        removed1.Should().BeFalse();

        // Arrange
        tree.Add(0);
        tree.Add(1);
        tree.Add(2);

        // Act
        var removed2 = tree.Remove(3);
        var removed3 = tree.Remove(-1);

        // Assert
        removed2.Should().BeFalse();
        removed3.Should().BeFalse();
    }

    [Fact]
    public void PopMin_Should_Throw_On_Empty_Tree()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        Action action = () => tree.PopMin();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void PopMax_Should_Throw_On_Empty_Tree()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        Action action = () => tree.PopMax();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Min_Should_Throw_On_Empty_Tree()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        Action action = () => tree.Min();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Max_Should_Throw_On_Empty_Tree()
    {
        // Arrange
        var tree = new AvlTree<int>();

        // Act
        Action action = () => tree.Max();

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}