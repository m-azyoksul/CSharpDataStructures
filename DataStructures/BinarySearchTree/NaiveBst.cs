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
        if (Root.Left == null)
        {
            var result = Root.Value;
            Root = Root.Right;
            return result;
        }

        var current = Root;
        while (current.Left!.Left != null)
            current = current.Left;

        var min = current.Left.Value;
        current.Left = null;
        return min;
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
        current.Right = null;
        return max;
    }

    public override bool Remove(T value)
    {
        throw new NotImplementedException();
    }
}