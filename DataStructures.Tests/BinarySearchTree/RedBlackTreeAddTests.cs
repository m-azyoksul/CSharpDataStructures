using DataStructures.BinarySearchTree;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests.BinarySearchTree;

public class RedBlackTreeTests
{
    [Fact]
    public void Should_Add_Node_To_Empty_Tree()
    {
        // Arrange
        var tree = new RedBlackTree<int>();

        // Act
        tree.Add(4);

        // Assert
        tree.Root!.Value.Should().Be(4);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left.Should().BeNull();
        tree.Root.Right.Should().BeNull();
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Not_Fixup()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(4);

        // Act
        tree.Add(3);

        // Assert
        tree.Root!.Value.Should().Be(4);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(3);
        tree.Root.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right.Should().BeNull();
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Right_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(4);
        tree.Add(3);

        // Act
        tree.Add(2);

        // Assert
        tree.Root!.Value.Should().Be(3);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Recolor_With_Root()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(4);
        tree.Add(3);
        tree.Add(2);

        // Act
        tree.Add(0);

        // Assert
        tree.Root!.Value.Should().Be(3);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Left_Right_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(4);
        tree.Add(3);
        tree.Add(2);
        tree.Add(0);

        // Act
        tree.Add(1);

        // Assert
        tree.Root!.Value.Should().Be(3);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Right!.Value.Should().Be(2);
        tree.Root.Left.Right.IsBlack.Should().BeFalse();
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Left.Right.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(4);
        tree.Root.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Right_Rotation_Then_Right_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(7);
        tree.Add(6);
        tree.Add(5);
        tree.Add(4);
        tree.Add(3);
        tree.Add(2);
        tree.Add(1);

        // Act
        tree.Add(0);

        // Assert
        tree.Root!.Value.Should().Be(4);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(2);
        tree.Root.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left!.Value.Should().Be(1);
        tree.Root.Left.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Left.Parent.Should().Be(tree.Root.Left.Left);
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Right!.Value.Should().Be(3);
        tree.Root.Left.Right.IsBlack.Should().BeTrue();
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Left.Right.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(6);
        tree.Root.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Left!.Value.Should().Be(5);
        tree.Root.Right.Left.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Left.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }


    [Fact]
    public void Should_Add_Node_And_Do_Left_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(0);
        tree.Add(1);

        // Act
        tree.Add(2);

        // Assert
        tree.Root!.Value.Should().Be(1);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(2);
        tree.Root.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Should().BeNull();
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Recolor_With_Root2()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(0);
        tree.Add(1);
        tree.Add(2);

        // Act
        tree.Add(4);

        // Assert
        tree.Root!.Value.Should().Be(1);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(2);
        tree.Root.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Should().BeNull();
        tree.Root.Right.Right!.Value.Should().Be(4);
        tree.Root.Right.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Right_Left_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(0);
        tree.Add(1);
        tree.Add(2);
        tree.Add(4);

        // Act
        tree.Add(3);

        // Assert
        tree.Root!.Value.Should().Be(1);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Left!.Value.Should().Be(2);
        tree.Root.Right.Left.IsBlack.Should().BeFalse();
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Left.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Right!.Value.Should().Be(4);
        tree.Root.Right.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Recolor_Without_Root()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(0);
        tree.Add(1);
        tree.Add(2);
        tree.Add(4);
        tree.Add(3);

        // Act
        tree.Add(5);

        // Assert
        tree.Root!.Value.Should().Be(1);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(0);
        tree.Root.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left.Should().BeNull();
        tree.Root.Left.Right.Should().BeNull();
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(3);
        tree.Root.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Left!.Value.Should().Be(2);
        tree.Root.Right.Left.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Left.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Right!.Value.Should().Be(4);
        tree.Root.Right.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right!.Value.Should().Be(5);
        tree.Root.Right.Right.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Right.Parent.Should().Be(tree.Root.Right.Right);
        tree.Root.Right.Right.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }

    [Fact]
    public void Should_Add_Node_And_Do_Left_Rotation_Then_Left_Rotation()
    {
        // Arrange
        var tree = new RedBlackTree<int>();
        tree.Add(0);
        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);
        tree.Add(5);
        tree.Add(6);

        // Act
        tree.Add(7);

        // Assert
        tree.Root!.Value.Should().Be(3);
        tree.Root.IsBlack.Should().BeTrue();
        tree.Root.Left!.Value.Should().Be(1);
        tree.Root.Left.IsBlack.Should().BeFalse();
        tree.Root.Left.Left!.Value.Should().Be(0);
        tree.Root.Left.Left.IsBlack.Should().BeTrue();
        tree.Root.Left.Left.Left.Should().BeNull();
        tree.Root.Left.Left.Right.Should().BeNull();
        tree.Root.Left.Left.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Right!.Value.Should().Be(2);
        tree.Root.Left.Right.IsBlack.Should().BeTrue();
        tree.Root.Left.Right.Left.Should().BeNull();
        tree.Root.Left.Right.Right.Should().BeNull();
        tree.Root.Left.Right.Parent.Should().Be(tree.Root.Left);
        tree.Root.Left.Parent.Should().Be(tree.Root);
        tree.Root.Right!.Value.Should().Be(5);
        tree.Root.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Left!.Value.Should().Be(4);
        tree.Root.Right.Left.IsBlack.Should().BeTrue();
        tree.Root.Right.Left.Left.Should().BeNull();
        tree.Root.Right.Left.Right.Should().BeNull();
        tree.Root.Right.Left.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Right!.Value.Should().Be(6);
        tree.Root.Right.Right.IsBlack.Should().BeTrue();
        tree.Root.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right!.Value.Should().Be(7);
        tree.Root.Right.Right.Right.IsBlack.Should().BeFalse();
        tree.Root.Right.Right.Right.Left.Should().BeNull();
        tree.Root.Right.Right.Right.Right.Should().BeNull();
        tree.Root.Right.Right.Right.Parent.Should().Be(tree.Root.Right.Right);
        tree.Root.Right.Right.Parent.Should().Be(tree.Root.Right);
        tree.Root.Right.Parent.Should().Be(tree.Root);
        tree.Root.Parent.Should().BeNull();
    }
}