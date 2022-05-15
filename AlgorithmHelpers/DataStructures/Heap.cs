using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmHelpers.DataStructures;

public abstract class Heap<T> : IEnumerable<T>
{
    protected readonly List<T> Items;

    protected Heap()
    {
        Items = new List<T>();
    }

    public int Count => Items.Count;

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