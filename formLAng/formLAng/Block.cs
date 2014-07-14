using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Block
{
    public Atomic<char> openCurly { get; private set; }
    public StatementList sl { get; private set; }
    public Atomic<char> closeCurly { get; private set; }

    public Block(StatementList sl)
    {
        this.openCurly = new Atomic<char>('{');
        this.sl = sl;
        this.closeCurly = new Atomic<char>('}');
    }
}

