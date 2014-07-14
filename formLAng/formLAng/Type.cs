using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Type
{
    public Atomic<string> typeString { get; private set; }
    public TypeTail tt { get; private set; }

    public Type(Atomic<string> typeString, TypeTail tt)
    {
        this.typeString = typeString;
        this.tt = tt;
    }
}

