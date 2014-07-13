using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Num
{
    public Atomic<char> minus { get; private set; }
    public Base b { get; private set; }
    public Exponent e { get; private set; }

    public Num(Atomic<char> minus, Base b, Exponent e)
    {
        this.minus = minus;
        this.b = b;
        this.e = e;
    }
}
