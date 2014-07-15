using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectType
{
    public string id { get; private set; }
    public string stringValue { get; private set; }
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public string type { get; private set; }

    public SelectType(string value ,string type)
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

    public SelectType(int intValue)
    {
        this.intValue = intValue;
    }

    public SelectType(double doubleValue)
    {
        this.doubleValue = doubleValue;
    }

    
}

