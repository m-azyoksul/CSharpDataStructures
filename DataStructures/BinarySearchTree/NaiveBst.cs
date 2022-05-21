using System;

namespace DataStructures.BinarySearchTree;

public class BinaryNode<T> : BstNode<T, BinaryNode<T>>
{
    public BinaryNode(T value) : base(value)
    {
    }
}

public class NaiveBst<T> : Bst<T, BinaryNode<T>>
    where T : IComparable<T>
{
    public override void Add(T value)
    {
        if (Root == null)
        {
            Root = new BinaryNode<T>(value);
            return;
        }

        var current = Root;
        while (true)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                if (current.Left == null)
                {
                    current.Left = new BinaryNode<T>(value);
                    return;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new BinaryNode<T>(value);
                    return;
                }

                current = current.Right;
            }
        }
    }

    public override T PopMin()
    {
        if (Root == null)
            throw new InvalidOperationException("Bst is empty");

        (var min, Root) = PopMin(Root);
        return min;
    }

    private static (T, BinaryNode<T>?) PopMin(BinaryNode<T> node)
    {
        if (node.Left == null)
        {
            var result = node.Value;
            return (result, node.Right);
        }

        var current = node;
        while (current.Left!.Left != null)
            current = current.Left;

        var min = current.Left.Value;
        current.Left = current.Left.Right;
        return (min, node);
    }

    public override T PopMax()
    {
        if (Root == null)
            throw new InvalidOperationException("Bst is empty");
        if (Root.Right == null)
        {
            var result = Root.Value;
            Root = Root.Left;
            return result;
        }

        var current = Root;
        while (current.Right!.Right != null)
            current = current.Right;

        var max = current.Right.Value;
        current.Right = current.Right.Left;
        return max;
    }

    public override bool Remove(T value)
    {
        if (Root == null)
            return false;

        if (Root.Value.CompareTo(value) == 0)
        {
            Root = RemoveNode(Root);
            return true;
        }

        return RemoveFromBranches(Root, value);
    }

    private static bool RemoveFromBranches(BinaryNode<T> node, T value)
    {
        while (true)
        {
            // If the value is on the left side of the tree
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    return false;

                if (node.Left.Value.CompareTo(value) == 0)
                {
                    node.Left = RemoveNode(node.Left);
                    return true;
                }

                node = node.Left;
            }

            // If the value is on the right side of the tree
            else
            {
                if (node.Right == null)
                    return false;

                if (node.Right.Value.CompareTo(value) == 0)
                {
                    node.Right = RemoveNode(node.Right);
                    return true;
                }

                node = node.Right;
            }
        }
    }

    private static BinaryNode<T>? RemoveNode(BinaryNode<T> node)
    {
        if (node.Left == null)
            return node.Right;

        if (node.Right == null)
            return node.Left;

        (var min, node.Right) = PopMin(node.Right);
        node.Value = min;
        return node;
    }
}