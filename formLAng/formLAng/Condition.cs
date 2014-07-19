using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Condition {
    public ConditionHead ch { get; private set; }
    public CondictionTail ct { get; private set; }
    public Condition(ConditionHead ch, CondictionTail ct) {
        this.ch = ch;
        this.ct = ct;
    }
}

