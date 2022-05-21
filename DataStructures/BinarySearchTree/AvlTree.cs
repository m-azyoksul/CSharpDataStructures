using System;

namespace AlgorithmHelpers.DataStructures;

public class AvlNode<T> : BstNode<T, AvlNode<T>>
{
    public AvlNode(T value) : base(value)
    {
        Height = 1;
    }

    public int Height { get; set; }
}

public class AvlTree<T> : Bst<T, AvlNode<T>>
    where T : IComparable<T>
{
    public AvlTree(AvlNode<T>? root = null)
    {
        Root = root;
    }

    public override void Add(T value)
    {
        Root = Add(Root, value);
    }

    private static AvlNode<T> Add(AvlNode<T>? node, T value)
    {
        if (node == null)
            return new AvlNode<T>(value);

        // Insert left
        if (value.CompareTo(node.Value) <= 0)
        {
            node.Left = Add(node.Left, value);
            return BalanceRight(node);
        }

        // Insert right
        node.Right = Add(node.Right, value);
        return BalanceLeft(node);
    }

    public override T PopMin()
    {
        if (Root == null)
            throw new InvalidOperationException("Tree is empty");

        return PopMin(Root).Value;
    }

    private static (T Value, AvlNode<T>?) PopMin(AvlNode<T> node)
    {
        if (node.Left == null)
            return (node.Value, node.Right);

        (var minValue, node.Left) = PopMin(node.Left);
        return (minValue, BalanceLeft(node));
    }

    public override T PopMax()
    {
        if (Root == null)
            throw new InvalidOperationException("Tree is empty");

        return PopMax(Root).Value;
    }

    private static (T Value, AvlNode<T>?) PopMax(AvlNode<T> node)
    {
        if (node.Right == null)
            return (node.Value, node.Left);

        (var maxValue, node.Right) = PopMax(node.Right);
        return (maxValue, BalanceRight(node));
    }

    public override bool Remove(T value)
    {
        (var removed, Root) = Remove(Root, value);
        return removed;
    }

    private static (bool Removed, AvlNode<T>?) Remove(AvlNode<T>? node, T value)
    {
        if (node == null)
            return (false, null);

        if (value.CompareTo(node.Value) < 0)
        {
            (var removed, node.Left) = Remove(node.Left, value);
            if (!removed)
                return (false, node);
            return (true, BalanceLeft(node));
        }

        if (value.CompareTo(node.Value) > 0)
        {
            (var removed, node.Right) = Remove(node.Right, value);
            if (!removed)
                return (false, node);
            return (true, BalanceRight(node));
        }

        // Found the node to remove
        if (node.Left == null)
            return (true, node.Right);
        if (node.Right == null)
            return (true, node.Left);

        // Node has two children
        (var minRight, node.Right) = PopMin(node.Right);
        node.Value = minRight;
        return (true, BalanceRight(node));
    }

    private static AvlNode<T> BalanceRight(AvlNode<T> node)
    {
        if (GetBalance(node) <= 1)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return node;
        }

        if (GetBalance(node.Left!) >= 0)
            return RotateRight(node);

        return RotateLeftRight(node);
    }

    private static AvlNode<T> BalanceLeft(AvlNode<T> node)
    {
        if (GetBalance(node) >= -1)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return node;
        }

        if (GetBalance(node.Right!) <= 0)
            return RotateLeft(node);

        return RotateRightLeft(node);
    }

    private static AvlNode<T> RotateRight(AvlNode<T> node)
    {
        var left = node.Left!;
        node.Left = left.Right;
        left.Right = node;

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        left.Height = 1 + Math.Max(GetHeight(left.Left), node.Height);

        return left;
    }

    private static AvlNode<T> RotateLeftRight(AvlNode<T> node)
    {
        var newRoot = node.Left!.Right!;
        node.Left!.Right = newRoot.Left;
        newRoot.Left = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        newRoot.Left.Height = 1 + Math.Max(GetHeight(newRoot.Left.Left), GetHeight(newRoot.Left.Right));
        newRoot.Height = 1 + Math.Max(newRoot.Left.Height, node.Height);

        return newRoot;
    }

    private static AvlNode<T> RotateLeft(AvlNode<T> node)
    {
        var right = node.Right!;
        node.Right = right.Left;
        right.Left = node;

        node.Height = 1 + Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0);
        right.Height = 1 + Math.Max(right.Left?.Height ?? 0, node.Height);

        return right;
    }

    private static AvlNode<T> RotateRightLeft(AvlNode<T> node)
    {
        var newRoot = node.Right!.Left!;
        node.Right!.Left = newRoot.Right;
        newRoot.Right = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        newRoot.Right.Height = 1 + Math.Max(GetHeight(newRoot.Right.Left), GetHeight(newRoot.Right.Right));
        newRoot.Height = 1 + Math.Max(node.Height, newRoot.Right.Height);

        return newRoot;
    }

    private static int GetHeight(AvlNode<T>? node)
    {
        return node?.Height ?? 0;
    }

    private static int GetBalance(AvlNode<T> node)
    {
        return GetHeight(node.Left) - GetHeight(node.Right);
    }
}