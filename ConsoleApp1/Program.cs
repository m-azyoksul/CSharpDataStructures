using System;
using ConsoleApp1;

var result = MyClass.Foo();

if (result.Value != null)
{
    Console.WriteLine(result.Value.Count);
    Console.WriteLine(result.Error.Type);
}