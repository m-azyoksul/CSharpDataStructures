using System;

namespace AlgorithmHelpers.DataStructures;

public class IndexedMaxIndexedHeap<TKey, TValue> : IndexedHeap<TKey, TValue>
    where TKey : notnull
    where TValue : IComparable
{
    protected override void HeapifyUp(int index)
    {
        while (index > 0 && Items[index].Value.CompareTo(Items[Parent(index)].Value) > 0)
        {
            var parent = Parent(index);
            KeyIndex[Items[index].Key] = parent;
            KeyIndex[Items[parent].Key] = index;
            (Items[index], Items[parent]) = (Items[parent], Items[index]);
            index = Parent(index);
        }
    }

    protected override void HeapifyDown(int index)
    {
        int max = index;
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < Items.Count && Items[left].Value.CompareTo(Items[max].Value) > 0)
                max = left;

            if (right < Items.Count && Items[right].Value.CompareTo(Items[max].Value) > 0)
                max = right;

            if (max == index)
                return;

            KeyIndex[Items[index].Key] = max;
            KeyIndex[Items[max].Key] = index;
            (Items[index], Items[max]) = (Items[max], Items[index]);
            index = max;
        }
    }
}