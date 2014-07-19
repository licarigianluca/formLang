using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Exponent {
    public Atomic<char> minus { get; private set; }
    public ExponentTail et { get; private set; }
    public Exponent(Atomic<char> minus, ExponentTail et) {
        this.minus = minus;
        this.et = et;
    }
}

