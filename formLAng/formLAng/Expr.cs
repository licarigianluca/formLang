using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Expr {
    public Term t { get; private set; }
    public ExprTail et { get; private set; }
    public Expr(Term t, ExprTail et) {
        this.t = t;
        this.et = et;
    }
}

