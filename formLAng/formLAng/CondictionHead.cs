using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CondictionHead
{
    public Atomic<char> not { get; private set; }
    public Expr e { get; private set; }

    public CondictionHead(Atomic<char> not, Expr e)
    {
        this.not = not;
        this.e = e;
    }
}

