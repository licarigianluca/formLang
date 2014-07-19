using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ConditionHead {
    public Atomic<char> not { get; private set; }
    public Expr e { get; private set; }
    public ConditionHead(Atomic<char> not, Expr e) {
        this.not = not;
        this.e = e;
    }
}

