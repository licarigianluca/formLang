using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Base
{
    public string type { get; private set; }
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public string id { get; private set; }

    public Base(int intValue)
    {
        this.intValue = intValue;
        this.type = "INTEGER";
    }

    public Base(double doubleValue)
    {
        this.doubleValue = doubleValue;
        this.type = "REAL";
    }

    public Base(string id)
    {
        this.id = id;
        this.type = "ID";
    }
}

