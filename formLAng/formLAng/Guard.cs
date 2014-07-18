using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Guard
{
    public ConditionList cl { get; private set; }

    public Guard(ConditionList cl)
    {
        this.cl = cl;
    }
}

