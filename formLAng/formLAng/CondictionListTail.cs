using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CondictionListTail
{
    public Atomic<string> op { get; private set; }
    public Condiction c { get; private set; }
    public CondictionList cl { get; private set; }

    public CondictionListTail(Atomic<string> op, Condiction c, CondictionList cl)
    {
        this.op = op;
        this.c = c;
        this.cl = cl;
    }
}

