using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectListTail
{
    public Atomic<char> comma { get; private set; }
    public SelectListHead slh { get; private set; }
    public SelectList sl { get; private set; }

    public SelectListTail(SelectListHead slh, SelectList sl)
    {
        this.comma = new Atomic<char>(',');
        this.slh = slh;
        this.sl = sl;
    }
}

