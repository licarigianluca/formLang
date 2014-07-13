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

        {"CondictionListTail", new List<int>{(int)type.AND,(int)type.OR
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
        {"Exponent",new List<int>{(int)type.POWER},
        {"ExponentTail",new List<int>{(int)type.INTEGER,(int)type.MINUS}}

    };








    public Parser()
    {

    }

    //public T parse<T>(String s)
    //{
    //    t = new Tokenizer(s);
    //    lookahead = t.nextToken();
    //    //T p = (T)(Object)Form();
    //    Match(type.EOF);
    //    return p;

    //}

    //private Form Form()
    //{

    //}
    protected void Match(type t)
    {
        Debug.Assert(lookahead.type == (int)t, "Syntax error expected " + Test.converter((int)t));
        lookahead = this.t.nextToken();
    }

}