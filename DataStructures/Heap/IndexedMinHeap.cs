using System;

namespace DataStructures.Heap;

public class IndexedMinHeap<TKey, TValue> : IndexedHeap<TKey, TValue>
    where TKey : notnull
    where TValue : IComparable
{
    protected override void HeapifyUp(int index)
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

            KeyIndex[Items[index].Key] = min;
            KeyIndex[Items[min].Key] = index;
            (Items[index], Items[min]) = (Items[min], Items[index]);
            index = min;
        }
    }
}