using System;
using System.Collections.Generic;

namespace AlgorithmHelpers.DataStructures;

public class MinHeap<TKey, TValue> : Heap<(TKey Key, TValue Value)>
    where TValue : IComparable<TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, int> _heapIndex;

    public MinHeap()
    {
        _heapIndex = new Dictionary<TKey, int>();
    }

    public new void Add((TKey Key, TValue Value) item)
    {
        if (_heapIndex.ContainsKey(item.Key))
            throw new ArgumentException("Key already exists in heap");

        _heapIndex.Add(item.Key, Items.Count);
        Items.Add(item);
        HeapifyUp(Items.Count - 1);
    }

    public new (TKey Key, TValue Value) Pop()
    {
        if (Items.Count == 0)
            throw new InvalidOperationException("Heap is empty");
        
        var min = Items[0];
        _heapIndex.Remove(Items[0].Key);
        
        Items[0] = Items[^1];
        _heapIndex[Items[0].Key] = 0;
        Items.RemoveAt(Items.Count - 1);

        HeapifyDown(0);
        return min;
    }

    public void Update(TKey key, TValue value)
    {
        if (!_heapIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        Items[index] = (key, value);
        HeapifyUp(index);
        HeapifyDown(index);
    }

    protected override void HeapifyUp(int index)
    {
        while (index > 0 && Items[index].Value.CompareTo(Items[Parent(index)].Value) < 0)
        {
            var parent = Parent(index);
            _heapIndex[Items[index].Key] = parent;
            _heapIndex[Items[parent].Key] = index;
            (Items[index], Items[parent]) = (Items[parent], Items[index]);
            index = Parent(index);
        }
    }

    protected override void HeapifyDown(int index)
    {
        int min = index;
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < Items.Count && Items[left].Value.CompareTo(Items[min].Value) < 0)
                min = left;

            if (right < Items.Count && Items[right].Value.CompareTo(Items[min].Value) < 0)
                min = right;

            if (min == index)
                return;

            _heapIndex[Items[index].Key] = min;
            _heapIndex[Items[min].Key] = index;
            (Items[index], Items[min]) = (Items[min], Items[index]);
            index = min;
        }
    }
}