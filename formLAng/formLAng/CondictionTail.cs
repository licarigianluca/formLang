using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CondictionTail
{
    public Atomic<string> op { get; private set; }
    public CondictionHead ch { get; private set; }

    public CondictionTail(Atomic<string> op, CondictionHead ch)
    {
        this.op = op;
        this.ch = ch;
    }
}

