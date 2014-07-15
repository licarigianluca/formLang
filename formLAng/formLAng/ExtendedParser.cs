using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ExtendedParser : Parser
{
    public ExtendedParser() : base() { }

    //TypeTail	->	'('	Expr	')'         	|
    //              '[' SelectList	']' Type	|	eps
      public override TypeTail TypeTail()
    {
        if (lookahead.type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            Expr e = Expr();
            Match(type.CLOSE_PAR);
            return new TypeTail(e);
        }
        else if (lookahead.type == (int)type.OPEN_SQUARE)
        {
            Match(type.OPEN_SQUARE);
            SelectList sl = SelectList();
            Match(type.CLOSE_SQUARE);
            string id = lookahead.value;
            Match(type.ID);
            return new TypeTail(sl, id);
        }
        else return null;
    }

    //SelectList	->	SelectListHead	SelectListTail	|	eps
    protected SelectList SelectList()
    {
        if (lookahead.type == (int)type.ID || lookahead.type == (int)type.REAL || lookahead.type == (int)type.INTEGER)
        {
            return new SelectList(SelectListHead(), SelectListTail());
        }
        else return null;
    }

    //SelectListHead	->	SelectType	':'	SelectType
    protected SelectListHead SelectListHead()
    {
        SelectType st = SelectType();
        Match(type.COLON);
        return new SelectListHead(st, SelectType());
    }

    //SelectListTail	->	','	SelectListHead	SelectList	|	eps
    protected SelectListTail SelectListTail()
    {
        if (lookahead.type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new SelectListTail(SelectListHead(), SelectList());
        }
        else return null;
    }

    //SelectType    ->  Id		|
    //                  Real	|
    //                  Integer |
    
    
    protected SelectType SelectType()
    {
        if (lookahead.type == (int)type.ID)
        {
            string id = lookahead.value;
            Match(type.ID);
            return new SelectType(id, "ID");
        }
        else if (lookahead.type == (int)type.REAL)
        {
            double doubleValue = Convert.ToDouble(lookahead.value);
            Match(type.REAL);
            return new SelectType(doubleValue);
        }
        else
        {
            int intValue = Convert.ToInt32(lookahead.value);
            Match(type.INTEGER);
            return new SelectType(intValue);
        }
    }
}

