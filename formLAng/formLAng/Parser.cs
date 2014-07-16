using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Parser
{
    protected Tokenizer t;
    protected Token lookahead;
    protected Dictionary<string, List<int>> firstSets = new Dictionary<string, List<int>>(){
        {"Form",new List<int>{(int)type.FORM}},
        {"Block",new List<int>{(int)type.OPEN_CURLY}},
        {"StatementList",new List<int>{(int)type.ID,(int)type.IF}},
        {"StatementListTail",new List<int>{(int)type.COMMA}},
        {"Statement",new List<int>{(int)type.ID}},
        {"Control",new List<int>{(int)type.IF}},
        {"Type",new List<int>{(int)type.TYPE_BOOL,(int)type.TYPE_DATE,(int)type.TYPE_MONEY,(int)type.TYPE_REAL,(int)type.TYPE_INT,(int)type.TYPE_STRING}},
        {"TypeTail",new List<int>{(int)type.OPEN_PAR}},
        {"Guard", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionList", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionListTail", new List<int>{(int)type.AND,(int)type.OR}},
        {"Condiction", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictioinHead", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR,(int)type.NOT}},
        {"CondictionTail",new List<int>{(int)type.LT,(int)type.GE,(int)type.LE,(int)type.GT,(int)type.EQUAL,(int)type.DISEQUAL}},
        {"Expr", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"ExprTail",new List<int>{(int)type.PLUS,(int)type.MINUS}},
        {"Term", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"TermTail",new List<int>{(int)type.TIMES,(int)type.SLASH}},
        {"Factor", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS,(int)type.STRING,(int)type.BOOL,(int)type.OPEN_PAR}},
        {"Num", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID,(int)type.MINUS}},
        {"Base", new List<int>{(int)type.INTEGER,(int)type.REAL,(int)type.ID}},
        {"NumTail",new List<int>{(int)type.POWER}},
        {"Exponent",new List<int>{(int)type.MINUS,(int)type.INTEGER,(int)type.ID}},
        {"ExponentTail",new List<int>{(int)type.INTEGER,(int)type.ID}}
    };

    public Parser()
    {

    }

    public T parse<T>(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();
        T p = (T)(Object)Form();

        Match(type.EOF);
        return p;

    }
    //Form	->	'form' Id Block
    protected Form Form()
    {
        Match(type.FORM);
        string id = lookahead.value;
        Match(type.ID);
        return new Form(id, Block());
    }
    //Block	->	'{' StatementList '}'	
    protected Block Block()
    {
        Match(type.OPEN_CURLY);
        StatementList sl = StatementList();
        Match(type.CLOSE_CURLY);
        return new Block(sl);
    }
    //StatementList -> Statement StatementListHead	|
    //                 Control StatementList		| eps    
    protected StatementList StatementList()
    {
        if (lookahead.type == (int)type.ID)
        {
            return new StatementList(Statement(), StatementListHead());
        }
        else if (lookahead.type == (int)type.IF)
        {
            return new StatementList(Control(), StatementList());
        }
        else return null;
    }
    //StatementListHead -> ',' StatementListTail    |   eps
    protected StatementListHead StatementListHead()
    {
        if (lookahead.type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new StatementListHead(StatementListTail());
        }
        else return null;
    }

    //StatementListTail-> Statement StatementListHead		|
    //                    Control StatementList	
    protected StatementListTail StatementListTail()
    {
        if (lookahead.type == (int)type.ID)
        {
            return new StatementListTail(Statement(),StatementListHead());
        }
        else 
        {
            return new StatementListTail(Control(),StatementList());
        }
        
    }

    //Control   ->  'if' '(' Guard ')'  Block   
    protected Control Control()
    {
        Atomic<string> ifCode = new Atomic<string>(lookahead.value);
        Match(type.IF);
        Match(type.OPEN_PAR);
        Guard g = Guard();
        Match(type.CLOSE_PAR);
        return new Control(ifCode, g, Block());
    }

    //Statement	->	Id ':' String Type			
    protected Statement Statement()
    {
        string id = lookahead.value;
        Match(type.ID);
        Match(type.COLON);
        string stringValue = lookahead.value;
        Match(type.STRING);
        return new Statement(id, stringValue, Type());

    }

    //Type	->	'integer'	TypeTail	|
    //          'real'		TypeTail	|
    //          'boolean'	TypeTail	|
    //          'money'		TypeTail	|
    //          'date'		Typetail	|
    //          'string'	TypeTail
    protected Type Type()
    {
        int found = firstSets["Type"].Find(x => x == lookahead.type);
        Atomic<string> typeString = new Atomic<string>(lookahead.value);
        Match((type)found);
        return new Type(typeString, TypeTail());
    }

    //TypeTail	->	'('	Expr	')'	|	eps
    protected virtual TypeTail TypeTail()
    {
        if (lookahead.type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            Expr e = Expr();
            Match(type.CLOSE_PAR);
            return new TypeTail(e);
        }
        else return null;
    }

    //Guard	->	CondictionList	|	eps
    protected Guard Guard()
    {
        int found = firstSets["Guard"].Find(x => x == lookahead.type);
        if (found > 0)
        {
            return new Guard(CondictionList());
        }
        else return null;
    }

    //CondictionList	->	Condiction	CondictionListTail	|	eps
    protected CondictionList CondictionList()
    {
        int found = firstSets["CondictionList"].Find(x => x == lookahead.type);
        if (found > 0)
        {
            return new CondictionList(Condiction(), CondictionListTail());
        }
        else return null;
    }

    //CondictionListTail	->	'&&'	Condiction	CondictionList	|
    //                          '||'	Condiction	CondictionList  |   eps
    protected CondictionListTail CondictionListTail()
    {
        if (lookahead.type == (int)type.AND)
        {
            string op = lookahead.value;
            Match(type.AND);
            return new CondictionListTail(new Atomic<string>(op), Condiction(), CondictionList());
        }
        else if(lookahead.type == (int)type.OR)
        {
            string op = lookahead.value;
            Match(type.OR);
            return new CondictionListTail(new Atomic<string>(op), Condiction(), CondictionList());
        }
        else return null;
    }

    //Condiction	->	CondictionHead CondictionTail
    protected Condiction Condiction()
    {
        return new Condiction(CondictionHead(), CondictionTail());
    }

    //CondictionHead	->	Expr		    |	
    //                      '!'	    Expr
    protected CondictionHead CondictionHead()
    {
        Atomic<char> not = null;
        if (lookahead.type == (int)type.NOT)
        {
            Match(type.NOT);
            not = new Atomic<char>('!');
        }

        return new CondictionHead(not, Expr());
    }

    //CondictionTail	->	'<'		CondictionHead	|
    //                      '>'		CondictionHead	|
    //                      '<='	CondictionHead	|
    //                      '>='	CondictionHead	|
    //                      '=='	CondictionHead	|
    //                      '!='	CondictionHead	|	eps
    protected CondictionTail CondictionTail()
    {
        int found = firstSets["CondictionTail"].Find(x => x == lookahead.type);
        if (found > 0)
        {
            Atomic<string> op = new Atomic<string>(lookahead.value);
            Match((type)found);
            return new CondictionTail(op, CondictionHead());
        }
        else return null;

    }

    //Expr	->	Term	ExprTail
    protected Expr Expr()
    {
        return new Expr(Term(), ExprTail());
    }

    //ExprTail	->	'+'	Term	ExprTail	|	
    //              '-'	Term	ExprTail	|	eps
    protected ExprTail ExprTail()
    {
        if (lookahead.type == (int)type.PLUS)
        {
            Match(type.PLUS);
            return new ExprTail(new Atomic<char>('+'), Term(), ExprTail());

        }
        else if (lookahead.type == (int)type.MINUS)
        {
            Match(type.MINUS);
            return new ExprTail(new Atomic<char>('-'), Term(), ExprTail());

        }
        else return null;
    }

    //Term	->	Factor	TermTail
    protected Term Term()
    {
        return new Term(Factor(), TermTail());
    }

    //TermTail	->	'*'	Factor	TermTail	|
    //              '/'	Factor	TermTail	|	eps
    protected TermTail TermTail()
    {
        if (lookahead.type == (int)type.TIMES)
        {
            Match(type.TIMES);
            return new TermTail(new Atomic<char>('*'), Factor(), TermTail());

        }
        else if (lookahead.type == (int)type.SLASH)
        {
            Match(type.SLASH);
            return new TermTail(new Atomic<char>('/'), Factor(), TermTail());

        }
        else return null;
    }
    //Factor	->	'('	Expr ')'	|
    //               Num			|
    //               String			|
    //               Bool			
    protected Factor Factor()
    {
        Expr e;

        if (lookahead.type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = Expr();
            Match(type.CLOSE_PAR);
            return new Factor(e);
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

    //Num	->	Base NumTail		|
    //          '-'	Base NumTail	
    protected Num Num()
    {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS)
        {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }

        return new Num(minus, Base(), NumTail());
    }

    //Base ->	Integer	|
    //          Real    |
    //          Id
    protected Base Base()
    {
        if (lookahead.type == (int)type.INTEGER)
        {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new Base(intValue);
        }
        else if (lookahead.type == (int)type.REAL)
        {
            double doubleValue = Convert.ToDouble(lookahead.value);
            Match(type.REAL);
            return new Base(doubleValue);
        }
        else
        {
            string id = lookahead.value;
            Match(type.ID);
            return new Base(id);
        }
    }

    //NumTail	->	'^'	Exponent	|	eps
    protected NumTail NumTail()
    {
        if (lookahead.type == (int)type.POWER)
        {
            Match(type.POWER);
            return new NumTail(Exponent());
        }
        else return null;
    }

    //Exponent	->	'-'	ExponentTail	|
    //              ExponentTail		|
    protected Exponent Exponent()
    {
        Atomic<char> minus = null;
        if (lookahead.type == (int)type.MINUS)
        {
            Match(type.MINUS);
            minus = new Atomic<char>('-');
        }

        return new Exponent(minus, ExponentTail());
    }
    //ExponentTail	->	Integer	|
    //                  Id               
    protected ExponentTail ExponentTail()
    {

        if (lookahead.type == (int)type.INTEGER)
        {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new ExponentTail(intValue);
        }
        else
        {
            string id = lookahead.value;
            Match(type.ID);
            return new ExponentTail(id);
        }

    }




    protected void Match(type t)
    {
        Debug.Assert(lookahead.type == (int)t, "Syntax error expected " + Test.converter((int)t));
        lookahead = this.t.nextToken();
    }

}