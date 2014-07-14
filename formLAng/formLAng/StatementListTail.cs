using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatementListTail
{
    public Atomic<char> comma { get; private set; }
    public Statement s { get; private set; }
    public Control c { get; private set; }
    public StatementListHead slh { get; private set; }
    public StatementList sl { get; private set; }

    public StatementListTail(Statement s, StatementListHead slh)
    {
        this.comma = new Atomic<char>(',');
        this.s = s;
        this.slh = slh;
    }

    public StatementListTail(Control c, StatementList sl)
    {
        this.comma = new Atomic<char>(',');
        this.c = c;
        this.sl = sl;
    }
}

