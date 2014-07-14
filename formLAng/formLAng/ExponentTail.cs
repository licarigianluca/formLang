using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExponentTail
{
    public string id { get; private set; }
    public int intValue { get; private set; }

    public ExponentTail(int intValue)
    {
        this.intValue = intValue;
    }

    public ExponentTail(string id)
    {
        this.id = id;
    }
}

