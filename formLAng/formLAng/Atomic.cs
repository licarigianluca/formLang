using System;
using System.Collections.Generic;

public class Atomic<T>
{
    public T value { get; private set; }

    public Atomic(T value)
    {
        this.value = value;
    }

}