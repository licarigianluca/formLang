using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExponentTail {
    public string type { get; private set; }
    public string id { get; private set; }
    public int intValue { get; private set; }
    public ExponentTail(int intValue) {
        this.intValue = intValue;
        this.type = "INTEGER";
    }
    public ExponentTail(string id) {
        this.id = id;
        this.type = "ID";
    }
}

