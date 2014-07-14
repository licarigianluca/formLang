using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Statement
{
    public string id { get; private set; }
    public Atomic<char> colon { get; private set; }
    public string stringValue { get; private set; }
    public Type t { get; private set; }

    public Statement(string id, string stringValue, Type t)
    {
        this.id = id;
        this.colon = new Atomic<char>(':');
        this.stringValue = stringValue;
        this.t = t;

    }


}

