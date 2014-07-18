using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConditionListTail
{
    public Atomic<string> op { get; private set; }
    public Condition c { get; private set; }
    public ConditionList cl { get; private set; }

    public ConditionListTail(Atomic<string> op, Condition c, ConditionList cl)
    {
        this.op = op;
        this.c = c;
        this.cl = cl;
    }
}

