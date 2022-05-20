using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmHelpers.DataStructures;

Console.WriteLine("Hello World!");
var heap = new IndexedMinHeap<int, int>();


var stack = new Stack<int>();
stack.Push(1);
stack.Push(2);

var list = stack.ToList();
Console.WriteLine(string.Join(",", list));