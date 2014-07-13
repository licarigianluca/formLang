using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExponentTail
{
    public Atomic<char> minus { get; private set; }
    public int intValue { get; private set; }

    public ExponentTail(Atomic<char> minus, int intValue)
    {
        this.minus = minus;
        this.intValue = intValue;
    }
}

