using System;

namespace AlgorithmHelpers.DataStructures;

public class BinaryTreeNode<T>
{
    public BinaryTreeNode(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
    public BinaryTreeNode<T>? Left { get; set; }
    public BinaryTreeNode<T>? Right { get; set; }
}

public class BinarySearchTree<T>
    where T : IComparable
{
    private BinaryTreeNode<T>? Root { get; set; }
    
    
}