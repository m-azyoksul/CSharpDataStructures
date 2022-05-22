using System;
using System.Collections.Generic;

namespace DataStructures.BinarySearchTree;

public abstract class BstNode<T, TNode>
    where TNode : BstNode<T, TNode>
{
    protected BstNode(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
    public TNode? Left { get; set; }
    public TNode? Right { get; set; }
}

public abstract class Bst<T, TNode>
    where T : IComparable<T>
    where TNode : BstNode<T, TNode>
{
    public TNode? Root { get; set; }

    public abstract void Add(T value);

    public abstract bool Remove(T value);

    public T Min()
    {
        if (Root == null)
            throw new InvalidOperationException("Tree is empty");

        return Min(Root).Value;
    }

    private static BstNode<T, TNode> Min(BstNode<T, TNode> node)
    {
        while (true)
        {
            if (node.Left == null)
                return node;

            node = node.Left;
        }
    }

    public T Max()
    {
        if (Root == null)
            throw new InvalidOperationException("Tree is empty");

        return Max(Root).Value;
    }

    private static BstNode<T, TNode> Max(BstNode<T, TNode> node)
    {
        while (true)
        {
            if (node.Right == null)
                return node;

            node = node.Right;
        }
    }

    public abstract T PopMin();

    public abstract T PopMax();

    public bool Contains(T value)
    {
        if (Root == null)
            return false;

        return Contains(value, Root);
    }

    private static bool Contains(T value, BstNode<T, TNode> node)
    {
        while (true)
        {
            if (node.Value.CompareTo(value) == 0)
                return true;

            // If value is less than node's value, go left
            if (node.Value.CompareTo(value) > 0)
            {
                if (node.Left == null)
                    return false;

                node = node.Left;
                continue;
            }

            // If value is greater than node's value, go right
            if (node.Right == null)
                return false;

            node = node.Right;
        }
    }

    public int Height()
    {
        return Height(Root);
    }

    private static int Height(BstNode<T, TNode>? node)
    {
        if (node == null)
            return 0;

        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    public bool IsEmpty()
    {
        return Root == null;
    }

    public void Clear()
    {
        Root = null;
    }

    public List<T> ToSortedList()
    {
        var list = new List<T>();
        ToSortedList(Root, list);
        return list;
    }

    private static void ToSortedList(BstNode<T, TNode>? node, List<T> list)
    {
        while (true)
        {
            if (node == null) return;

            ToSortedList(node.Left, list);
            list.Add(node.Value);
            node = node.Right;
        }
    }
}