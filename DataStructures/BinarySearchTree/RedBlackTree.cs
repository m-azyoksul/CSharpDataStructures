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

/// <summary>
/// A Red-Black Tree is a type of self-balancing binary search tree.
/// Its nodes are colored red or black.
/// The coloring of the nodes ensures that the tree remains somewhat balanced during insertions and deletions.
/// The coloring has the following properties:
/// - The root is black.
/// - Red nodes cannot have red children.
/// - Every path to a leaf node contains the same number of black nodes.
/// </summary>
/// <typeparam name="T"></typeparam>
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