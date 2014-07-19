using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Parser {
    protected Tokenizer t;
    protected Token lookahead;
    public Parser() { }
    public T parse<T>(String s) {
        t = new Tokenizer(s);
        lookahead = t.nextToken();
        T p = (T)(Object)Form();
        Match(type.EOF);
        return p;
    }
    protected Form Form() {
        Match(type.FORM);
        string id = lookahead.value;
        Match(type.ID);
        return new Form(id, Block());
    }
    protected Block Block() {
        Match(type.OPEN_CURLY);
        StatementList sl = StatementList();
        Match(type.CLOSE_CURLY);
        return new Block(sl);
    }
    protected StatementList StatementList() {
        if (lookahead.type == (int)type.ID) {
            return new StatementList(Statement(), StatementListHead());
        } else if (lookahead.type == (int)type.IF) {
            return new StatementList(Control(), StatementList());
        } else return null;
    }
    protected StatementListHead StatementListHead() {
        if (lookahead.type == (int)type.COMMA) {
            Match(type.COMMA);
            return new StatementListHead(StatementListTail());
        } else return null;
    }
    protected StatementListTail StatementListTail() {
        if (lookahead.type == (int)type.ID) {
            return new StatementListTail(Statement(), StatementListHead());
        } else {
            return new StatementListTail(Control(), StatementList());
        }
    }
    protected Control Control() {
        Atomic<string> ifCode = new Atomic<string>(lookahead.value);
        Match(type.IF);
        Match(type.OPEN_PAR);
        Guard g = Guard();
        Match(type.CLOSE_PAR);
        return new Control(ifCode, g, Block());
    }
    protected Statement Statement() {
        string id = lookahead.value;
        Match(type.ID);
        Match(type.COLON);
        string stringValue = lookahead.value;
        Match(type.STRING);
        return new Statement(id, stringValue, Type());
    }
    protected Type Type() {
        int found = lookahead.type;
        Atomic<string> typeString = new Atomic<string>(lookahead.value);
        Match((type)found);
        return new Type(typeString, TypeTail());
    }
    protected virtual TypeTail TypeTail() {
        if (lookahead.type == (int)type.OPEN_PAR) {
            Match(type.OPEN_PAR);
            Expr e = Expr();
            Match(type.CLOSE_PAR);
            return new TypeTail(e);
        } else return null;
    }
    protected Guard Guard() {
        if (lookahead.type == (int)type.INTEGER || lookahead.type == (int)type.REAL ||
            lookahead.type == (int)type.ID || lookahead.type == (int)type.MINUS ||
            lookahead.type == (int)type.STRING || lookahead.type == (int)type.BOOL ||
            lookahead.type == (int)type.OPEN_PAR || lookahead.type == (int)type.NOT) {
            return new Guard(CondictionList());
        } else return null;
    }
    protected ConditionList CondictionList() {
        if (lookahead.type == (int)type.INTEGER || lookahead.type == (int)type.REAL || 
            lookahead.type == (int)type.ID || lookahead.type == (int)type.MINUS ||
            lookahead.type == (int)type.STRING || lookahead.type == (int)type.BOOL ||
            lookahead.type == (int)type.OPEN_PAR || lookahead.type == (int)type.NOT) {
            return new ConditionList(Condiction(), CondictionListTail());
        } else return null;
    }
   protected ConditionListTail CondictionListTail() {
        if (lookahead.type == (int)type.AND) {
            Atomic<string> op = new Atomic<string>(lookahead.value);
            Match(type.AND);
            return new ConditionListTail(op, Condiction(), CondictionList());
        } else if (lookahead.type == (int)type.OR) {
            Atomic<string> op = new Atomic<string>(lookahead.value);
            Match(type.OR);
            return new ConditionListTail(op, Condiction(), CondictionList());
        } else return null;
    }
   protected Condition Condiction() {
        return new Condition(CondictionHead(), CondictionTail());
    }
   protected ConditionHead CondictionHead() {
        Atomic<char> not = null;
        if (lookahead.type == (int)type.NOT) {
            Match(type.NOT);
            not = new Atomic<char>('!');
        }
        return new ConditionHead(not, Expr());
    }
    protected CondictionTail CondictionTail() {
        if (lookahead.type == (int)type.LT || lookahead.type == (int)type.GT || 
            lookahead.type == (int)type.LE || lookahead.type == (int)type.GE ||
            lookahead.type == (int)type.EQUAL || lookahead.type == (int)type.DISEQUAL) {
            int found = lookahead.type;
            Atomic<string> op = new Atomic<string>(lookahead.value);
            Match((type)found);
            return new CondictionTail(op, CondictionHead());
        } else return null;
    }
    protected Expr Expr() {
        return new Expr(Term(), ExprTail());
    }
    protected ExprTail ExprTail() {
        if (lookahead.type == (int)type.PLUS) {
            Match(type.PLUS);
            return new ExprTail(new Atomic<char>('+'), Term(), ExprTail());
        } else if (lookahead.type == (int)type.MINUS) {
            Match(type.MINUS);
            return new ExprTail(new Atomic<char>('-'), Term(), ExprTail());
        } else return null;
    }
    protected Term Term() {
        return new Term(Factor(), TermTail());
    }
    protected TermTail TermTail() {
        if (lookahead.type == (int)type.TIMES) {
            Match(type.TIMES);
            return new TermTail(new Atomic<char>('*'), Factor(), TermTail());
        } else if (lookahead.type == (int)type.SLASH) {
            Match(type.SLASH);
            return new TermTail(new Atomic<char>('/'), Factor(), TermTail());
        } else return null;
    }
    protected Factor Factor() {
        if (lookahead.type == (int)type.OPEN_PAR) {
            Match(type.OPEN_PAR);
            Expr e = Expr();
            Match(type.CLOSE_PAR);
            return new Factor(e);
        } else if (lookahead.type == (int)type.STRING) {
            string id = lookahead.value;
            Match(type.STRING);
            return new Factor(id, "STRING");
        } else if (lookahead.type == (int)type.BOOL) {
            string boolValue = lookahead.value;
            Match(type.BOOL);
            return new Factor(boolValue.Equals("true"));
        } else return new Factor(Num());
    }
    protected Num Num() {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS) {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }
        return new Num(minus, Base(), NumTail());
    }
    protected Base Base() {
        if (lookahead.type == (int)type.INTEGER) {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new Base(intValue);
        } else if (lookahead.type == (int)type.REAL) {
            double doubleValue = Convert.ToDouble(lookahead.value);
            Match(type.REAL);
            return new Base(doubleValue);
        } else {
            string id = lookahead.value;
            Match(type.ID);
            return new Base(id);
        }
    }
    protected NumTail NumTail() {
        if (lookahead.type == (int)type.POWER) {
            Match(type.POWER);
            return new NumTail(Exponent());
        } else return null;
    }
    protected Exponent Exponent() {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS) {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }
        return new Exponent(minus, ExponentTail());
    }
    protected ExponentTail ExponentTail() {
        if (lookahead.type == (int)type.INTEGER) {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new ExponentTail(intValue);
        } else {
            string id = lookahead.value;
            Match(type.ID);
            return new ExponentTail(id);
        }
    }
    protected void Match(type t) {
        Debug.Assert(lookahead.type == (int)t, "Syntax error expected ");
        lookahead = this.t.nextToken();
    }
}