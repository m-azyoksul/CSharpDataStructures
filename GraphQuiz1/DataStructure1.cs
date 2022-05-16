using System;
using System.Collections.Generic;

namespace GraphQuiz1;

public interface IDataStructure
{
    void Add(int key, int value); // O(log n)
    void Update(int key, int value); // O(log n)
    (int Key, int Value) PopMinValue(); // O(log n)
    int GetValue(int key); // O(1)
    bool ContainsKey(int key); // O(1)
    int Count(); // O(1)
}

public class DataStructure : IDataStructure
{
    public int Count()
    {
        throw new NotImplementedException();
    }

    public (int Key, int Value) PopMinValue()
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(int key)
    {
        throw new NotImplementedException();
    }

    public void Add(int key, int value)
    {
        throw new NotImplementedException();
    }

    public int GetValue(int key)
    {
        throw new NotImplementedException();
    }

    public void Update(int key, int value)
    {
        throw new NotImplementedException();
    }
}

public class DataStructureSolution : IDataStructure
{
    private readonly List<(int Key, int Value)> Items;
    private readonly Dictionary<int, int> KeyIndex;

    public DataStructureSolution()
    {
        Items = new List<(int Key, int Value)>();
        KeyIndex = new Dictionary<int, int>();
    }

    public int Count()
    {
        return Items.Count;
    }

    public (int Key, int Value) PopMinValue()
    {
        if (Items.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        var min = Items[0];
        KeyIndex.Remove(Items[0].Key);

        Items[0] = Items[^1];
        KeyIndex[Items[0].Key] = 0;
        Items.RemoveAt(Items.Count - 1);

        HeapifyDown(0);
        return min;
    }

    public bool ContainsKey(int key)
    {
        return KeyIndex.ContainsKey(key);
    }

    public void Add(int key, int value)
    {
        if (KeyIndex.ContainsKey(key))
            throw new ArgumentException("Key already exists in heap");

        KeyIndex.Add(key, Items.Count);
        Items.Add((key, value));
        HeapifyUp(Items.Count - 1);
    }

    public int GetValue(int key)
    {
        if (!KeyIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        return Items[index].Value;
    }

    public void Update(int key, int value)
    {
        if (!KeyIndex.TryGetValue(key, out var index))
            throw new ArgumentException("Key does not exist in heap");

        Items[index] = (key, value);
        HeapifyUp(index);
        HeapifyDown(index);
    }


    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && Items[index].Value.CompareTo(Items[Parent(index)].Value) < 0)
        {
            var parent = Parent(index);
            KeyIndex[Items[index].Key] = parent;
            KeyIndex[Items[parent].Key] = index;
            (Items[index], Items[parent]) = (Items[parent], Items[index]);
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int min = index;
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < Items.Count && Items[left].Value.CompareTo(Items[min].Value) < 0)
                min = left;

            if (right < Items.Count && Items[right].Value.CompareTo(Items[min].Value) < 0)
                min = right;

            if (min == index)
                return;

            KeyIndex[Items[index].Key] = min;
            KeyIndex[Items[min].Key] = index;
            (Items[index], Items[min]) = (Items[min], Items[index]);
            index = min;
        }
    }
}