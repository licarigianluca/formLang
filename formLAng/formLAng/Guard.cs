using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Guard
{
    public CondictionList cl { get; private set; }

    public Guard(CondictionList cl)
    {
        this.cl = cl;
    }
}

