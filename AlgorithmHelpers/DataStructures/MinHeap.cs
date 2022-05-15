using System;

namespace AlgorithmHelpers.DataStructures;

public class MinHeap<T> : Heap<T>
    where T : IComparable<T>
{
    protected override void HeapifyUp(int index)
    {
        while (index > 0 && Items[index].CompareTo(Items[Parent(index)]) < 0)
        {
            var parent = Parent(index);
            (Items[index], Items[parent]) = (Items[parent], Items[index]);
            index = parent;
        }
    }

    protected override void HeapifyDown(int index)
    {
        int min = index;
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < Items.Count && Items[left].CompareTo(Items[min]) < 0)
                min = left;

            if (right < Items.Count && Items[right].CompareTo(Items[min]) < 0)
                min = right;

            if (min == index)
                return;

            (Items[index], Items[min]) = (Items[min], Items[index]);
            index = min;
        }
    }
}