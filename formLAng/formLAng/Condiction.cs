using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Condiction
{
    public CondictionHead ch { get; private set; }
    public CondictionTail ct { get; private set; }

    public Condiction(CondictionHead ch, CondictionTail ct)
    {
        this.ch = ch;
        this.ct = ct;
    }
}

