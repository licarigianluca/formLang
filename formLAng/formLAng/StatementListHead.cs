using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StatementListHead
{
    public Atomic<char> comma { get; private set; }
    public StatementListTail slt { get; private set; }

    public StatementListHead(StatementListTail slt)
    {
        this.comma = new Atomic<char>(',');
        this.slt = slt;
    }
}

