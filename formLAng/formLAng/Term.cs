using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Term {
    public Factor f { get; private set; }
    public TermTail tt { get; private set; }
    public Term(Factor f, TermTail tt) {
        this.f = f;
        this.tt = tt;
    }
}

