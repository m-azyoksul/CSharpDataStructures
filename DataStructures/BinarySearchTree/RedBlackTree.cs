using System;

namespace DataStructures.BinarySearchTree;

public class RedBlackTreeNode<T> : BstNode<T, RedBlackTreeNode<T>>
{
    public RedBlackTreeNode(T value) : base(value)
    {
        IsBlack = true;
    }

    public RedBlackTreeNode(T value, RedBlackTreeNode<T> parent) : base(value)
    {
        Parent = parent;
        IsBlack = false;
    }

    public bool IsBlack { get; set; }

    public RedBlackTreeNode<T>? Parent { get; set; }
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
/// <typeparam name="T">Node value type</typeparam>
public class RedBlackTree<T> : Bst<T, RedBlackTreeNode<T>>
    where T : IComparable<T>
{
    public override void Add(T value)
    {
        if (Root == null)
        {
            Root = new RedBlackTreeNode<T>(value);
            return;
        }

        // Insert the new node
        var current = Root;
        while (true)
        {
            if (value.CompareTo(current.Value) <= 0)
            {
                if (current.Left == null)
                {
                    current.Left = new RedBlackTreeNode<T>(value, current);
                    current = current.Left;
                    break;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new RedBlackTreeNode<T>(value, current);
                    current = current.Right;
                    break;
                }

                current = current.Right;
            }
        }

        // Insert fix-up
        while (current.Parent is {IsBlack: false}) // Parent is not black which means it is not root.
        {
            if (current.Parent == current.Parent.Parent!.Left) // If the parent is a left child
            {
                var uncle = current.Parent.Parent.Right;
                if (uncle is {IsBlack: false}) // Case 1: Uncle is red
                {
                    current.Parent.IsBlack = true;
                    uncle.IsBlack = true;
                    current.Parent.Parent.IsBlack = false;
                    current = current.Parent.Parent;
                }
                else // Case 2: Uncle is black
                {
                    if (current == current.Parent.Right) // Case 2.1: If the node is a right child
                    {
                        current = current.Parent;
                        LeftRotate(current);
                    }

                    // Case 2.2: If the node is a left child
                    current.Parent!.IsBlack = true;
                    current.Parent.Parent!.IsBlack = false;
                    RightRotate(current.Parent.Parent);
                }
            }
            else // If the parent is a right child
            {
                var uncle = current.Parent.Parent.Left;
                if (uncle is {IsBlack: false}) // Case 1: Uncle is red
                {
                    current.Parent.IsBlack = true;
                    uncle.IsBlack = true;
                    current.Parent.Parent.IsBlack = false;
                    current = current.Parent.Parent;
                }
                else // Case 2: Uncle is black
                {
                    if (current == current.Parent.Left) // Case 2.1: If the node is a left child
                    {
                        current = current.Parent;
                        RightRotate(current);
                    }

                    // Case 2.2: If the node is a right child
                    current.Parent!.IsBlack = true;
                    current.Parent.Parent!.IsBlack = false;
                    LeftRotate(current.Parent.Parent);
                }
            }
        }
        
        Root.IsBlack = true;
    }

    private void LeftRotate(RedBlackTreeNode<T> node)
    {
        // Update root
        if (node == Root)
            Root = node.Right;

        // Connect parent and right child
        node.Right!.Parent = node.Parent;
        if (node.Parent == null)
        {
        }
        else if (node == node.Parent.Left)
            node.Parent.Left = node.Right;
        else
            node.Parent.Right = node.Right;

        // Set node's right child as parent
        node.Parent = node.Right;

        // Connect node and right child's left child
        node.Right = node.Right.Left;
        if (node.Right != null)
            node.Right.Parent = node;

        // Set new root's left child as node
        node.Parent.Left = node;
    }

    private void RightRotate(RedBlackTreeNode<T> node)
    {
        // Update root
        if (node == Root)
            Root = node.Left;

        // Connect parent and left child
        node.Left!.Parent = node.Parent;
        if (node.Parent == null)
        {
        }
        else if (node == node.Parent.Right)
            node.Parent.Right = node.Left;
        else
            node.Parent.Left = node.Left;

        // Set node's left child as parent
        node.Parent = node.Left;

        // Connect node and left child's right child
        node.Left = node.Left.Right;
        if (node.Left != null)
            node.Left.Parent = node;

        // Set new root's right child as node
        node.Parent.Right = node;
    }

    public override T PopMin()
    {
        if (Root == null)
            throw new InvalidOperationException("The tree is empty");
        if (Root.Left == null)
        {
            var value = Root.Value;
            Root = Root.Right;
            if (Root != null)
                Root.Parent = null;
            return value;
        }

        var current = Root.Left;
        while (current.Left != null)
            current = current.Left;

        if (!current.IsBlack)
        {
            current.Parent!.Left = null;
        }

        //Remove(current);
        return current.Value;
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