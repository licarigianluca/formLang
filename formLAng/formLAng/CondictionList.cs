using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CondictionList
{
    public Condiction c { get; private set; }
    public CondictionListTail clt { get; private set; }

    public CondictionList(Condiction c, CondictionListTail clt)
    {
        this.c = c;
        this.clt = clt;
    }
}
