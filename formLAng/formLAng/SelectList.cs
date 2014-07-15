using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectList
{
    public SelectListHead slh {get; private set;}
    public SelectListTail slt { get; private set; }

    public SelectList(SelectListHead slh, SelectListTail slt)
    {
        this.slh = slh;
        this.slt = slt;
    }
}

