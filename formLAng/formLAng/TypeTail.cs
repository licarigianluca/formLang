using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TypeTail
{
    public Atomic<char> openPar { get; private set; }
    public Expr e { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public Atomic<char> openSquare { get; private set; }
    public SelectList sl { get; private set; }
    public Atomic<char> closeSquare { get; private set; }
    public string id { get; private set; }

    public TypeTail(Expr e)
    {
        this.openPar = new Atomic<char>('(');
        this.e = e;
        this.closePar = new Atomic<char>(')');
    }

    public TypeTail(SelectList sl, string id)
    {
        this.openSquare = new Atomic<char>('[');
        this.sl=sl;
        this.closeSquare = new Atomic<char>(']');
        this.id = id;
    }
}

