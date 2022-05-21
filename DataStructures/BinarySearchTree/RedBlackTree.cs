using System;

namespace DataStructures.BinarySearchTree;

public class RedBlackTreeNode<T> : BstNode<T, RedBlackTreeNode<T>>
{
    public RedBlackTreeNode(T value) : base(value)
    {
        IsRed = true;
    }

    public bool IsRed { get; set; }
}

public class RedBlackTree<T> : Bst<T, RedBlackTreeNode<T>>
    where T : IComparable<T>
{
    public override void Add(T value)
    {
        throw new System.NotImplementedException();
    }

    public override T PopMin()
    {
        throw new System.NotImplementedException();
    }

    public override T PopMax()
    {
        throw new System.NotImplementedException();
    }

    public override bool Remove(T value)
    {
        throw new System.NotImplementedException();
    }
}