using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Parser
{
    private Tokenizer t;
    private Token lookahead;
    private Dictionary<string, List<int>> firstSets = new Dictionary<string, List<int>>(){
        {"Form",new List<int>{(int)type.FORM}},
        {"Block",new List<int>{(int)type.OPEN_CURLY}},
        {"StatementList",new List<int>{(int)type.ID,(int)type.IF}},
        {"StatementListTail",new List<int>{(int)type.COMMA}},
        {"Statement",new List<int>{(int)type.ID,(int)type.IF}},
        {"Type",new List<int>{(int)type.TYPE_BOOL,(int)type.TYPE_DATE,(int)type.TYPE_MONEY,(int)type.TYPE_REAL,(int)type.TYPE_INT,(int)type.TYPE_STRING}},
        {"TypeTail",new List<int>{(int)type.OPEN_PAR}},
        {"Guard", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionList", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionListTail", new List<int>{(int)type.AND,(int)type.OR}},
        {"Condiction", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionHead", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionTail",new List<int>{(int)type.LT,(int)type.GE,(int)type.LE,(int)type.GE,(int)type.EQUAL,(int)type.DISEQUAL}},
        {"Expr", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"ExprTail",new List<int>{(int)type.PLUS,(int)type.MINUS}},
        {"Term", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"TermTail",new List<int>{(int)type.TIMES,(int)type.SLASH}},
        {"Factor", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS,(int)type.ID,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"Num", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.MINUS}},
        {"Base", new List<int>{(int)type.INTEGER,(int)type.REAL}},
        {"Exponent",new List<int>{(int)type.POWER}},
        {"ExponentTail",new List<int>{(int)type.INTEGER,(int)type.MINUS}}
    };

    public Parser()
    {

    }
      
    public T parse<T>(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();
        T p = (T)(Object)Num();
        
        Match(type.EOF);
        return p;

    }
    //TermTail	->	'*'	Factor	TermTail	|
    //              '/'	Factor	TermTail	|	eps
    TermTail TermTail()
    {
        if (lookahead.type == (int)type.TIMES)
        {
            
        }
    }
    //Factor	->	'('	Expr ')'	|
    //               Id				|
    //               Num			|
    //               String			|
    //               Bool			
    Factor Factor()
    {
        Expr e;

        if (lookahead.type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = Expr();
            Match(type.CLOSE_PAR);
            return new Factor(e);
        }
        else if (lookahead.type == (int)type.ID)
        {
            string id = lookahead.value;
            Match(type.ID);
            return new Factor(id, "ID");

        }
        else if (lookahead.type == (int)type.STRING)
        {
            string id = lookahead.value;
            Match(type.STRING);
            return new Factor(id, "STRING");

        }
        else if (lookahead.type == (int)type.BOOL)
        {
            string boolValue = lookahead.value;
            Match(type.BOOL);
            return new Factor(boolValue.Equals("true"));
        }
        else
        {
            return new Factor(Num());
        }
        
    }

    //Num	->	Base Exponent		|
    //          '-'	Base Exponent	
    Num Num()
    {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS)
        {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }

        return new Num(minus, Base(), Exponent());
    }

    //Base ->	Integer	|	Real
    Base Base()
    {
        if (lookahead.type == (int)type.INTEGER)
        {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new Base(intValue);
        }
        else
        {
            double doubleValue = Convert.ToDouble(lookahead.value);
            Match(type.REAL);
            return new Base(doubleValue);
        }
    }

    //Exponent	->	'^'	ExponentTail	|	eps
    Exponent Exponent()
    {
        if (lookahead.type == (int)type.POWER)
        {
            Match(type.POWER);
            return new Exponent(ExponentTail());
        }
        else return null;
    }

    //ExponentTail	->	Integer			|
    //                  '-'	Integer	
    ExponentTail ExponentTail()
    {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS)
        {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }

        int intValue = Convert.ToInt32(lookahead.value);
        Match(type.INTEGER);
        return new ExponentTail(minus, intValue);

    }




    protected void Match(type t)
    {
        Debug.Assert(lookahead.type == (int)t, "Syntax error expected " + Test.converter((int)t));
        lookahead = this.t.nextToken();
    }

}