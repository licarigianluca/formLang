using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExprTail
{
    public Atomic<char> op { get; private set; }
    public Term t { get; private set; }
    public ExprTail et { get; private set; }

    public ExprTail(Atomic<char> op, Term t, ExprTail et)
    {
        this.op = op;
        this.t = t;
        this.et = et;
    }
}

