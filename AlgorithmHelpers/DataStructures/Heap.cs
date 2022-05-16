using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmHelpers.DataStructures;

/// <summary>
/// Heap data implementation with dynamic array.
///     O(1) time peek
///     O(logn) time pop and insert
/// </summary>
public abstract class Heap<T> : IEnumerable<T>
{
    protected readonly List<T> Items;
    public int Count => Items.Count;

    protected Heap()
    {
        Items = new List<T>();
    }

    public void Add(T item)
    {
        Items.Add(item);
        HeapifyUp(Items.Count - 1);
    }

    public T Pop()
    {
        if (Items.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        var min = Items[0];
        Items[0] = Items[^1];
        Items.RemoveAt(Items.Count - 1);
        HeapifyDown(0);
        return min;
    }

    public T Peek()
    {
        if (Items.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return Items[0];
    }

    public void Remove(T item)
    {
        var index = Items.IndexOf(item);
        if (index == -1)
            throw new InvalidOperationException("Item not found");

        Items[index] = Items[^1];
        Items.RemoveAt(Items.Count - 1);
        HeapifyDown(index);
        HeapifyUp(index);
    }

    protected int Parent(int index)
    {
        if (index == 0)
            throw new InvalidOperationException("Root has no parent");

        return (index - 1) / 2;
    }

    protected int LeftChild(int index)
    {
        return 2 * index + 1;
    }

    protected int RightChild(int index)
    {
        return 2 * index + 2;
    }

    protected abstract void HeapifyUp(int index);

    protected abstract void HeapifyDown(int index);

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}