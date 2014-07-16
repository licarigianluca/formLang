using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Compiler
{
    private static bool hidden = false;
    private static string master;
    private static bool readOnly = false;

    public Compiler()
    {

    }

    public void compile(Form f)
    {
        string path = @"C:\Users\gianlu\Desktop\PA\output\" + f.id + ".html";
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileBlock(f.b));
        Console.WriteLine(output.ToString());
        System.IO.File.WriteAllText(path, output.ToString());

    }

    protected string compileBlock(Block b)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        try { output.Append(compileStatementList(b.sl)); }
        catch (ArgumentNullException) { };
        return output.ToString();
    }

    protected string compileStatementList(StatementList sl)
    {
        if (sl == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        if (sl.c != null)
        {
            output.Append(compileControl(sl.c));
            try { output.Append(compileStatementList(sl.sl)); }
            catch (ArgumentNullException) { }
        }
        else
        {
            output.Append(compileStatement(sl.s));
            try { output.Append(compileStatementListHead(sl.slh)); }
            catch (ArgumentNullException) { }
        }
        return output.ToString();
    }

    protected string compileStatementListHead(StatementListHead slh)
    {
        if (slh == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileStatementListTail(slh.slt));
        return output.ToString();
    }


    protected string compileStatementListTail(StatementListTail slt)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();

        if (slt.c != null)
        {
            output.Append(compileControl(slt.c));
            try { output.Append(compileStatementList(slt.sl)); }
            catch (ArgumentNullException) { }
        }
        else
        {
            output.Append(compileStatement(slt.s));
            try { output.Append(compileStatementListHead(slt.slh)); }
            catch (ArgumentNullException) { }
        }
        return output.ToString();
    }

    protected string compileStatement(Statement s)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(string.Format("{{\"hidden\" : \"{0}\", \"id\" : \"{1}\", \"label\" : {2} ", hidden.ToString().ToLower(), s.id, s.stringValue));
        output.Append(compileType(s.t));
        output.AppendLine("}");
        return output.ToString();
    }

    protected string compileType(Type t)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(string.Format(",\"type\" : \"{0}\"", t.typeString.value));
        try { output.Append(compileTypeTail(t.tt)); }
        catch (ArgumentNullException)
        {
            if (t.typeString.value.Equals("boolean"))
                output.Append(string.Format(", \"tag\" : \"checkbox\"", t.typeString.value));
            else
                output.Append(string.Format(", \"tag\" : \"input\", \"readOnly\" : true"));
        }
        return output.ToString();
    }
    protected string compileTypeTail(TypeTail tt)
    {
        if (tt == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(string.Format(",\"tag\" : \"input\", \"readOnly\" : false , \"expr\" : \"{0}\"", compileExpr(tt.e)));
        return output.ToString();
    }

    protected string compileControl(Control c)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        try 
        { 
          output.AppendLine(string.Format("{{\"tag\" : \"condiction\", \"cond\" : \"{0}\" ",  compileGuard(c.g)));
        
        }
        catch (ArgumentNullException) { }
        hidden = true;
        output.Append(compileBlock(c.b));
        hidden = false;
        return output.ToString();

    }

    protected string compileGuard(Guard g)
    {
        if (g == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        try { output.Append(compileCondictionList(g.cl)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileCondictionList(CondictionList cl)
    {
        if (cl == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        try { output.Append(compileCondiction(cl.c)); }
        catch (ArgumentNullException) { }
        try { output.Append(compileCondictionListTail(cl.clt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileCondictionListTail(CondictionListTail clt)
    {
        if (clt == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(clt.op.value);
        output.Append(compileCondiction(clt.c));
        try { output.Append(compileCondictionList(clt.cl)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileCondiction(Condiction c)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileCondictionHead(c.ch));
        try { output.Append(compileCondictionTail(c.ct)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileCondictionHead(CondictionHead ch)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        if (ch.not != null) output.Append(ch.not.value);
        output.Append(compileExpr(ch.e));
        return output.ToString();
    }

    protected string compileCondictionTail(CondictionTail ct)
    {
        if (ct == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(ct.op.value);
        output.Append(compileCondictionHead(ct.ch));
        return output.ToString();
    }
    protected string compileExpr(Expr e)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileTerm(e.t));
        try { output.Append(compileExprTail(e.et)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileExprTail(ExprTail et)
    {
        if (et == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(et.op.value);
        output.Append(compileTerm(et.t));
        try { output.Append(compileExprTail(et.et)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileTerm(Term t)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileFactor(t.f));
        try { output.Append(compileTermTail(t.tt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileTermTail(TermTail tt)
    {
        if (tt == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(tt.op.value);
        output.Append(compileFactor(tt.f));
        try { output.Append(compileTermTail(tt.tt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }

    protected string compileFactor(Factor f)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        if (f.e != null)
        {
            output.Append(f.openPar.value);
            output.Append(compileExpr(f.e));
            output.Append(f.closePar.value);
        }
        else if (f.n != null)
        {
            output.Append(compileNum(f.n));
        }
        else
        {
            output.Append(f.stringValue);
            output.Append(f.boolValue.ToString().ToLower());
        }
        return output.ToString();
    }

    protected string compileNum(Num n)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        try { output.Append(string.Format("Math.pow({0},{1})", compileBase(n.b), compileNumTail(n.nt))); }
        catch (ArgumentNullException) { output.Append(compileBase(n.b)); }
        return output.ToString();
    }

    protected string compileNumTail(NumTail nt)
    {
        if (nt == null) throw new ArgumentNullException();
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        output.Append(compileExponent(nt.e));
        return output.ToString();
    }

    protected string compileBase(Base b)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();

        if (b.type == "ID")
        {
            output.Append(b.id);
        }
        else if (b.type == "INTEGER")
        {
            output.Append(b.intValue);
        }
        else
        {
            output.Append(b.doubleValue);
        }

        return output.ToString();
    }

    protected string compileExponent(Exponent e)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        if (e.minus != null) output.Append(e.minus.value);
        output.Append(compileExponentTail(e.et));
        return output.ToString();
    }

    protected string compileExponentTail(ExponentTail et)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        if (et.type == "ID") output.Append(et.id);
        if (et.type == "INTEGER") output.Append(et.intValue);
        return output.ToString();
    }
}

