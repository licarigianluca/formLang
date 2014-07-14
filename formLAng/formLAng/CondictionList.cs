using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CondictionList
{
    public Condiction c { get; private set; }
    public CondictionList cl { get; private set; }

    public CondictionList(Condiction c, CondictionList cl)
    {
        this.c = c;
        this.cl = cl;
    }
}
