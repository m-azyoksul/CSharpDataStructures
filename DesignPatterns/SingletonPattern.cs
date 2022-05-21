using System;

namespace DesignPatterns;

public sealed class SingletonClass
{
    private static readonly Lazy<SingletonClass> InstanceField = new(() => new SingletonClass());

    private SingletonClass()
    {
    }

    public static SingletonClass Instance => InstanceField.Value;
}