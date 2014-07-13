using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TermTail
{
    public Atomic<char> op { get; private set; }
    public Factor f { get; private set; }
    public TermTail tt { get; private set; }

    public TermTail(Atomic<char> op, Factor f, TermTail tt)
    {
        this.op = op;
        this.f = f;
        this.tt = tt;
    }
}

