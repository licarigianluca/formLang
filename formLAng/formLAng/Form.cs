using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Form
{
    public Atomic<string> formCode { get; private set; }
    public string id { get; private set; }
    public Block b { get; private set; }

    public Form(string id, Block b)
    {
        this.formCode = new Atomic<string>("form");
        this.id = id;
        this.b = b;
    }
}

