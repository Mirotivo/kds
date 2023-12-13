using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class MyCustomAttribute : Attribute
{
    public MyCustomAttribute()
    {
        // You can add code here to initialize the attribute
    }
}
