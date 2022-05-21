using System;
using System.Collections.Generic;

namespace AlgorithmHelpers.DataStructures;

/// <summary>
/// Special heap implementation with dynamic array and key-index dictionary.
///     O(1) time peek
///     O(logn) time pop and insert
/// 
/// Key-index dictionary also allows
///     O(1) time key search
///     O(logn) time update and delete.
/// </summary>
public abstract class IndexedHeap<TKey, TValue> : Heap<(TKey Key, TValue Value)>
    where TKey : notnull
{
    protected readonly Dictionary<TKey, int> KeyIndex;

    protected IndexedHeap()
    {
        KeyIndex = new Dictionary<TKey, int>();
    }

    public new void Add((TKey Key, TValue Value) item)
    {
        if (KeyIndex.ContainsKey(item.Key))
            throw new ArgumentException("Key already exists in heap");

        KeyIndex.Add(item.Key, Items.Count);
        Items.Add(item);
        HeapifyUp(Items.Count - 1);
    }

    public void Add(TKey key, TValue value)
    {
        Add((key, value));
    }

    public new (TKey Key, TValue Value) Pop()
    {
        if (Items.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        var min = Items[0];
        KeyIndex.Remove(min.Key);
        
        if (Items.Count == 1)
        {
            Items.RemoveAt(0);
        }
        else
        {
            Items[0] = Items[^1];
            KeyIndex[Items[0].Key] = 0;
            Items.RemoveAt(Items.Count - 1);

            HeapifyDown(0);
        }

        return min;
    }

    public int IndexOfKey(TKey key)
    {
        if (!KeyIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        return index;
    }

    public bool ContainsKey(TKey key)
    {
        return KeyIndex.ContainsKey(key);
    }

    public TValue ValueOfKey(TKey key)
    {
        return Items[IndexOfKey(key)].Value;
    }

    public void UpdateKey(TKey key, TValue value)
    {
        if (!KeyIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        Items[index] = (key, value);
        HeapifyUp(index);
        HeapifyDown(index);
    }

    public void RemoveKey(TKey key)
    {
        if (!KeyIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        KeyIndex.Remove(key);
        Items[index] = Items[^1];
        KeyIndex[Items[index].Key] = index;
        Items.RemoveAt(Items.Count - 1);

        HeapifyDown(index);
    }
}