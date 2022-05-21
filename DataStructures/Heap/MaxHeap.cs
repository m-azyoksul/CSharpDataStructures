using System;

namespace DataStructures.Heap;

public class MaxHeap<T> : Heap<T>
    where T : IComparable<T>
{
    protected override void HeapifyUp(int index)
    {
        while (index > 0 && Items[index].CompareTo(Items[Parent(index)]) > 0)
        {
            var parent = Parent(index);
            (Items[index], Items[parent]) = (Items[parent], Items[index]);
            index = parent;
        }
    }

    protected override void HeapifyDown(int index)
    {
        int max = index;
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < Items.Count && Items[left].CompareTo(Items[max]) > 0)
                max = left;

            if (right < Items.Count && Items[right].CompareTo(Items[max]) > 0)
                max = right;

            if (max == index)
                return;

            (Items[index], Items[max]) = (Items[max], Items[index]);
            index = max;
        }
    }
}