using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Compiler {
    protected bool hidden = false;
    protected string cond;
    public Compiler() { }
    public void compile(Form f) {
        string path = @"C:\Users\gianlu\Desktop\PA\output\" + f.id + ".html";
        StringBuilder output = new StringBuilder();
        output.Append("<!DOCTYPE html><head><script src=\"jquery-1.11.1.min.js\"></script>\n<script src=GUIRenderer.js></script></head><body onload=\"init();\">\n<form></form>\n<script>\nvar init = function() {");
        output.Append(compileBlock(f.b));
        output.Append("}</script></body></html>");
        Console.WriteLine(output.ToString());
        System.IO.File.WriteAllText(path, output.ToString());
    }
    protected string compileBlock(Block b) {
        StringBuilder output = new StringBuilder();
        try { output.Append(compileStatementList(b.sl)); }
        catch (ArgumentNullException) { };
        return output.ToString();
    }
    protected string compileStatementList(StatementList sl) {
        if (sl == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        if (sl.c != null) {
            output.Append(compileControl(sl.c));
            try { output.Append(compileStatementList(sl.sl)); }
            catch (ArgumentNullException) { }
        } else {
            output.Append(compileStatement(sl.s));
            try { output.Append(compileStatementListHead(sl.slh)); }
            catch (ArgumentNullException) { }
        }
        return output.ToString();
    }
    protected string compileStatementListHead(StatementListHead slh) {
        if (slh == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(compileStatementListTail(slh.slt));
        return output.ToString();
    }
    protected string compileStatementListTail(StatementListTail slt) {
        StringBuilder output = new StringBuilder();
        if (slt.c != null) {
            output.Append(compileControl(slt.c));
            try { output.Append(compileStatementList(slt.sl)); }
            catch (ArgumentNullException) { }
        } else {
            output.Append(compileStatement(slt.s));
            try { output.Append(compileStatementListHead(slt.slh)); }
            catch (ArgumentNullException) { }
        }
        return output.ToString();
    }
    protected string compileStatement(Statement s) {
        StringBuilder output = new StringBuilder();
        if (hidden)
            output.Append(string.Format("GUIRenderer({{\"hidden\" : {0} , \"id\" : \"{1}\" , \"label\" : {2} , \"cond\" : \"{3}\" ",
                hidden.ToString().ToLower(), s.id, s.stringValue, cond));
        else
            output.Append(string.Format("GUIRenderer({{\"hidden\" : {0} , \"id\" : \"{1}\" , \"label\" : {2} ",
                hidden.ToString().ToLower(), s.id, s.stringValue));

        output.Append(compileType(s.t));
        output.AppendLine("});");
        return output.ToString();
    }
    protected string compileType(Type t) {
        StringBuilder output = new StringBuilder();
        output.Append(string.Format(",\"type\" : \"{0}\"", t.typeString.value));
        try { output.Append(compileTypeTail(t.tt)); }
        catch (ArgumentNullException) {
            output.Append(string.Format(", \"tag\" : \"input\", \"readOnly\" : false"));
        }
        return output.ToString();
    }
    protected virtual string compileTypeTail(TypeTail tt) {
        if (tt == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(string.Format(", \"tag\" : \"input\" , \"readOnly\" : true , \"expr\" : \"{0}\"", compileExpr(tt.e)));
        return output.ToString();
    }
    protected string compileControl(Control c) {
        StringBuilder output = new StringBuilder();
        try { cond = compileGuard(c.g).ToString(); }
        catch (ArgumentNullException) { }
        hidden = true;
        output.Append(compileBlock(c.b));
        hidden = false;
        cond = "";
        return output.ToString();
    }
    protected string compileGuard(Guard g) {
        StringBuilder output = new StringBuilder();
        if (g == null) throw new ArgumentNullException();
        try { output.Append(compileCondictionList(g.cl)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileCondictionList(ConditionList cl) {
        if (cl == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        try { output.Append(compileCondiction(cl.c)); }
        catch (ArgumentNullException) { }
        try { output.Append(compileCondictionListTail(cl.clt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileCondictionListTail(ConditionListTail clt) {
        if (clt == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(clt.op.value);
        output.Append(compileCondiction(clt.c));
        try { output.Append(compileCondictionList(clt.cl)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileCondiction(Condition c) {
        StringBuilder output = new StringBuilder();
        output.Append(compileCondictionHead(c.ch));
        try { output.Append(compileCondictionTail(c.ct)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileCondictionHead(ConditionHead ch) {
        StringBuilder output = new StringBuilder();
        if (ch.not != null) output.Append(ch.not.value);
        output.Append(compileExpr(ch.e));
        return output.ToString();
    }
    protected string compileCondictionTail(CondictionTail ct) {
        if (ct == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(ct.op.value);
        output.Append(compileCondictionHead(ct.ch));
        return output.ToString();
    }
    protected string compileExpr(Expr e) {
        StringBuilder output = new StringBuilder();
        output.Append(compileTerm(e.t));
        try { output.Append(compileExprTail(e.et)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileExprTail(ExprTail et) {
        if (et == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(et.op.value);
        output.Append(compileTerm(et.t));
        try { output.Append(compileExprTail(et.et)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileTerm(Term t) {
        StringBuilder output = new StringBuilder();
        output.Append(compileFactor(t.f));
        try { output.Append(compileTermTail(t.tt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileTermTail(TermTail tt) {
        if (tt == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(tt.op.value);
        output.Append(compileFactor(tt.f));
        try { output.Append(compileTermTail(tt.tt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileFactor(Factor f) {
        StringBuilder output = new StringBuilder();
        if (f.e != null) {
            output.Append(f.openPar.value);
            output.Append(compileExpr(f.e));
            output.Append(f.closePar.value);
        } else if (f.n != null) {
            output.Append(compileNum(f.n));
        } else {
            output.Append(f.stringValue);
            output.Append(f.boolValue.ToString().ToLower());
        }
        return output.ToString();
    }
    protected string compileNum(Num n) {
        StringBuilder output = new StringBuilder();
        try { output.Append(string.Format("Math.pow({0},{1})", compileBase(n.b), compileNumTail(n.nt))); }
        catch (ArgumentNullException) { output.Append(compileBase(n.b)); }
        return output.ToString();
    }
    protected string compileNumTail(NumTail nt) {
        StringBuilder output = new StringBuilder();
        if (nt == null) throw new ArgumentNullException();
        output.Append(compileExponent(nt.e));
        return output.ToString();
    }
    protected string compileBase(Base b) {
        StringBuilder output = new StringBuilder();
        if (b.type == "ID") {
            output.Append(b.id);
        } else if (b.type == "INTEGER") {
            output.Append(b.intValue);
        } else {
            output.Append(b.doubleValue);
        }
        return output.ToString();
    }
    protected string compileExponent(Exponent e) {
        StringBuilder output = new StringBuilder();
        if (e.minus != null) output.Append(e.minus.value);
        output.Append(compileExponentTail(e.et));
        return output.ToString();
    }
    protected string compileExponentTail(ExponentTail et) {
        StringBuilder output = new StringBuilder();
        if (et.type == "ID") output.Append(et.id);
        if (et.type == "INTEGER") output.Append(et.intValue);
        return output.ToString();
    }
}

