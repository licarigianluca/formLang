using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NumTail
{
    public Atomic<char> power { get; private set; }
    public Exponent e { get; private set; }

    public NumTail(Exponent e)
    {
        this.power = new Atomic<char>('^');
        this.e = e;
    }

}

