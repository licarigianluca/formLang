using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Base
{
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public string stringValue { get; private set; }

    public Base(int intValue)
    {
        this.intValue = intValue;
    }

    public Base(double doubleValue)
    {
        this.doubleValue = doubleValue;
    }

    public Base(string stringValue)
    {
        this.stringValue = stringValue;
    }
}

