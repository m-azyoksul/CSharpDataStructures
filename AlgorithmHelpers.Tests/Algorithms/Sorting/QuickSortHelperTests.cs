using System;
using System.Collections.Generic;
using AlgorithmHelpers.Algorithms.Sorting;
using Xunit;

namespace AlgorithmHelpers.Tests.Algorithms.Sorting;

public class QuickSortHelperTests
{
    /// <summary>
    /// Inner class to test sorting list of IComparable objects
    /// </summary>
    private class Number : IComparable<Number>
    {
        public Number(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public int CompareTo(Number? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    [Fact]
    public void IntArray()
    {
        // Arrange
        var list = new[] {3, 2, 1, 5, 4};

        // Act
        list.QuickSort();

        // Assert
        Assert.Equal(new[] {1, 2, 3, 4, 5}, list);
    }

    [Fact]
    public void IntArray_Random()
    {
        // Arrange
        var list = new List<int>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
            list.Add(random.Next(0, 100));

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void IntArray_Duplicates()
    {
        // Arrange
        var list = new List<int>();
        for (var i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            list.Add(j);

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void IntList()
    {
        // Arrange
        var list = new List<int> {3, 2, 1, 5, 4};

        // Act
        list.QuickSort();

        // Assert
        Assert.Equal(new List<int> {1, 2, 3, 4, 5}, list);
    }

    [Fact]
    public void IntList_Random()
    {
        // Arrange
        var list = new List<int>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
            list.Add(random.Next(0, 100));

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void IntList_Duplicates()
    {
        // Arrange
        var list = new List<int>();
        for (var i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            list.Add(j);

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void NumberList()
    {
        // Arrange
        var list = new List<Number> {new(3), new(2), new(1), new(5), new(4)};

        // Act
        list.QuickSort();

        // Assert
        Assert.Equal(new List<Number> {new(1), new(2), new(3), new(4), new(5)}, list);
    }

    [Fact]
    public void NumberList_Random()
    {
        // Arrange
        var list = new List<Number>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
            list.Add(new(random.Next(0, 100)));

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void NumberList_Duplicates()
    {
        // Arrange
        var list = new List<Number>();
        for (var i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            list.Add(new(j));

        // Act
        list.QuickSort();

        // Assert
        Assert.True(list.IsSorted());
    }

    [Fact]
    public void ComparePredicateList()
    {
        // Arrange
        var list = new List<Number> {new(3), new(2), new(1), new(5), new(4)};

        // Act
        list.QuickSort((x, y) => x.Value.CompareTo(y.Value));

        // Assert
        Assert.Equal(new List<Number> {new(1), new(2), new(3), new(4), new(5)}, list);
    }

    [Fact]
    public void ComparePredicateList_Random()
    {
        // Arrange
        var list = new List<Number>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
            list.Add(new(random.Next(0, 100)));
        var comparePredicate = (Func<Number, Number, int>) ((x, y) => x.Value.CompareTo(y.Value));

        // Act
        list.QuickSort(comparePredicate);

        // Assert
        Assert.True(list.IsSorted(comparePredicate));
    }

    [Fact]
    public void ComparePredicateList_Duplicates()
    {
        // Arrange
        var list = new List<Number>();
        for (var i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            list.Add(new(j));

        var comparePredicate = (Func<Number, Number, int>) ((x, y) => x.Value.CompareTo(y.Value));

        // Act
        list.QuickSort(comparePredicate);

        // Assert
        Assert.True(list.IsSorted(comparePredicate));
    }

    [Fact]
    public void ComparerList()
    {
        // Arrange
        var list = new List<Number> {new(3), new(2), new(1), new(5), new(4)};

        // Act
        list.QuickSort(Comparer<Number>.Create((x, y) => x.Value.CompareTo(y.Value)));

        // Assert
        Assert.Equal(new List<Number> {new(1), new(2), new(3), new(4), new(5)}, list);
    }

    [Fact]
    public void ComparerList_Random()
    {
        // Arrange
        var list = new List<Number>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
            list.Add(new(random.Next(0, 100)));
        var comparer = Comparer<Number>.Create((x, y) => x.Value.CompareTo(y.Value));

        // Act
        list.QuickSort(comparer);

        // Assert
        Assert.True(list.IsSorted(comparer));
    }

    [Fact]
    public void ComparerList_Duplicates()
    {
        // Arrange
        var list = new List<Number>();
        for (var i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            list.Add(new(j));

        var comparer = Comparer<Number>.Create((x, y) => x.Value.CompareTo(y.Value));

        // Act
        list.QuickSort(comparer);

        // Assert
        Assert.True(list.IsSorted(comparer));
    }
}