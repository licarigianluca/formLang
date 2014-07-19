using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ConditionList {
    public Condition c { get; private set; }
    public ConditionListTail clt { get; private set; }
    public ConditionList(Condition c, ConditionListTail clt) {
        this.c = c;
        this.clt = clt;
    }
}
