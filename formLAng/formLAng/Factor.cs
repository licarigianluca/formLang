using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Factor
{

    public Atomic<char> openPar { get; private set; }
    public Expr e { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public string id { get; private set; }
    public Num  n { get; private set; }
    public string stringValue { get; private set; }
    public bool boolValue { get; private set; }
    public string type { get; private set; }

    public Factor(Expr e)
    {
        this.openPar = new Atomic<char>('(');
        this.e = e;
        this.closePar = new Atomic<char>(')');
    }
    
    public Factor(Num n)
    {
        this.n = n;
    }

    public Factor(string value, string type)
    {
        if (type == "STRING")
        {
            this.stringValue = value;
        }
        else
        {
            this.id = value;
        }
        this.type = type;
    }
    public Factor(bool boolValue)
    {
        this.boolValue = boolValue;
    }

    
}

