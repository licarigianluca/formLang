using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatementList {
    public Statement s { get; private set; }
    public StatementListHead slh { get; private set; }
    public Control c { get; private set; }
    public StatementList sl { get; private set; }
    public StatementList(Statement s, StatementListHead slh) {
        this.s = s;
        this.slh = slh;
    }
    public StatementList(Control c, StatementList sl) {
        this.c = c;
        this.sl = sl;
    }
}

