using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Exponent
{
    public Atomic<char> power { get; private set; }
    public ExponentTail et { get; private set; }

    public Exponent(ExponentTail et)
    {
        this.power = new Atomic<char>('^');
        this.et = et;
    }
}

