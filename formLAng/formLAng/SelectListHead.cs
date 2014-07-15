using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectListHead
{
    public SelectType st1 { get; private set; }
    public Atomic<char> colon { get; private set; }
    public SelectType st2 { get; private set; }

    public SelectListHead(SelectType st1, SelectType st2)
    {
        this.st1 = st1;
        this.colon = new Atomic<char>(':');
        this.st2 = st2;

    }
}

