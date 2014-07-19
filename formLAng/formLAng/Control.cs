using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Control {
    public Atomic<string> ifCode { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public Guard g { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public Block b { get; private set; }
public Control(Atomic<string> ifCode, Guard g, Block b) {
        this.ifCode = new Atomic<string>("if");
        this.openPar = new Atomic<char>('(');
        this.g = g;
        this.closePar = new Atomic<char>(')');
        this.b = b;
    }
}

